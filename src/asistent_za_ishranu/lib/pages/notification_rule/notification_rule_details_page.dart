import 'package:asistent_za_ishranu/models/food_product_request.dart';
import 'package:asistent_za_ishranu/models/notification_rule_request.dart';
import 'package:asistent_za_ishranu/models/meal_request.dart';
import 'package:asistent_za_ishranu/models/notification_rule_user_contact_model.dart';
import 'package:asistent_za_ishranu/models/user_model.dart';
import 'package:asistent_za_ishranu/services/api_service.dart';
import 'package:asistent_za_ishranu/services/auth_service.dart';
import 'package:flutter/material.dart';

import 'notification_rule_update_page.dart';

class NotificationRuleDetailsPage extends StatefulWidget {
  const NotificationRuleDetailsPage({Key? key}) : super(key: key);

  static const routeName = '/notification_rule_details';

  @override
  _NotificationRuleDetailsPageState createState() => _NotificationRuleDetailsPageState();
}

class _NotificationRuleDetailsPageState extends State<NotificationRuleDetailsPage> {
  FoodProductRequest? foodProductRequest;
  UserModel? user; 
  Future<NotificationRuleRequest> getItem(id) async {
    var apiService = ApiService();
    var notificationRule = NotificationRuleRequest.resultFromJson(await apiService.get("api/notificationrule/$id"));
    
    user = UserModel.resultFromJson(await apiService.get("api/user/${AuthService().userId}"));
    foodProductRequest = FoodProductRequest.resultFromJson(await apiService.get("api/foodproduct/${notificationRule.foodProductId}"));
    return notificationRule;
  }

  Future<void> deleteItem(id) async {
    var apiService = ApiService();
    await apiService.delete("api/notificationrule/$id");
  }

  List<Widget> getListOfNotificationContactsRuleItems(List<NotificationRuleUserContactModel> notificationruleRequests) {
    List<Widget> fields = notificationruleRequests!.toList().map((e) {
      return
        TextFormField(
          initialValue: "${user!.userContacts!.singleWhere((element) => element.id == e.userContactInfosId).contact}",
          decoration: InputDecoration(labelText: "Kontakt:"),
          readOnly: true,

        );}).toList();

    return fields;
  }

  @override
  Widget build(BuildContext context) {
    final id = ModalRoute.of(context)!.settings.arguments as int;
    return Scaffold(
        appBar: AppBar(
          title: Text("Detalji notifikacijskog pravila "),
        ),
        body: FutureBuilder<NotificationRuleRequest>(
            future: getItem(id),
            builder: (BuildContext context,
                AsyncSnapshot<NotificationRuleRequest> snapshot) {
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
                      initialValue: foodProductRequest != null ? foodProductRequest!.name : "",
                      decoration: InputDecoration(labelText: "Prehrambeni proizvod"),
                      readOnly: true,
                    ),
                    Column(
                        children: getListOfNotificationContactsRuleItems(snapshot!.data!.notificationRuleUserContacts!)),
                    Center(
                      child: ElevatedButton(
                        child: Text("Izmijeni"),
                        onPressed: () {
                          Navigator.of(context).pushNamed(
                              NotificationRuleUpdatePage.routeName,
                              arguments: [id]).then((value) => setState((){}));
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
