import 'package:asistent_za_ishranu/models/food_product_request.dart';
import 'package:asistent_za_ishranu/models/notification_rule_request.dart';
import 'package:asistent_za_ishranu/models/notification_rule_user_contact_model.dart';
import 'package:asistent_za_ishranu/models/user_contact_model.dart';
import 'package:asistent_za_ishranu/models/user_model.dart';
import 'package:asistent_za_ishranu/services/auth_service.dart';
import 'package:asistent_za_ishranu/util/wrapped_boolean.dart';
import 'package:asistent_za_ishranu/widgets/checkbox_with_id_meal.dart';
import 'package:asistent_za_ishranu/models/meal_request.dart';
import 'package:asistent_za_ishranu/services/api_service.dart';
import 'package:asistent_za_ishranu/widgets/checkbox_with_id_user_contact.dart';
import 'package:flutter/material.dart';

class NotificationRuleCreatePage extends StatefulWidget {
  const NotificationRuleCreatePage({Key? key}) : super(key: key);

  static const routeName = "/notification_rule_create";

  @override
  _NotificationRuleCreatePageState createState() =>
      _NotificationRuleCreatePageState();
}

class _NotificationRuleCreatePageState
    extends State<NotificationRuleCreatePage> {
  final _formKey = GlobalKey<FormState>();
  var userId = AuthService().userId;
  late Future<UserModel> user;
  List<FoodProductRequest>? foodProducts = [];
  TextEditingController _nameController = TextEditingController();
  List<CheckBoxWithIdUserContact> checkboxes = [];
  int foodProductId = 0;
  bool firstSet = true;

  Future<void> create() async {
    var apiService = ApiService();
    List<NotificationRuleUserContactModel> notificationruleUserContactModels =
        [];
    checkboxes.forEach((element) {
      if (element.wrappedBoolean!.value)
        notificationruleUserContactModels.add(
            NotificationRuleUserContactModel(element.userContactModel!.id, 0));
    });
    var req = NotificationRuleRequest(
            foodProductId, notificationruleUserContactModels)
        .modelToJson();
    await apiService.post("api/notificationrule", req);
    Navigator.of(context).pop(context);
  }

  Future<UserModel> getUser() async {
    var apiService = ApiService();
    var result = await apiService.get("api/user/${userId}");
    foodProducts = await getFoodProducts();
    return UserModel.resultFromJson(result);
  }

  Future<List<FoodProductRequest>> getFoodProducts() async {
    var apiService = ApiService();
    var result = await apiService.get("api/foodproduct?pageSize=1000&index=0");
    return FoodProductRequest.resultListFromJson(result);
  }

  List<CheckBoxWithIdUserContact> populateCheckBoxes(
      List<UserContactModel>? data) {
    if (firstSet) {
      checkboxes = data!.map((e) {
        return CheckBoxWithIdUserContact(e);
      }).toList();
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
      setState(() {});
      user = getUser();
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: Text("Unos Notifikacije"),
        ),
        body: FutureBuilder<UserModel>(
            future: user,
            builder: (BuildContext context, AsyncSnapshot<UserModel> snapshot) {
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
                          )),
                      Column(
                        children:
                            populateCheckBoxes(snapshot!.data!.userContacts),
                      ),
                      ElevatedButton(
                        onPressed: () {
                          if (_formKey.currentState!.validate()) {
                            create();
                          }
                        },
                        child: const Text("Unos"),
                      )
                    ]))));
              }
            }));
  }
}
