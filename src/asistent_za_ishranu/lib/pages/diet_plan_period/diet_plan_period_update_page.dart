import 'package:asistent_za_ishranu/models/diet_plan_period_request.dart';
import 'package:asistent_za_ishranu/models/diet_plan_request.dart';
import 'package:asistent_za_ishranu/services/api_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter_datetime_picker/flutter_datetime_picker.dart';
import 'package:intl/intl.dart';

class DietPlanPeriodUpdatePage extends StatefulWidget {
  const DietPlanPeriodUpdatePage({Key? key}) : super(key: key);

  static const routeName = '/diet_plan_period_update';

  @override
  _DietPlanPeriodUpdatePageState createState() =>
      _DietPlanPeriodUpdatePageState();
}

class _DietPlanPeriodUpdatePageState extends State<DietPlanPeriodUpdatePage> {
  late Future<DietPlanPeriodRequest> foodProduct;
  final _formKey = GlobalKey<FormState>();
  var id = 0;
  var unitOfMeasureId = 0;
  var firstLoad = true;

  DateTime startDate = DateTime.now();
  DateTime endDate = DateTime.now();
  int dietPlanId = 0;

  List<DietPlanRequest>? dietPlans;

  Future<List<DietPlanRequest>> getDietPlans() async {
    var apiService = ApiService();
    var result = await apiService.get("api/dietplan?pageSize=1000&index=0");
    return DietPlanRequest.resultListFromJson(result);
  }

  Future<DietPlanPeriodRequest> getItem(id) async {
    var apiService = ApiService();
    var result = await apiService.get("api/dietplanperiod/$id");
    dietPlans = await getDietPlans();
    return DietPlanPeriodRequest.resultFromJson(result);
  }

  @override
  void initState() {
    super.initState();
    // future that allows us to access context. function is called inside the future
    // otherwise it would be skipped and args would return null
    Future.delayed(Duration.zero, () {
      setState(() {
        id = ModalRoute.of(context)!.settings.arguments as int;
      });
      foodProduct = getItem(id);
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: Text("Izmjeni period planah ishrane"),
        ),
        body: FutureBuilder<DietPlanPeriodRequest>(
            future: foodProduct,
            builder: (BuildContext context,
                AsyncSnapshot<DietPlanPeriodRequest> snapshot) {
              if (snapshot.connectionState == ConnectionState.waiting) {
                return Form(
                    child: Column(
                  children: [
                    TextFormField(
                      decoration: InputDecoration(labelText: ""),
                      initialValue: "",
                      readOnly: true,
                    )
                  ],
                ));
              } else {
                if (firstLoad) {
                  startDate = snapshot.data!.startDate!;
                  endDate = snapshot.data!.endDate!;
                  dietPlanId = snapshot.data!.dietPlanId!;
                  firstLoad = false;
                }
                ;
                return Form(
                    key: _formKey,
                    child: SingleChildScrollView(
                        child: Column(
                      children: [
                        ConstrainedBox(
                            constraints:
                                BoxConstraints.tight(const Size(200, 50)),
                            child: DropdownButtonFormField(
                              items: dietPlans!
                                  .map((e) => DropdownMenuItem(
                                        child: Text(e.name!),
                                        value: e.id,
                                      ))
                                  .toList(),
                              hint: Text('Plan ishrane'),
                              onChanged: (value) {
                                setState(() {
                                  dietPlanId = value! as int;
                                });
                              },
                              value: dietPlanId,
                              validator: (int? value) {
                                if (value == null || value == 0) {
                                  return 'Odaberite plan ishrane';
                                }
                                return null;
                              },
                            )),
                        ConstrainedBox(
                            constraints:
                                BoxConstraints.tight(const Size(200, 50)),
                            child: Padding(
                                padding: EdgeInsets.all(10),
                                child: ElevatedButton(
                                  onPressed: () {
                                    DatePicker.showDatePicker(context,
                                        showTitleActions: true,
                                        onConfirm: (date) {
                                      setState(() {
                                        startDate = date;
                                      });
                                    }, currentTime: startDate);
                                  },
                                  child: Text(
                                    'Početni datum',
                                  ),
                                ))),
                        Text("${DateFormat('dd.MM.yyyy').format(startDate)}"),
                        ConstrainedBox(
                            constraints:
                                BoxConstraints.tight(const Size(200, 50)),
                            child: Padding(
                                padding: EdgeInsets.all(10),
                                child: ElevatedButton(
                                  onPressed: () {
                                    DatePicker.showDatePicker(
                                      context,
                                      showTitleActions: true,
                                      onConfirm: (date) {
                                        setState(() {
                                          endDate = date;
                                        });
                                      },
                                      currentTime: endDate,
                                    );
                                  },
                                  child: Text(
                                    'Krajnji datum',
                                  ),
                                ))),
                        Text("${DateFormat('dd.MM.yyyy').format(endDate)}"),
                        Center(
                          child: ElevatedButton(
                            child: Text("Izmijeni"),
                            onPressed: () async {
                              if (_formKey.currentState!.validate()) {
                                var apiService = ApiService();
                                var dietPlanPeriodRequest = DietPlanPeriodRequest(
                                            dietPlanId, startDate, endDate, id)
                                        .modelToJson();
                                var result = await apiService.put(
                                    "api/dietplanperiod/", dietPlanPeriodRequest
                                    );
                                Navigator.of(context).pop();
                              }
                            },
                          ),
                        ),
                      ],
                    )));
              }
            }));
  }
}
