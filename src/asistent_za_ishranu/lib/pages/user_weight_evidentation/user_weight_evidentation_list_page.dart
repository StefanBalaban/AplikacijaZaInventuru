import 'package:asistent_za_ishranu/models/user_weight_evidentation_request.dart';
import 'package:asistent_za_ishranu/models/diet_plan_request.dart';
import 'package:asistent_za_ishranu/pages/user_weight_evidentation/user_weight_evidentation_details_page.dart';
import 'package:asistent_za_ishranu/services/api_service.dart';
import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

import 'user_weight_evidentation_create_page.dart';

class UserWeightEvidentationListPage extends StatefulWidget {
  const UserWeightEvidentationListPage({Key? key}) : super(key: key);

  static const routeName = 'user_weight_evidentation_list';

  @override
  _UserWeightEvidentationListPageState createState() =>
      _UserWeightEvidentationListPageState();
}

class _UserWeightEvidentationListPageState
    extends State<UserWeightEvidentationListPage> {

  Future<List<UserWeightEvidentationRequest>> getItems() async {
        var result = UserWeightEvidentationRequest.resultListFromJson(await ApiService()
        .get("api/userweightevidention?pageSize=1000&index=0"));
    return result;
  }

  @override
  Widget build(BuildContext context) {
    List<UserWeightEvidentationRequest> _foodProducts = [
      UserWeightEvidentationRequest.forListResponse(1, DateTime.now())
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
                        .pushNamed(UserWeightEvidentationCreatePage.routeName)
                        .then((value) => setState(() {}));
                  },
                ),
              ),
            ]),
        body: Column(children: [
          FutureBuilder<List<UserWeightEvidentationRequest>>(
            future: getItems(),
            builder: (BuildContext context,
                AsyncSnapshot<List<UserWeightEvidentationRequest>> snapshot) {
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
                                    "${DateFormat("dd.MM.yyyy.").format(snapshot.data![index].evidentationDate!)}")),
                            onTap: () {
                              Navigator.of(context)
                                  .pushNamed(
                                      UserWeightEvidentationDetailsPage.routeName,
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
