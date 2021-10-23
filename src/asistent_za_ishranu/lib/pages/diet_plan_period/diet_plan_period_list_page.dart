import 'package:asistent_za_ishranu/models/diet_plan_period_request.dart';
import 'package:asistent_za_ishranu/models/diet_plan_request.dart';
import 'package:asistent_za_ishranu/pages/diet_plan_period/diet_plan_period_details_page.dart';
import 'package:asistent_za_ishranu/services/api_service.dart';
import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

import 'diet_plan_period_create_page.dart';

class DietPlanPeriodListPage extends StatefulWidget {
  const DietPlanPeriodListPage({Key? key}) : super(key: key);

  static const routeName = 'diet_plan_period_list';

  @override
  _DietPlanPeriodListPageState createState() => _DietPlanPeriodListPageState();
}

class _DietPlanPeriodListPageState extends State<DietPlanPeriodListPage> {
  late Future<List<DietPlanPeriodRequest>> foodProducts;
  String? name;
  List<DietPlanRequest>? dietPlans;

  Future<List<DietPlanRequest>> getDietPlans() async {
    var apiService = ApiService();
    var result = await apiService.get("api/dietplan?pageSize=1000&index=0");
    return DietPlanRequest.resultListFromJson(result);
  }

  Future<List<DietPlanPeriodRequest>> getItems(String? name) async {
    var apiService = ApiService();
    if (name == null) {
      var result =
          await apiService.get("api/dietplanperiod?pageSize=1000&index=0");
      dietPlans = await getDietPlans();
      return DietPlanPeriodRequest.resultListFromJson(result);
    }
    var result = await apiService
        .get("api/dietplanperiod?pageSize=1000&index=0&name=$name");

    dietPlans = await getDietPlans();
    return DietPlanPeriodRequest.resultListFromJson(result);
  }

  @override
  Widget build(BuildContext context) {
    List<DietPlanPeriodRequest> _foodProducts = [
      DietPlanPeriodRequest.forListResponse(1, DateTime.now())
    ];

    return Scaffold(
        appBar: AppBar(
            title: Text(
              "Period plana ishrane",
              style: TextStyle(fontSize: 15),
            ),
            actions: <Widget>[
              Padding(
                padding: EdgeInsets.only(right: 20.0),
                child: ElevatedButton(
                  child: Text("Nova stavka"),
                  onPressed: () {
                    Navigator.of(context)
                        .pushNamed(DietPlanPeriodCreatePage.routeName)
                        .then((value) => setState(() {}));
                  },
                ),
              ),
            ]),
        body: Column(children: [
          
          FutureBuilder<List<DietPlanPeriodRequest>>(
            future: getItems(name),
            builder: (BuildContext context,
                AsyncSnapshot<List<DietPlanPeriodRequest>> snapshot) {
              if (snapshot.connectionState == ConnectionState.waiting) {
                return Column(children: [
                  ListView.builder(
                      itemCount: _foodProducts.length,
                      scrollDirection: Axis.vertical,
                      shrinkWrap: true,
                      itemBuilder: (BuildContext ctxt, int index) {
                        return ListTile(
                          title: Text(""),
                          onTap: () {},
                        );
                      })
                ]);
              } else {
                return Expanded(
                    //TODO: Add Expanded here
                    child: ListView.builder(
                        itemCount: snapshot.data!.length,
                        scrollDirection: Axis.vertical,
                        shrinkWrap: true,
                        itemBuilder: (BuildContext ctxt, int index) {
                          return ListTile(
                            title: Center(
                                child: Text(
                                    "${snapshot.data![index].dietPlanId != null ? dietPlans!.singleWhere((element) => element.id == snapshot.data![index].dietPlanId).name : ""}  ${DateFormat("dd.MM.yyyy.").format(snapshot.data![index].startDate!)}-${DateFormat("dd.MM.yyyy.").format(snapshot.data![index].endDate!)}")),
                            onTap: () {
                              Navigator.of(context)
                                  .pushNamed(
                                      DietPlanPeriodDetailsPage.routeName,
                                      arguments: snapshot.data![index].id)
                                  .then((value) => setState(() {}));
                            },
                          );
                        }));
              }
            },
          )
        ]));
  }
}
