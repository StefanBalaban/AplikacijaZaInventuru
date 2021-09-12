import 'package:asistent_za_ishranu/models/food_product_request.dart';
import 'package:asistent_za_ishranu/services/api_service.dart';
import 'package:flutter/material.dart';

class FoodProductUpdatePage extends StatefulWidget {
  const FoodProductUpdatePage({Key? key}) : super(key: key);

  static const routeName = '/food_product_update';

  @override
  _FoodProductUpdatePageState createState() => _FoodProductUpdatePageState();
}

class _FoodProductUpdatePageState extends State<FoodProductUpdatePage> {
  late Future<FoodProductRequest> foodProduct;
  var id = 0;
  var unitOfMeasureId = 0;
  var firstLoad = true;

  Future<FoodProductRequest> getItem(id) async {
    var apiService = ApiService();
    var result = await apiService.get("api/foodproduct/$id");
    return FoodProductRequest.resultFromJson(result);
  }

  @override
  void initState() {
    super.initState();
    // future that allows us to access context. function is called inside the future
    // otherwise it would be skipped and args would return null
    Future.delayed(Duration.zero, () {
      setState(() {
        id = ModalRoute.of(context)!.settings.arguments as int;
      });
      foodProduct = getItem(id);
    });

  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: Text("Izmjeni prehrambeni proizvod"),
        ),
        body: FutureBuilder<FoodProductRequest>(
            future: foodProduct,
            builder: (BuildContext context,
                AsyncSnapshot<FoodProductRequest> snapshot) {

              if (snapshot.connectionState == ConnectionState.waiting) {
                return Form(
                    child: Column(
                      children: [TextFormField(
                          decoration: InputDecoration(labelText: "Naziv"),
                          initialValue: "",
                        readOnly: true,
                      )],
                    ));
              } else {
                var name = TextEditingController(text: snapshot.data!.name);
                var calories = TextEditingController(text: snapshot.data!.calories.toString());
                var carbohydrates = TextEditingController(text: snapshot.data!.carbohydrates.toString());
                var protein = TextEditingController(text: snapshot.data!.protein.toString());
                var fats = TextEditingController(text: snapshot.data!.fats.toString());
                if (firstLoad) {
                  unitOfMeasureId = snapshot.data!.unitOfMeasureId!;
                  firstLoad = false;
                };
                return Form(
                    child: Column(
                      children: [
                        TextFormField(
                          decoration: InputDecoration(labelText: "Naziv"),
                          controller: name
                        ),
                        DropdownButtonFormField(
                          items: [DropdownMenuItem(child: Text("Komad"), value: 1), DropdownMenuItem(child: Text("Težina"), value: 2)]
                              .toList(),
                          hint: Text('Jedinica mjere'),
                          onChanged: (value) {
                            setState(() {
                              unitOfMeasureId = value! as int;
                            });

                          },
                          value: snapshot.data!.unitOfMeasureId,
                        ),
                        TextFormField(
                          decoration: InputDecoration(labelText: "Kalorije"),
                          controller: calories,
                        ),
                        TextFormField(
                          decoration: InputDecoration(labelText: "Ugljikohidrati"),
                          controller: carbohydrates,
                        ),
                        TextFormField(
                          decoration: InputDecoration(labelText: "Proteini"),
                          controller: protein,
                        ),
                        TextFormField(
                          decoration: InputDecoration(labelText: "Masti"),
                          controller: fats,
                        ),
                        Center(child: ElevatedButton(child: Text("Izmijeni"), onPressed: ( ) async {
                          var apiService = ApiService();
                          var result = await apiService.put("api/foodproduct/",
                              FoodProductRequest(
                                  name.text,
                                  unitOfMeasureId,
                                  double.parse(calories.text),
                                  double.parse(protein.text),
                                  double.parse(carbohydrates.text),
                                  double.parse(fats.text),
                                  id
                              )
                                  .modelToJson());
                          Navigator.of(context).pop();
                        },),),

                      ],
                    ));
              }
            }));
  }
}
