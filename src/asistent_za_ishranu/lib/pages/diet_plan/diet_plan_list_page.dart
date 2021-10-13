import 'package:asistent_za_ishranu/models/diet_plan_request.dart';
//import 'package:asistent_za_ishranu/pages/dietplan/diet_plan_details_page.dart';
import 'package:asistent_za_ishranu/services/api_service.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;

import 'diet_plan_create_page.dart';
import 'diet_plan_details_page.dart';

class DietPlanListPage extends StatefulWidget {
  const DietPlanListPage({Key? key}) : super(key: key);

  static const routeName = 'diet_plan_list';

  @override
  _DietPlanListPageState createState() => _DietPlanListPageState();
}

class _DietPlanListPageState extends State<DietPlanListPage> {
  late Future<List<DietPlanRequest>> dietplans;
  String? name;
  Future<List<DietPlanRequest>> getItems(String? name) async {
    var apiService = ApiService();
    if (name == null) {
      var result =
      await apiService.get("api/dietplan?pageSize=1000&index=0");
      return DietPlanRequest.resultListFromJson(result);
    }
    var result =
    await apiService.get("api/dietplan?pageSize=1000&index=0&name=$name");
    return DietPlanRequest.resultListFromJson(result);
  }



  @override
  Widget build(BuildContext context) {
    List<DietPlanRequest> _dietplans = [
      DietPlanRequest.forListResponse(1, "name")
    ];


    return Scaffold(
        appBar: AppBar(
            title: Text(
              "Planovi ishrane",
              style: TextStyle(fontSize: 15),
            ),
            actions: <Widget>[
              Padding(
                padding: EdgeInsets.only(right: 20.0),
                child: ElevatedButton(
                  child: Text("Nova stavka"),
                  onPressed: () {
                    Navigator.of(context)
                        .pushNamed(DietPlanCreatePage.routeName)
                        .then((value) => setState(() {}));
                  },
                ),
              ),
            ]),
        body: Column(children: [
          TextFormField(
            decoration: InputDecoration(labelText: "Pretraga"),
            onChanged: (input) {
                name = input;
                setState(() {

                });
            },
          ),

          FutureBuilder<List<DietPlanRequest>>(
            future: getItems(name),
            builder: (BuildContext context,
                AsyncSnapshot<List<DietPlanRequest>> snapshot) {
              if (snapshot.connectionState == ConnectionState.waiting) {
                return Column(children: [
                  ListView.builder(
                      itemCount: _dietplans.length,
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
                                child: Text("${snapshot.data![index].name!}")),
                            onTap: () {
                              Navigator.of(context)
                                  .pushNamed(DietPlanDetailsPage.routeName,
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
