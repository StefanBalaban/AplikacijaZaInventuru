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
  final _formKey = GlobalKey<FormState>();
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
                  children: [
                    TextFormField(
                      decoration: InputDecoration(labelText: ""),
                      initialValue: "",
                      readOnly: true,
                    )
                  ],
                ));
              } else {
                var name = TextEditingController(text: snapshot.data!.name);
                var calories = TextEditingController(
                    text: snapshot.data!.calories.toString());
                var carbohydrates = TextEditingController(
                    text: snapshot.data!.carbohydrates.toString());
                var protein = TextEditingController(
                    text: snapshot.data!.protein.toString());
                var fats =
                    TextEditingController(text: snapshot.data!.fats.toString());
                if (firstLoad) {
                  unitOfMeasureId = snapshot.data!.unitOfMeasureId!;
                  firstLoad = false;
                }
                ;
                return Form(
                    key: _formKey,
                    child: SingleChildScrollView(
                        child: Column(
                      children: [
                        ConstrainedBox(
                            constraints:
                                BoxConstraints.tight(const Size(200, 50)),
                            child: TextFormField(
                              decoration: InputDecoration(labelText: "Naziv"),
                              controller: name,
                              validator: (String? value) {
                                if (value == null || value.isEmpty) {
                                  return 'Vrijednost ne smije biti prazna';
                                }
                                return null;
                              },
                            )),
                        ConstrainedBox(
                            constraints:
                                BoxConstraints.tight(const Size(200, 50)),
                            child: DropdownButtonFormField(
                              items: [
                                DropdownMenuItem(
                                    child: Text("Komad"), value: 1),
                                DropdownMenuItem(
                                    child: Text("Težina"), value: 2)
                              ].toList(),
                              hint: Text('Jedinica mjere'),
                              onChanged: (value) {
                                setState(() {
                                  unitOfMeasureId = value! as int;
                                });
                              },
                              value: snapshot.data!.unitOfMeasureId,
                            )),
                        ConstrainedBox(
                            constraints:
                                BoxConstraints.tight(const Size(200, 50)),
                            child: TextFormField(
                                decoration:
                                    InputDecoration(labelText: "Kalorije"),
                                controller: calories,
                                validator: (String? value) {
                                  if (value == null ||
                                      value.isEmpty ||
                                      double.tryParse(value) == null) {
                                    return 'Vrijednost je prazna ili nije broj';
                                  }
                                  return null;
                                },
                                keyboardType: TextInputType.numberWithOptions(decimal: true))),
                        ConstrainedBox(
                            constraints:
                                BoxConstraints.tight(const Size(200, 50)),
                            child: TextFormField(
                              decoration:
                                  InputDecoration(labelText: "Ugljikohidrati"),
                              controller: carbohydrates,validator: (String? value) {
                              if (value == null ||
                                  value.isEmpty ||
                                  double.tryParse(value) == null) {
                                return 'Vrijednost je prazna ili nije broj';
                              }
                              return null;
                            },
                                keyboardType: TextInputType.numberWithOptions(decimal: true)
                            )),
                        ConstrainedBox(
                            constraints:
                                BoxConstraints.tight(const Size(200, 50)),
                            child: TextFormField(
                                decoration:
                                    InputDecoration(labelText: "Proteini"),
                                controller: protein,
                                validator: (String? value) {
                                  if (value == null ||
                                      value.isEmpty ||
                                      !(double.tryParse(value) is double)) {
                                    return 'Vrijednost je prazna ili nije broj';
                                  }
                                  return null;
                                },
                                keyboardType: TextInputType.numberWithOptions(decimal: true))),
                        ConstrainedBox(
                            constraints:
                                BoxConstraints.tight(const Size(200, 50)),
                            child: TextFormField(
                                decoration: InputDecoration(labelText: "Masti"),
                                controller: fats,
                                validator: (String? value) {
                                  if (value == null ||
                                      value.isEmpty ||
                                      !(double.tryParse(value) is double)) {
                                    return 'Vrijednost je prazna ili nije broj';
                                  }
                                  return null;
                                },
                                keyboardType: TextInputType.numberWithOptions(decimal: true))),
                        Center(
                          child: ElevatedButton(
                            child: Text("Izmijeni"),
                            onPressed: () async {
                              if (_formKey.currentState!.validate()) {
                                var apiService = ApiService();
                                var result = await apiService.put(
                                    "api/foodproduct/",
                                    FoodProductRequest(
                                            name.text,
                                            unitOfMeasureId,
                                            double.parse(calories.text),
                                            double.parse(protein.text),
                                            double.parse(carbohydrates.text),
                                            double.parse(fats.text),
                                            id)
                                        .modelToJson());
                                Navigator.of(context).pop();
                              }
                            },
                          ),
                        ),
                      ],
                    )));
              }
            }));
  }
}
