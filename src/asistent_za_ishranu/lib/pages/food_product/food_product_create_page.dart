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
    return Form(
        child: Scaffold(
            appBar: AppBar(
              title: Text("Unos prehrambenog proizvoda"),
            ),
            body: Column(
              key: _formKey,

              children: <Widget>[
                TextFormField(
                  validator: (value) {
                    if (value == null || value.isEmpty) {
                      return 'Naziv';
                    }
                  },
                  controller: _nameController,
                  decoration: InputDecoration(hintText: "Naziv"),
                ),

                DropdownButtonFormField(
                  items: [DropdownMenuItem(child: Text("Komad"), value: 1), DropdownMenuItem(child: Text("Težina"), value: 2)]
                      .toList(),
                  hint: Text('Jedinica mjere'),
                  onChanged: (value) {
                    setState(() {
                      _unitOfId = value! as int;
                    });
                  },

                ),
                TextFormField(
                  validator: (value) {
                    if (value == null || value.isEmpty) {
                      return 'Kalorije';
                    }
                  },
                  controller: _caloriesController,
                  keyboardType: TextInputType.numberWithOptions(decimal: true),
                ),
                TextFormField(
                  validator: (value) {
                    if (value == null || value.isEmpty) {
                      return 'Proteini';
                    }
                  },
                  controller: _proteinController,
                  keyboardType: TextInputType.numberWithOptions(decimal: true),
                ),
                TextFormField(
                  validator: (value) {
                    if (value == null || value.isEmpty) {
                      return 'Ugljikohidrati';
                    }
                  },
                  controller: _carbohydratesController,
                  keyboardType: TextInputType.numberWithOptions(decimal: true),
                ),
                TextFormField(
                  validator: (value) {
                    if (value == null || value.isEmpty) {
                      return 'Masti';
                    }
                  },
                  controller: _fatsController,
                  keyboardType: TextInputType.numberWithOptions(decimal: true),
                ),
                ElevatedButton(
                  onPressed: () {
                    create();
                  },
                  child: const Text("Unos"),
                )
              ],
            )));
  }
}
