import 'package:asistent_za_ishranu/models/food_product_request.dart';
import 'package:asistent_za_ishranu/models/notification_rule_request.dart';
import 'package:asistent_za_ishranu/models/meal_request.dart';
import 'package:asistent_za_ishranu/models/notification_rule_user_contact_model.dart';
import 'package:asistent_za_ishranu/models/user_contact_model.dart';
import 'package:asistent_za_ishranu/models/user_model.dart';
import 'package:asistent_za_ishranu/services/api_service.dart';
import 'package:asistent_za_ishranu/services/auth_service.dart';
import 'package:asistent_za_ishranu/widgets/checkbox_with_id_user_contact.dart';
import 'package:flutter/material.dart';

class NotificationRuleUpdatePage extends StatefulWidget {
  const NotificationRuleUpdatePage({Key? key}) : super(key: key);

  static const routeName = '/notification_rule_update';

  @override
  _NotificationRuleUpdatePageState createState() =>
      _NotificationRuleUpdatePageState();
}

class _NotificationRuleUpdatePageState
    extends State<NotificationRuleUpdatePage> {
  final _formKey = GlobalKey<FormState>();
  late Future<NotificationRuleRequest> dietPlan;
  List<FoodProductRequest>? foodProducts = [];
  int id = 0;
  int foodProductId = 0;
  List<CheckBoxWithIdUserContact> checkboxes = [];
  UserModel? user;
  bool firstSet = true;

  Future<NotificationRuleRequest> getItem(id) async {
    foodProducts = FoodProductRequest.resultListFromJson(
        await ApiService().get("api/foodproduct?pageSize=1000&index=0"));
    user = UserModel.resultFromJson(
        await ApiService().get("api/user/${AuthService().userId}"));
    return NotificationRuleRequest.resultFromJson(
        await ApiService().get("api/notificationrule/$id"));
  }

  Future<void> update() async {
    var apiService = ApiService();
    List<NotificationRuleUserContactModel> dietPlanMealModels = [];
    checkboxes.forEach((element) {
      if (element.wrappedBoolean!.value)
        dietPlanMealModels.add(
            NotificationRuleUserContactModel(element.userContactModel!.id, 0));
    });
    var req = NotificationRuleRequest(foodProductId, dietPlanMealModels, id)
        .modelToJson();
    await apiService.put("api/notificationrule", req);
    Navigator.of(context).pop(context);
  }

  List<CheckBoxWithIdUserContact> populateCheckBoxes(
      List<UserContactModel>? data,
      List<NotificationRuleUserContactModel>? notificationRuleUserContacts) {
    if (firstSet) {
      checkboxes = data!.map((e) {
        return CheckBoxWithIdUserContact(e);
      }).toList();
      notificationRuleUserContacts!.forEach((element) {
        checkboxes
            .singleWhere((checkbox) =>
                checkbox.userContactModel!.id == element.userContactInfosId)
            .wrappedBoolean!
            .value = true;
      });
      firstSet = false;
    }

    return checkboxes;
  }

  @override
  void initState() {
    super.initState();
    // future that allows us to access context. function is called inside the future
    // otherwise it would be skipped and args would return null
    Future.delayed(Duration.zero, () {
      setState(() {
        id = (ModalRoute.of(context)!.settings.arguments as List<dynamic>)[0]
            as int;
      });
      dietPlan = getItem(id);
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: Text("Uredi notifikacijsko pravilo"),
        ),
        body: FutureBuilder<NotificationRuleRequest>(
            future: dietPlan,
            builder: (BuildContext context,
                AsyncSnapshot<NotificationRuleRequest> snapshot) {
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
                            items: foodProducts!
                                .map((e) => DropdownMenuItem(
                                      child: Text(e.name!),
                                      value: e.id,
                                    ))
                                .toList(),
                            hint: Text('Prehrambeni proizvod'),
                            onChanged: (value) {
                              setState(() {
                                foodProductId = value! as int;
                              });
                            },
                            validator: (int? value) {
                              if (value == null || value == 0) {
                                return 'Odaberite prehambeni proizvod';
                              }
                              return null;
                            },
                            value: snapshot.data!.foodProductId,
                          )),
                      Column(
                        children: populateCheckBoxes(user!.userContacts,
                            snapshot.data!.notificationRuleUserContacts),
                      ),
                      ElevatedButton(
                        onPressed: () {
                          if (_formKey.currentState!.validate()) {
                            update();
                          }
                        },
                        child: const Text("Unos"),
                      )
                    ]))));
              }
            }));
  }
}
