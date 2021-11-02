import 'package:asistent_za_ishranu/models/user_weight_evidentation_request.dart';
import 'package:asistent_za_ishranu/models/diet_plan_request.dart';
import 'package:asistent_za_ishranu/services/api_service.dart';
import 'package:asistent_za_ishranu/services/auth_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter_datetime_picker/flutter_datetime_picker.dart';
import 'package:intl/intl.dart';

class UserWeightEvidentationCreatePage extends StatefulWidget {
  const UserWeightEvidentationCreatePage({Key? key}) : super(key: key);

  static const routeName = "/user_weight_evidentation_create";

  @override
  _UserWeightEvidentationCreatePageState createState() =>
      _UserWeightEvidentationCreatePageState();
}

class _UserWeightEvidentationCreatePageState
    extends State<UserWeightEvidentationCreatePage> {
  final _formKey = GlobalKey<FormState>();
  DateTime evidentionDate = DateTime.now();
  int dietPlanId = 0;
  TextEditingController weight = TextEditingController();
  late Future<List<DietPlanRequest>> dietPlans;

  Future<void> create() async {
    {
      var apiService = ApiService();
      await apiService.post(
          "api/userweightevidention",
          UserWeightEvidentationRequest(AuthService().userId, evidentionDate,
                  double.parse(weight.text))
              .modelToJson());

      Navigator.of(context).pop(context);
    }
  }

  @override
  void initState() {
    super.initState();
    // future that allows us to access context. function is called inside the future
    // otherwise it would be skipped and args would return null
    Future.delayed(Duration.zero, () {
      setState(() {});
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: Text("Unos evidencije kilaže"),
        ),
        body: Form(
            key: _formKey,
            child: SingleChildScrollView(
                child: Center(
                    child: Column(children: <Widget>[
              ConstrainedBox(
                  constraints: BoxConstraints.tight(const Size(200, 50)),
                  child: TextFormField(
                      decoration: InputDecoration(labelText: "Kilaža"),
                      controller: weight,
                      validator: (String? value) {
                        if (value == null ||
                            value.isEmpty ||
                            double.tryParse(value) == null) {
                          return 'Vrijednost je prazna ili nije broj';
                        }
                        return null;
                      },
                      keyboardType:
                          TextInputType.numberWithOptions(decimal: true))),
              ConstrainedBox(
                  constraints: BoxConstraints.tight(const Size(200, 50)),
                  child: Padding(
                      padding: EdgeInsets.all(10),
                      child: ElevatedButton(
                        onPressed: () {
                          DatePicker.showDatePicker(context,
                              showTitleActions: true, onConfirm: (date) {
                            setState(() {
                              evidentionDate = date;
                            });
                          }, currentTime: evidentionDate);
                        },
                        child: Text(
                          'Datum evidencije',
                        ),
                      ))),
              Text("${DateFormat('dd.MM.yyyy').format(evidentionDate)}"),
              ElevatedButton(
                onPressed: () {
                  if (_formKey.currentState!.validate()) {
                    create();
                  }
                },
                child: const Text("Unos"),
              )
            ])))));
  }
}
