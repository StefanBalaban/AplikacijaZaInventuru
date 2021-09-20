import 'package:asistent_za_ishranu/models/food_product_request.dart';
import 'package:asistent_za_ishranu/pages/food_product/food_product_details_page.dart';
import 'package:asistent_za_ishranu/services/api_service.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;

import 'food_product_create_page.dart';

class FoodProductListPage extends StatefulWidget {
  const FoodProductListPage({Key? key}) : super(key: key);

  @override
  _FoodProductListPageState createState() => _FoodProductListPageState();
}

class _FoodProductListPageState extends State<FoodProductListPage> {
  late Future<List<FoodProductRequest>> foodProducts;
  String? name;
  Future<List<FoodProductRequest>> getItems(String? name) async {
    var apiService = ApiService();
    if (name == null) {
      var result =
      await apiService.get("api/foodproduct?pageSize=1000&index=0");
      return FoodProductRequest.resultListFromJson(result);
    }
    var result =
    await apiService.get("api/foodproduct?pageSize=1000&index=0&name=$name");
    return FoodProductRequest.resultListFromJson(result);
  }



  @override
  Widget build(BuildContext context) {
    List<FoodProductRequest> _foodProducts = [
      FoodProductRequest.forListResponse(1, "name")
    ];


    return Scaffold(
        appBar: AppBar(
            title: Text(
              "Prehrambeni proizvodi",
              style: TextStyle(fontSize: 15),
            ),
            actions: <Widget>[
              Padding(
                padding: EdgeInsets.only(right: 20.0),
                child: ElevatedButton(
                  child: Text("Nova stavka"),
                  onPressed: () {
                    Navigator.of(context)
                        .pushNamed(FoodProductCreatePage.routeName)
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
                setState(() {

                });
            },
          ),

          FutureBuilder<List<FoodProductRequest>>(
            future: getItems(name),
            builder: (BuildContext context,
                AsyncSnapshot<List<FoodProductRequest>> snapshot) {
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
                    //TODO: Add Expanded here
                    child: ListView.builder(
                        itemCount: snapshot.data!.length,
                        scrollDirection: Axis.vertical,
                        shrinkWrap: true,
                        itemBuilder: (BuildContext ctxt, int index) {
                          return ListTile(
                            title: Center(
                                child: Text("${snapshot.data![index].name!}")),
                            onTap: () {
                              Navigator.of(context)
                                  .pushNamed(FoodProductDetailsPage.routeName,
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
