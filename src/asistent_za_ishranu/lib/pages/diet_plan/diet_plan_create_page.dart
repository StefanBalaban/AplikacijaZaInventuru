import 'package:asistent_za_ishranu/models/diet_plan_meal_model.dart';
import 'package:asistent_za_ishranu/widgets/checkbox_with_id.dart';
import 'package:asistent_za_ishranu/models/food_product_request.dart';
import 'package:asistent_za_ishranu/models/diet_plan_request.dart';
import 'package:asistent_za_ishranu/models/meal_request.dart';
import 'package:asistent_za_ishranu/services/api_service.dart';
import 'package:flutter/material.dart';

class DietPlanCreatePage extends StatefulWidget {
  const DietPlanCreatePage({Key? key}) : super(key: key);

  static const routeName = "/diet_plan_create";

  @override
  _DietPlanCreatePageState createState() => _DietPlanCreatePageState();
}

class _DietPlanCreatePageState extends State<DietPlanCreatePage> {
  final _formKey = GlobalKey<FormState>();
  late Future<List<MealRequest>> meals;
  TextEditingController _nameController = TextEditingController();
  List<CheckBoxWithId> checkboxes = [];

  Future<void> create() async {    
      var apiService = ApiService();
      List<DietPlanMealModel> dietPlanMealModels = [];
      checkboxes.forEach((element) {if (element.wrappedBoolean!.value) dietPlanMealModels.add(DietPlanMealModel(element.mealRequest!.id, 0));});
      var req = DietPlanRequest(_nameController.text, dietPlanMealModels
      ).modelToJson();
      await apiService.post("api/dietplan", req);
      Navigator.of(context).pop(context);
  }

  Future<List<MealRequest>> getMeals() async {
    var apiService = ApiService();
    var result = await apiService.get("api/meal?pageSize=1000&index=0");
    return MealRequest.resultListFromJson(result);
  }

  List<CheckBoxWithId> populateCheckBoxes(List<MealRequest>? data) {
    checkboxes = data!.map((e) => CheckBoxWithId(e)).toList();
    
    return checkboxes;
  }

  @override
  void initState() {
    super.initState();
    // future that allows us to access context. function is called inside the future
    // otherwise it would be skipped and args would return null
    Future.delayed(Duration.zero, () {
      setState(() {});
      meals = getMeals();
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: Text("Unos plana ishrane"),
        ),
        body: FutureBuilder<List<MealRequest>>(
            future: meals,
            builder: (BuildContext context,
                AsyncSnapshot<List<MealRequest>> snapshot) {
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
                        children: populateCheckBoxes(snapshot!.data),
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
