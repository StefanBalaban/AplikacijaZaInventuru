import 'package:asistent_za_ishranu/models/diet_plan_period_request.dart';
import 'package:asistent_za_ishranu/models/diet_plan_request.dart';
import 'package:asistent_za_ishranu/services/api_service.dart';
import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

import 'diet_plan_period_update_page.dart';

class DietPlanPeriodDetailsPage extends StatefulWidget {
  const DietPlanPeriodDetailsPage({Key? key}) : super(key: key);

  static const routeName = '/diet_plan_period_details';

  @override
  _DietPlanPeriodDetailsPageState createState() => _DietPlanPeriodDetailsPageState();
}

class _DietPlanPeriodDetailsPageState extends State<DietPlanPeriodDetailsPage> {  
  DietPlanRequest? dietPlan;
  Future<DietPlanPeriodRequest> getItem(id) async {
    var apiService = ApiService();
    var result = await apiService.get("api/dietplanperiod/$id");    
    var dietPlanPeriodRequest = DietPlanPeriodRequest.resultFromJson(result);
    dietPlan = await getDietPlan(dietPlanPeriodRequest.dietPlanId!);
    return DietPlanPeriodRequest.resultFromJson(result);
  }

    Future<DietPlanRequest> getDietPlan(int id) async {
    var apiService = ApiService();
    var result = await apiService.get("api/dietplan/$id");
    return DietPlanRequest.resultFromJson(result);
  }

  Future<void> deleteItem(id) async {
    var apiService = ApiService();
    await apiService.delete("api/dietplanperiod/$id");
  }

  @override
  Widget build(BuildContext context) {
    final id = ModalRoute.of(context)!.settings.arguments as int;
    return Scaffold(
        appBar: AppBar(
          title: Text("Detalji perioda plana ishrane"),
        ),
        body: FutureBuilder<DietPlanPeriodRequest>(
            future: getItem(id),
            builder: (BuildContext context,
                AsyncSnapshot<DietPlanPeriodRequest> snapshot) {
              if (snapshot.connectionState == ConnectionState.waiting) {
                return Form(
                    child: Column(
                  children: [],
                ));
              } else {
                return SingleChildScrollView(
                  child: Form(
                    child: Column(
                  children: [                    
                    TextFormField(
                      initialValue:  dietPlan?.name,
                      decoration: InputDecoration(labelText: "Plan ishrane"),
                      readOnly: true,
                    ),
                    TextFormField(
                      initialValue: "${DateFormat('dd.MM.yyyy').format(snapshot.data!.startDate!)}",
                      decoration: InputDecoration(labelText: "Naziv"),
                      readOnly: true,
                    ),
                    TextFormField(
                      initialValue:  "${DateFormat('dd.MM.yyyy').format(snapshot.data!.endDate!)}",
                      decoration: InputDecoration(labelText: "Jedinica mjere"),
                      readOnly: true,
                    ),
                    Center(
                      child: ElevatedButton(
                        child: Text("Izmijeni"),
                        onPressed: () {
                          Navigator.of(context).pushNamed(
                              DietPlanPeriodUpdatePage.routeName,
                              arguments: id).then((value) => setState((){}));
                        },
                      ),
                    ),
                    Center(
                      child: ElevatedButton(
                        child: Text("Izbriši"),
                        onPressed: () => showDialog<String>(
                          context: context,
                          builder: (BuildContext context) => AlertDialog(
                            title: const Text('Potvrda brisanja'),
                            content: const Text('Da li ste sigurni da želite obrisati stavku?'),
                            actions: <Widget>[
                              TextButton(
                                onPressed: () =>
                                    Navigator.pop(context, 'Ne'),
                                child: const Text('Ne'),
                              ),
                              TextButton(
                                onPressed: () async {
                                  await deleteItem(id);
                                  Navigator.pop(context, 'Da');
                                  Navigator.pop(context);
                                },
                                child: const Text('Da'),
                              ),
                            ],
                          ),
                        ),
                      ),
                    )
                  ],
                )));
              }
            }));
  }
}
