/* import 'package:asistent_za_ishranu/models/food_stock_request.dart';
import 'package:asistent_za_ishranu/models/diet_plan_request.dart';
import 'package:asistent_za_ishranu/services/api_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter_datetime_picker/flutter_datetime_picker.dart';
import 'package:intl/intl.dart';

class FoodStockCreatePage extends StatefulWidget {
  const FoodStockCreatePage({Key? key}) : super(key: key);

  static const routeName = "/food_stock_create";

  @override
  _FoodStockCreatePageState createState() =>
      _FoodStockCreatePageState();
}

class _FoodStockCreatePageState extends State<FoodStockCreatePage> {
  final _formKey = GlobalKey<FormState>();
  DateTime startDate = DateTime.now();
  DateTime endDate = DateTime.now();
  int dietPlanId = 0;
  late Future<List<DietPlanRequest>> dietPlans;

  Future<void> create() async {
    {
      var apiService = ApiService();
      await apiService.post("api/foodstock",
          FoodStockRequest(dietPlanId, startDate, endDate).modelToJson());

      Navigator.of(context).pop(context);
    }
  }

  Future<List<DietPlanRequest>> getDietPlans() async {
    var apiService = ApiService();
    var result = await apiService.get("api/dietplan?pageSize=1000&index=0");
    return DietPlanRequest.resultListFromJson(result);
  }

  @override
  void initState() {
    super.initState();
    // future that allows us to access context. function is called inside the future
    // otherwise it would be skipped and args would return null
    Future.delayed(Duration.zero, () {
      setState(() {});
      dietPlans = getDietPlans();
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: Text("Unos perioda plana ishrane"),
        ),
        body: FutureBuilder<List<DietPlanRequest>>(
            future: dietPlans,
            builder: (BuildContext context,
                AsyncSnapshot<List<DietPlanRequest>> snapshot) {
              if (snapshot.connectionState == ConnectionState.waiting) {
                return Column(
                  children: [],
                );
              } else {
                return Form(
                    key: _formKey,
                    child: SingleChildScrollView(
                        child: Center(
                            child: Column(children: <Widget>[
                      ConstrainedBox(
                          constraints:
                              BoxConstraints.tight(const Size(200, 50)),
                          child: DropdownButtonFormField(
                            items: snapshot.data!
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
                      ElevatedButton(
                        onPressed: () {
                          if (_formKey.currentState!.validate()) {
                            create();
                          }
                        },
                        child: const Text("Unos"),
                      )
                    ]))));
              }
            }));
  }
}
 */