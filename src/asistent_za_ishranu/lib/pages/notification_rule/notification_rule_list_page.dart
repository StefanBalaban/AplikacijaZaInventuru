import 'package:asistent_za_ishranu/models/food_product_request.dart';
import 'package:asistent_za_ishranu/models/notification_rule_request.dart';
import 'package:asistent_za_ishranu/services/api_service.dart';
import 'package:flutter/material.dart';

import 'notification_rule_create_page.dart';
import 'notification_rule_details_page.dart';

class NotificationRuleListPage extends StatefulWidget {
  const NotificationRuleListPage({Key? key}) : super(key: key);

  static const routeName = 'notification_rule_list';

  @override
  _NotificationRuleListPageState createState() =>
      _NotificationRuleListPageState();
}

class _NotificationRuleListPageState extends State<NotificationRuleListPage> {
  late Future<List<NotificationRuleRequest>> notificationrules;
  List<FoodProductRequest> foodProducts = [];
  String? name;
  Future<List<NotificationRuleRequest>> getItems(String? name) async {
    var apiService = ApiService();
    foodProducts = FoodProductRequest.resultListFromJson(
        await apiService.get("api/foodproduct?pageSize=1000&index=0"));
    return NotificationRuleRequest.resultListFromJson(
        await apiService.get("api/notificationrule?pageSize=1000&index=0"));
  }

  @override
  Widget build(BuildContext context) {
    List<NotificationRuleRequest> _notificationrules = [
      NotificationRuleRequest.forListResponse(1, 1)
    ];

    return Scaffold(
        appBar: AppBar(
            title: Text(
              "Notifikacijska pravila",
              style: TextStyle(fontSize: 15),
            ),
            actions: <Widget>[
              Padding(
                padding: EdgeInsets.only(right: 20.0),
                child: ElevatedButton(
                  child: Text("Nova stavka"),
                  onPressed: () {
                    Navigator.of(context)
                        .pushNamed(NotificationRuleCreatePage.routeName)
                        .then((value) => setState(() {}));
                  },
                ),
              ),
            ]),
        body: Column(children: [
          FutureBuilder<List<NotificationRuleRequest>>(
            future: getItems(name),
            builder: (BuildContext context,
                AsyncSnapshot<List<NotificationRuleRequest>> snapshot) {
              if (snapshot.connectionState == ConnectionState.waiting) {
                return Column(children: [
                  ListView.builder(
                      itemCount: _notificationrules.length,
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
                                    "${foodProducts.any((element) => element.id == snapshot.data![index].foodProductId!) ? foodProducts.singleWhere((element) => element.id == snapshot.data![index].foodProductId!).name : ""}")),
                            onTap: () {
                              Navigator.of(context)
                                  .pushNamed(
                                      NotificationRuleDetailsPage.routeName,
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
