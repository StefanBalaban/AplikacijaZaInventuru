import 'package:asistent_za_ishranu/models/diet_plan_meal_model.dart';
import 'package:asistent_za_ishranu/models/food_product_request.dart';
import 'package:asistent_za_ishranu/models/diet_plan_request.dart';
import 'package:asistent_za_ishranu/models/meal_request.dart';
import 'package:asistent_za_ishranu/services/api_service.dart';
import 'package:asistent_za_ishranu/widgets/checkbox_with_id.dart';
import 'package:flutter/material.dart';

class DietPlanUpdatePage extends StatefulWidget {
  const DietPlanUpdatePage({Key? key}) : super(key: key);

  static const routeName = '/diet_plan_update';

  @override
  _DietPlanUpdatePageState createState() => _DietPlanUpdatePageState();
}

class _DietPlanUpdatePageState extends State<DietPlanUpdatePage> {
  final _formKey = GlobalKey<FormState>();
  late Future<DietPlanRequest> dietPlan;
  int id = 0;
  List<MealRequest>? meals;
  TextEditingController _nameController = TextEditingController();
  List<CheckBoxWithId> checkboxes = [];

  Future<DietPlanRequest> getItem(id) async {
    var apiService = ApiService();
    var result = await apiService.get("api/dietplan/$id");
    return DietPlanRequest.resultFromJson(result);
  }

  Future<void> update() async {
    var apiService = ApiService();
    List<DietPlanMealModel> dietPlanMealModels = [];
    checkboxes.forEach((element) {
      if (element.wrappedBoolean!.value)
        dietPlanMealModels.add(DietPlanMealModel(element.mealRequest!.id, 0));
    });
    var req =
        DietPlanRequest(_nameController.text, dietPlanMealModels, id).modelToJson();
    await apiService.put("api/dietplan", req);
    Navigator.of(context).pop(context);
  }

  List<CheckBoxWithId> populateCheckBoxes(
      List<MealRequest>? data, List<DietPlanMealModel>? dietPlanMeals) {
    checkboxes = data!.map((e) => CheckBoxWithId(e)).toList();
    dietPlanMeals!.forEach((element) {
      checkboxes
          .singleWhere((checkbox) => checkbox.mealRequest!.id == element.mealId)
          .wrappedBoolean!
          .value = true;
    });

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
        meals = (ModalRoute.of(context)!.settings.arguments as List<dynamic>)[1]
            as List<MealRequest>;
      });
    dietPlan = getItem(id);
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: Text("Uredi plan ishrane"),
        ),
        body: FutureBuilder<DietPlanRequest>(
            future: dietPlan,
            builder: (BuildContext context,
                AsyncSnapshot<DietPlanRequest> snapshot) {
              if (snapshot.connectionState == ConnectionState.waiting) {
                return Column(
                  children: [],
                );
              } else {
                _nameController.text = snapshot.data!.name!;
                return Form(
                    key: _formKey,
                    child: SingleChildScrollView(
                        child: Center(
                            child: Column(children: <Widget>[
                      ConstrainedBox(
                          constraints:
                              BoxConstraints.tight(const Size(200, 50)),
                          child: TextFormField(
                            validator: (value) {
                              if (value == null || value.isEmpty) {
                                return 'Naziv';
                              }
                            
                            },
                            controller: _nameController,
                            decoration: InputDecoration(hintText: "Naziv"),
                          )),
                      Column(
                        children: populateCheckBoxes(
                            meals, snapshot.data!.dietPlanMeals),
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
