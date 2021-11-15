import 'package:asistent_za_ishranu/models/food_product_request.dart';
import 'package:asistent_za_ishranu/models/food_stock_request.dart';
import 'package:asistent_za_ishranu/models/diet_plan_request.dart';
import 'package:asistent_za_ishranu/services/api_service.dart';
import 'package:flutter/material.dart';
import 'package:asistent_za_ishranu/services/auth_service.dart';

import 'food_stock_create_page.dart';
import 'food_stock_details_page.dart';

class FoodStockListPage extends StatefulWidget {
  const FoodStockListPage({Key? key}) : super(key: key);

  static const routeName = 'food_stock_list';

  @override
  _FoodStockListPageState createState() => _FoodStockListPageState();
}

class _FoodStockListPageState extends State<FoodStockListPage> {
  late Future<List<FoodStockRequest>> foodStocks;
  String? name;
  List<FoodProductRequest>? foodProducts;

  Future<List<FoodProductRequest>> getFoodProduct() async {
    var apiService = ApiService();
    var result = await apiService.get("api/foodproduct?pageSize=1000&index=0&userId=${AuthService().userId}");
    return FoodProductRequest.resultListFromJson(result);
  }

  Future<List<FoodStockRequest>> getItems(String? name) async {
    
    var apiService = ApiService();
    var result =
          await apiService.get("api/foodstock?pageSize=1000&index=0&userId=${AuthService().userId}");
    if (name == null) {
      foodProducts = await getFoodProduct();
      return FoodStockRequest.resultListFromJson(result);
    }
    var foodProductsResult = await apiService
        .get("api/foodproduct?pageSize=1000&index=0&userId=${AuthService().userId}&name=$name");

    foodProducts = FoodProductRequest.resultListFromJson(foodProductsResult);
    var foodStockRequests = FoodStockRequest.resultListFromJson(result);
    return foodStockRequests.where((element) => foodProducts!.any((e) => e.id == element.foodProductId)).toList();
  }

  @override
  Widget build(BuildContext context) {
    List<FoodStockRequest> _foodProducts = [
      FoodStockRequest.forListResponse(1, 1)
    ];

    return Scaffold(
        appBar: AppBar(
            title: Text(
              "Periodi plana ishrane",
              style: TextStyle(fontSize: 15),
            ),
            actions: <Widget>[
              Padding(
                padding: EdgeInsets.only(right: 20.0),
                child: ElevatedButton(
                  child: Text("Nova stavka"),
                  onPressed: () {
                    Navigator.of(context)
                        .pushNamed(FoodStockCreatePage.routeName)
                        .then((value) => setState(() {}));
                  },
                ),
              ),
            ]),
        body: Column(children: [
                    TextFormField(
            decoration: InputDecoration(labelText: "Pretraga"),
            onChanged: (input) {
              name = input;
              setState(() {});
            },
          ),
          FutureBuilder<List<FoodStockRequest>>(
            future: getItems(name),
            builder: (BuildContext context,
                AsyncSnapshot<List<FoodStockRequest>> snapshot) {
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
                    child: ListView.builder(
                        itemCount: snapshot.data!.length,
                        scrollDirection: Axis.vertical,
                        shrinkWrap: true,
                        itemBuilder: (BuildContext ctxt, int index) {
                          return ListTile(
                            title: Center(
                                child: Text(
                                    "Zaliha za ${snapshot.data![index].foodProductId != null ? foodProducts!.singleWhere((element) => element.id == snapshot.data![index].foodProductId).name : ""}")),
                            onTap: () {
                              Navigator.of(context)
                                  .pushNamed(
                                      FoodStockDetailsPage.routeName,
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
