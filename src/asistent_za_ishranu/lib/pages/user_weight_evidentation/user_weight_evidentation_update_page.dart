import 'package:asistent_za_ishranu/models/user_weight_evidentation_request.dart';
import 'package:asistent_za_ishranu/models/diet_plan_request.dart';
import 'package:asistent_za_ishranu/services/api_service.dart';
import 'package:asistent_za_ishranu/services/auth_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter_datetime_picker/flutter_datetime_picker.dart';
import 'package:intl/intl.dart';

class UserWeightEvidentationUpdatePage extends StatefulWidget {
  const UserWeightEvidentationUpdatePage({Key? key}) : super(key: key);

  static const routeName = '/user_weight_evidentation_update';

  @override
  _UserWeightEvidentationUpdatePageState createState() =>
      _UserWeightEvidentationUpdatePageState();
}

class _UserWeightEvidentationUpdatePageState
    extends State<UserWeightEvidentationUpdatePage> {
  late Future<UserWeightEvidentationRequest> foodProduct;
  final _formKey = GlobalKey<FormState>();
  var id = 0;
  var unitOfMeasureId = 0;

  TextEditingController weight = TextEditingController();
  var firstLoad = true;

  DateTime startDate = DateTime.now();
  int dietPlanId = 0;

  Future<UserWeightEvidentationRequest> getItem(id) async {
    var apiService = ApiService();
    var result = await apiService.get("api/userweightevidention/$id");
    return UserWeightEvidentationRequest.resultFromJson(result);
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
          title: Text("Izmjeni evidenciju kilaže"),
        ),
        body: FutureBuilder<UserWeightEvidentationRequest>(
            future: foodProduct,
            builder: (BuildContext context,
                AsyncSnapshot<UserWeightEvidentationRequest> snapshot) {
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
                  startDate = snapshot.data!.evidentationDate!;
                  weight.text = snapshot.data!.weight.toString();
                  dietPlanId = snapshot.data!.userId!;
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
                            child: TextFormField(
                                decoration:
                                    InputDecoration(labelText: "Kilaža"),
                                controller: weight,
                                validator: (String? value) {
                                  if (value == null ||
                                      value.isEmpty ||
                                      double.tryParse(value) == null) {
                                    return 'Vrijednost je prazna ili nije broj';
                                  }
                                  return null;
                                },
                                keyboardType: TextInputType.numberWithOptions(
                                    decimal: true))),
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
                                    'Datum evidencije',
                                  ),
                                ))),
                        Text("${DateFormat('dd.MM.yyyy').format(startDate)}"),
                        Center(
                          child: ElevatedButton(
                            child: Text("Izmijeni"),
                            onPressed: () async {
                              if (_formKey.currentState!.validate()) {
                                var apiService = ApiService();
                                var dietPlanPeriodRequest =
                                    UserWeightEvidentationRequest(
                                            AuthService().userId,
                                            startDate,
                                            double.parse(weight.text),
                                            id)
                                        .modelToJson();
                                var result = await apiService.put(
                                    "api/userweightevidention/",
                                    dietPlanPeriodRequest);
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
