import 'package:asistent_za_ishranu/models/food_product_request.dart';
import 'package:asistent_za_ishranu/models/subscribe_model.dart';
import 'package:asistent_za_ishranu/services/api_service.dart';
import 'package:asistent_za_ishranu/services/auth_service.dart';
import 'package:flutter/material.dart';

class FoodProductCreatePage extends StatefulWidget {
  const FoodProductCreatePage({Key? key}) : super(key: key);

  static const routeName = "/food_product_create";

  @override
  _FoodProductCreatePageState createState() => _FoodProductCreatePageState();
}

class _FoodProductCreatePageState extends State<FoodProductCreatePage> {
  final _formKey = GlobalKey<FormState>();
  TextEditingController _nameController = TextEditingController();
  TextEditingController _caloriesController = TextEditingController();
  TextEditingController _proteinController = TextEditingController();
  TextEditingController _carbohydratesController = TextEditingController();
  TextEditingController _fatsController = TextEditingController();
  int? _unitOfId;

  Future<void> create() async {
    {
      var apiService = ApiService();
      await apiService.post(
          "api/foodproduct",
          FoodProductRequest(
                  _nameController.text,
                  _unitOfId!,
                  double.parse(_caloriesController.text),
                  double.parse(_proteinController.text),
                  double.parse(_carbohydratesController.text),
                  double.parse(_fatsController.text))
              .modelToJson());

      Navigator.of(context).pop(context);
    }
  }



  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: Text("Unos prehrambenog proizvoda"),
        ),
        body: Form(
            key: _formKey,
            child: SingleChildScrollView(
                child: Center(
                    child: Column(
              children: <Widget>[
                ConstrainedBox(
                    constraints: BoxConstraints.tight(const Size(200, 50)),
                    child: TextFormField(
                      validator: (value) {
                        if (value == null || value.isEmpty) {
                          return 'Naziv';
                        }
                      },
                      controller: _nameController,
                      decoration: InputDecoration(hintText: "Naziv"),
                    )),
                ConstrainedBox(
                    constraints: BoxConstraints.tight(const Size(200, 50)),
                    child: DropdownButtonFormField(
                      items: [
                        DropdownMenuItem(child: Text("Komad"), value: 1),
                        DropdownMenuItem(child: Text("Težina"), value: 2)
                      ].toList(),
                      hint: Text('Jedinica mjere'),
                      onChanged: (value) {
                        setState(() {
                          _unitOfId = value! as int;
                        });
                      },
                      validator: (int? value) {
                        if (value == null || value == 0) {
                          return 'Odaberite jedinicu';
                        }
                        return null;
                      },
                    )),
                ConstrainedBox(
                    constraints: BoxConstraints.tight(const Size(200, 50)),
                    child: TextFormField(
                        decoration: InputDecoration(labelText: "Kalorije"),
                        controller: _caloriesController,
                        validator: (String? value) {
                          if (value == null ||
                              value.isEmpty ||
                              double.tryParse(value) == null) {
                            return 'Vrijednost je prazna ili nije broj';
                          }
                          return null;
                        },
                        keyboardType:
                            TextInputType.numberWithOptions(decimal: true))),
                ConstrainedBox(
                    constraints: BoxConstraints.tight(const Size(200, 50)),
                    child: TextFormField(
                        decoration:
                            InputDecoration(labelText: "Ugljikohidrati"),
                        controller: _carbohydratesController,
                        validator: (String? value) {
                          if (value == null ||
                              value.isEmpty ||
                              double.tryParse(value) == null) {
                            return 'Vrijednost je prazna ili nije broj';
                          }
                          return null;
                        },
                        keyboardType:
                            TextInputType.numberWithOptions(decimal: true))),
                ConstrainedBox(
                    constraints: BoxConstraints.tight(const Size(200, 50)),
                    child: TextFormField(
                        decoration: InputDecoration(labelText: "Proteini"),
                        controller: _proteinController,
                        validator: (String? value) {
                          if (value == null ||
                              value.isEmpty ||
                              !(double.tryParse(value) is double)) {
                            return 'Vrijednost je prazna ili nije broj';
                          }
                          return null;
                        },
                        keyboardType:
                            TextInputType.numberWithOptions(decimal: true))),
                ConstrainedBox(
                    constraints: BoxConstraints.tight(const Size(200, 50)),
                    child: TextFormField(
                        decoration: InputDecoration(labelText: "Masti"),
                        controller: _fatsController,
                        validator: (String? value) {
                          if (value == null ||
                              value.isEmpty ||
                              !(double.tryParse(value) is double)) {
                            return 'Vrijednost je prazna ili nije broj';
                          }
                          return null;
                        },
                        keyboardType:
                            TextInputType.numberWithOptions(decimal: true))),
                ElevatedButton(
                  onPressed: () {
                    if (_formKey.currentState!.validate()) {
                      create();
                    }
                  },
                  child: const Text("Unos"),
                )
              ],
            )))));
  }
}
