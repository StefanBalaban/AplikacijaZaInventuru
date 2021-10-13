import 'package:asistent_za_ishranu/models/diet_plan_meal_model.dart';
import 'package:asistent_za_ishranu/models/food_product_request.dart';
import 'package:asistent_za_ishranu/models/diet_plan_request.dart';
import 'package:asistent_za_ishranu/models/meal_request.dart';
import 'package:asistent_za_ishranu/services/api_service.dart';
import 'package:flutter/material.dart';

class DietPlanDetailsPage extends StatefulWidget {
  const DietPlanDetailsPage({Key? key}) : super(key: key);

  static const routeName = '/diet_plan_details';

  @override
  _DietPlanDetailsPageState createState() => _DietPlanDetailsPageState();
}

class _DietPlanDetailsPageState extends State<DietPlanDetailsPage> {

  List<MealRequest> meals = []; 
  Future<DietPlanRequest> getItem(id) async {
    var apiService = ApiService();
    var result = await apiService.get("api/dietplan/$id");

    apiService = ApiService();
    var mealResult = await apiService.get("api/meal?pageSize=1000&index=0");
    meals = MealRequest.resultListFromJson(mealResult);

    return DietPlanRequest.resultFromJson(result);
  }

  Future<void> deleteItem(id) async {
    var apiService = ApiService();
    await apiService.delete("api/dietplan/$id");
  }

  List<Widget> getListOfDietPlanItems(List<DietPlanMealModel> dietplanRequests) {
    List<Widget> fields = dietplanRequests!.toList().map((e) {
      return
        TextFormField(
          initialValue: "${meals.singleWhere((element) => element.id == e.mealId)}",
          decoration: InputDecoration(labelText: "Obrok:"),
          readOnly: true,

        );}).toList();

    return fields;
  }

  @override
  Widget build(BuildContext context) {
    final id = ModalRoute.of(context)!.settings.arguments as int;
    return Scaffold(
        appBar: AppBar(
          title: Text("Detalji obroka"),
        ),
        body: FutureBuilder<DietPlanRequest>(
            future: getItem(id),
            builder: (BuildContext context,
                AsyncSnapshot<DietPlanRequest> snapshot) {
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
                      initialValue: snapshot.data!.name,
                      decoration: InputDecoration(labelText: "Naziv"),
                      readOnly: true,
                    ),
                    Column(
                        children: getListOfDietPlanItems(snapshot!.data!.dietPlanMeals!)),
                    Center(
                      child: ElevatedButton(
                        child: Text("Izmijeni"),
                        onPressed: () {
                          Navigator.of(context).pushNamed(
                              "DietPlanUpdatePage.routeName",
                              arguments: [id, meals]).then((value) => setState((){}));
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
