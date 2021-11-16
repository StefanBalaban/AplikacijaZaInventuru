import 'package:asistent_za_ishranu/models/meal_request.dart';
import 'package:asistent_za_ishranu/pages/meal/meal_details_page.dart';
import 'package:asistent_za_ishranu/services/api_service.dart';
import 'package:flutter/material.dart';
import 'package:asistent_za_ishranu/services/auth_service.dart';
import 'package:http/http.dart' as http;

import 'meal_create_page.dart';

class MealListPage extends StatefulWidget {
  const MealListPage({Key? key}) : super(key: key);

  static const routeName = 'meal_list';

  @override
  _MealListPageState createState() => _MealListPageState();
}

class _MealListPageState extends State<MealListPage> {
  late Future<List<MealRequest>> meals;
  String? name;
  Future<List<MealRequest>> getItems(String? name) async {
    var apiService = ApiService();
    if (name == null) {
      var result =
      await apiService.get("api/meal?pageSize=1000&index=0&userId=${AuthService().userId}");
      return MealRequest.resultListFromJson(result);
    }
    var result =
    await apiService.get("api/meal?pageSize=1000&index=0&userId=${AuthService().userId}&name=$name");
    return MealRequest.resultListFromJson(result);
  }



  @override
  Widget build(BuildContext context) {
    List<MealRequest> _meals = [
      MealRequest.forListResponse(1, "name")
    ];


    return Scaffold(
        appBar: AppBar(
            title: Text(
              "Obroci",
              style: TextStyle(fontSize: 15),
            ),
            actions: <Widget>[
              Padding(
                padding: EdgeInsets.only(right: 20.0),
                child: ElevatedButton(
                  child: Text("Nova stavka"),
                  onPressed: () {
                    Navigator.of(context)
                        .pushNamed(MealCreatePage.routeName)
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

          FutureBuilder<List<MealRequest>>(
            future: getItems(name),
            builder: (BuildContext context,
                AsyncSnapshot<List<MealRequest>> snapshot) {
              if (snapshot.connectionState == ConnectionState.waiting) {
                return Column(children: [
                  ListView.builder(
                      itemCount: _meals.length,
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
                                  .pushNamed(MealDetailsPage.routeName,
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
