import 'package:asistent_za_ishranu/models/food_product_request.dart';
import 'package:asistent_za_ishranu/models/meal_item_model.dart';
import 'package:asistent_za_ishranu/models/meal_request.dart';
import 'package:asistent_za_ishranu/pages/meal/meal_update_page.dart';
import 'package:asistent_za_ishranu/services/api_service.dart';
import 'package:asistent_za_ishranu/services/auth_service.dart';
import 'package:flutter/material.dart';

class MealDetailsPage extends StatefulWidget {
  const MealDetailsPage({Key? key}) : super(key: key);

  static const routeName = '/meal_details';

  @override
  _MealDetailsPageState createState() => _MealDetailsPageState();
}

class _MealDetailsPageState extends State<MealDetailsPage> {

  List<FoodProductRequest> foodProducts = [];
  Future<MealRequest> getItem(id) async {
    var apiService = ApiService();
    var result = await apiService.get("api/meal/$id");

    apiService = ApiService();
    var foodProductResult = await apiService.get("api/foodproduct?pageSize=1000&index=0&userId=${AuthService().userId}");
    foodProducts = FoodProductRequest.resultListFromJson(foodProductResult);

    return MealRequest.resultFromJson(result);
  }

  Future<void> deleteItem(id) async {
    var apiService = ApiService();
    await apiService.delete("api/meal/$id");
  }

  List<Widget> getListOfMealItems(List<MealItemModel>? mealRequests) {
    List<Widget> fields = [];

    fields = mealRequests!.toList().map((e) {
      return
        TextFormField(
          initialValue: "${e.amount}",
          decoration: InputDecoration(labelText: "${foodProducts.firstWhere((element) => element.id == e.foodProductId).name} količina:"),
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
        body: FutureBuilder<MealRequest>(
            future: getItem(id),
            builder: (BuildContext context,
                AsyncSnapshot<MealRequest> snapshot) {
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
                        children: getListOfMealItems(snapshot!.data!.meals)),
                    Center(
                      child: ElevatedButton(
                        child: Text("Izmijeni"),
                        onPressed: () {
                          Navigator.of(context).pushNamed(
                              MealUpdatePage.routeName,
                              arguments: [id, foodProducts]).then((value) => setState((){}));
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
