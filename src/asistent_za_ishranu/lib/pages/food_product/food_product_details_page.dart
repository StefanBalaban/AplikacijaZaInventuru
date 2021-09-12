import 'package:asistent_za_ishranu/models/food_product_request.dart';
import 'package:asistent_za_ishranu/pages/food_product/food_product_update_page.dart';
import 'package:asistent_za_ishranu/services/api_service.dart';
import 'package:flutter/material.dart';

class FoodProductDetailsPage extends StatefulWidget {
  const FoodProductDetailsPage({Key? key}) : super(key: key);

  static const routeName = '/food_product_details';

  @override
  _FoodProductDetailsPageState createState() => _FoodProductDetailsPageState();
}

class _FoodProductDetailsPageState extends State<FoodProductDetailsPage> {
  Future<FoodProductRequest> getItem(id) async {
    var apiService = ApiService();
    var result = await apiService.get("api/foodproduct/$id");
    return FoodProductRequest.resultFromJson(result);
  }

  Future<void> deleteItem(id) async {
    var apiService = ApiService();
    var result = await apiService.delete("api/foodproduct/$id");
  }

  @override
  Widget build(BuildContext context) {
    final id = ModalRoute.of(context)!.settings.arguments as int;
    return Scaffold(
        appBar: AppBar(
          title: Text("Detalji prehrambenog proizvoda"),
        ),
        body: FutureBuilder<FoodProductRequest>(
            future: getItem(id),
            builder: (BuildContext context,
                AsyncSnapshot<FoodProductRequest> snapshot) {
              if (snapshot.connectionState == ConnectionState.waiting) {
                return Form(
                    child: Column(
                  children: [],
                ));
              } else {
                return Form(
                    child: Column(
                  children: [
                    TextFormField(
                      initialValue: snapshot.data!.name,
                      decoration: InputDecoration(labelText: "Naziv"),
                      readOnly: true,
                    ),
                    TextFormField(
                      initialValue: snapshot.data!.unitOfMeasureId == 1
                          ? "Komad"
                          : "Težina",
                      decoration: InputDecoration(labelText: "Jedinica mjere"),
                      readOnly: true,
                    ),
                    TextFormField(
                      initialValue: snapshot.data!.calories.toString(),
                      decoration: InputDecoration(labelText: "Kalorije"),
                      readOnly: true,
                    ),
                    TextFormField(
                      initialValue: snapshot.data!.carbohydrates.toString(),
                      decoration: InputDecoration(labelText: "Ugljikohidrati"),
                      readOnly: true,
                    ),
                    TextFormField(
                      initialValue: snapshot.data!.protein.toString(),
                      decoration: InputDecoration(labelText: "Proteini"),
                      readOnly: true,
                    ),
                    TextFormField(
                      initialValue: snapshot.data!.fats.toString(),
                      decoration: InputDecoration(labelText: "Masti"),
                      readOnly: true,
                    ),
                    Center(
                      child: ElevatedButton(
                        child: Text("Izmijeni"),
                        onPressed: () {
                          Navigator.of(context).pushNamed(
                              FoodProductUpdatePage.routeName,
                              arguments: id).then((value) => setState((){}));
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
                ));
              }
            }));
  }
}
