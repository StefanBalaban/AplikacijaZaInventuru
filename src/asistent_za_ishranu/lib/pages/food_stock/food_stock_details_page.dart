/* import 'package:asistent_za_ishranu/models/food_stock_request.dart';
import 'package:asistent_za_ishranu/models/diet_plan_request.dart';
import 'package:asistent_za_ishranu/services/api_service.dart';
import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

import 'food_stock_update_page.dart';

class FoodStockDetailsPage extends StatefulWidget {
  const FoodStockDetailsPage({Key? key}) : super(key: key);

  static const routeName = '/food_stock_details';

  @override
  _FoodStockDetailsPageState createState() =>
      _FoodStockDetailsPageState();
}

class _FoodStockDetailsPageState extends State<FoodStockDetailsPage> {
  DietPlanRequest? dietPlan;
  Future<FoodStockRequest> getItem(id) async {
    var apiService = ApiService();
    var result = await apiService.get("api/foodstock/$id");
    var dietPlanPeriodRequest = FoodStockRequest.resultFromJson(result);
    dietPlan = await getDietPlan(dietPlanPeriodRequest.foodProductId!);
    return FoodStockRequest.resultFromJson(result);
  }

  Future<DietPlanRequest> getDietPlan(int id) async {
    var apiService = ApiService();
    var result = await apiService.get("api/dietplan/$id");
    return DietPlanRequest.resultFromJson(result);
  }

  Future<void> deleteItem(id) async {
    var apiService = ApiService();
    await apiService.delete("api/foodstock/$id");
  }

  @override
  Widget build(BuildContext context) {
    final id = ModalRoute.of(context)!.settings.arguments as int;
    return Scaffold(
        appBar: AppBar(
          title: Text("Detalji perioda plana ishrane"),
        ),
        body: FutureBuilder<FoodStockRequest>(
            future: getItem(id),
            builder: (BuildContext context,
                AsyncSnapshot<FoodStockRequest> snapshot) {
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
                      initialValue: dietPlan?.name,
                      decoration: InputDecoration(labelText: "Plan ishrane"),
                      readOnly: true,
                    ),
                    TextFormField(
                      initialValue:
                          "${DateFormat('dd.MM.yyyy').format(snapshot.data!.bestUseByDate!)}",
                      decoration: InputDecoration(labelText: "Naziv"),
                      readOnly: true,
                    ),
                    TextFormField(
                      initialValue:
                          "${DateFormat('dd.MM.yyyy').format(snapshot.data!.dateOfPurchase!)}",
                      decoration: InputDecoration(labelText: "Jedinica mjere"),
                      readOnly: true,
                    ),
                    Center(
                      child: ElevatedButton(
                        child: Text("Izmijeni"),
                        onPressed: () {
                          Navigator.of(context)
                              .pushNamed(FoodStockUpdatePage.routeName,
                                  arguments: id)
                              .then((value) => setState(() {}));
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
                            content: const Text(
                                'Da li ste sigurni da želite obrisati stavku?'),
                            actions: <Widget>[
                              TextButton(
                                onPressed: () => Navigator.pop(context, 'Ne'),
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
 */