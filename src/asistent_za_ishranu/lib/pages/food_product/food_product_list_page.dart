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
  @override
  Widget build(BuildContext context) {
    List<FoodProductRequest> _foodProducts = [
      FoodProductRequest.forListResponse(1, "name")
    ];
    Future<List<FoodProductRequest>> getItems() async {
      var apiService = ApiService();
      var result = await apiService.get("api/foodproduct?pageSize=1000  &index=0");
      return FoodProductRequest.resultListFromJson(result);
    }

    return Scaffold(
        appBar: AppBar(title: Text("Prehrambeni proizvodi", style: TextStyle(fontSize: 15),), actions: <Widget>[
          Padding(
            padding: EdgeInsets.only(right: 20.0),
            child: ElevatedButton(
              child: Text("Nova stavka"),
              onPressed: () {
                Navigator.of(context).pushNamed(FoodProductCreatePage.routeName).then((value) => setState((){}));
              },
            ),
          ),
        ]),
        body: Center(
            child: FutureBuilder<List<FoodProductRequest>>(
          future: getItems(),
          builder: (BuildContext context,
              AsyncSnapshot<List<FoodProductRequest>> snapshot) {
            if (snapshot.connectionState == ConnectionState.waiting) {
              return ListView.builder(
                  itemCount: _foodProducts.length,
                  itemBuilder: (BuildContext ctxt, int index) {
                    return ListTile(
                      title: Text(_foodProducts[index].name!),
                      onTap: () {
                      },
                    );
                  });
            } else {
              return new ListView.builder(
                  itemCount: snapshot.data!.length,
                  itemBuilder: (BuildContext ctxt, int index) {
                    return ListTile(
                      title: Text("${snapshot.data![index].name!}"),
                      onTap: () {
                        Navigator.of(context).pushNamed(FoodProductDetailsPage.routeName, arguments: snapshot.data![index].id).then((value) => setState((){}));
                      },
                    );
                  });
            }
          },
        )));
  }
}
