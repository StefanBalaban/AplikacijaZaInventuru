import 'package:asistent_za_ishranu/models/food_product_request.dart';
import 'package:asistent_za_ishranu/models/meal_item_model.dart';
import 'package:asistent_za_ishranu/models/meal_request.dart';
import 'package:asistent_za_ishranu/services/api_service.dart';
import 'package:flutter/material.dart';

class MealCreatePage extends StatefulWidget {
  const MealCreatePage({Key? key}) : super(key: key);

  static const routeName = "/meal_create";

  @override
  _MealCreatePageState createState() => _MealCreatePageState();
}

class _MealCreatePageState extends State<MealCreatePage> {
  final _formKey = GlobalKey<FormState>();
  late Future<List<FoodProductRequest>> foodProducts;
  TextEditingController _nameController = TextEditingController();
  List<TextEditingController> _foodProductIds = [];
  List<TextEditingController> _amountControllers = [];
  List<Widget> _foodProductFields = <Widget>[];
  int _foodProductFieldIndex = 0;
  bool initialization = true;

  Future<void> create() async {
    var fieldIndex = 0;
    var mealRequest = MealRequest(
            _nameController.text,
            _foodProductIds.map((e) {
              return MealItemModel(int.parse(e.text),
                  double.parse(_amountControllers[fieldIndex++].text));
            }).toList())
        .modelToJson();
    {
      var apiService = ApiService();
      await apiService.post("api/meal", mealRequest);

      Navigator.of(context).pop(context);
    }
  }

  Future<List<FoodProductRequest>> getFoodProducts() async {
    var apiService = ApiService();
    var result = await apiService.get("api/foodproduct?pageSize=1000&index=0");
    return FoodProductRequest.resultListFromJson(result);
  }

  List<Widget> getFoodProductFields(List<FoodProductRequest>? data) {
    if (initialization) {
      addFoodProductField(data!);
      initialization = false;
    }

    return _foodProductFields;
  }

  void addFoodProductField(List<FoodProductRequest>? data) {
    _foodProductIds.add(TextEditingController());
    var _foodProductIdIndex = _foodProductFieldIndex;
    _amountControllers.add(TextEditingController());
    _foodProductFields!.add(ConstrainedBox(
        constraints: BoxConstraints.tight(const Size(200, 50)),
        child: Padding(
          child: Text("Prehrambeni proizvod: ${_foodProductFieldIndex + 1}"),
          padding: EdgeInsets.all(10),
        )));
    _foodProductFields!.add(ConstrainedBox(
        constraints: BoxConstraints.tight(const Size(200, 50)),
        child: DropdownButtonFormField(
          items: data!
              .map((e) => DropdownMenuItem(
                    child: Text(e.name!),
                    value: e.id,
                  ))
              .toList(),
          hint: Text('Jedinica mjere'),
          onChanged: (value) {
            setState(() {
              _foodProductIds[_foodProductIdIndex].text = "$value";
            });
          },
          validator: (int? value) {
            if (value == null || value == 0) {
              return 'Odaberite jedinicu';
            }
            return null;
          },
        )));
    _foodProductFields!.add(ConstrainedBox(
        constraints: BoxConstraints.tight(const Size(200, 50)),
        child: TextFormField(
            decoration: InputDecoration(labelText: "Količina"),
            controller: _amountControllers[_foodProductFieldIndex],
            validator: (String? value) {
              if (value == null ||
                  value.isEmpty ||
                  double.tryParse(value) == null) {
                return 'Vrijednost je prazna ili nije broj';
              }
              return null;
            },
            keyboardType: TextInputType.numberWithOptions(decimal: true))));
    _foodProductFieldIndex++;
  }

  @override
  void initState() {
    super.initState();
    // future that allows us to access context. function is called inside the future
    // otherwise it would be skipped and args would return null
    Future.delayed(Duration.zero, () {
      setState(() {});
      foodProducts = getFoodProducts();
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: Text("Unos obroka"),
        ),
        body: FutureBuilder<List<FoodProductRequest>>(
            future: foodProducts,
            builder: (BuildContext context,
                AsyncSnapshot<List<FoodProductRequest>> snapshot) {
              if (snapshot.connectionState == ConnectionState.waiting) {
                return Column(
                  children: [],
                );
              } else {
                return Form(
                    key: _formKey,
                    child: SingleChildScrollView(
                        child: Center(
                            child: Column(children: <Widget>[
                      ConstrainedBox(
                          constraints:
                              BoxConstraints.tight(const Size(200, 50)),
                          child: TextFormField(
                            validator: (value) {
                              if (value == null || value.isEmpty) {
                                return 'Naziv';
                              }
                            },
                            controller: _nameController,
                            decoration: InputDecoration(hintText: "Naziv"),
                          )),
                      Column(children: getFoodProductFields(snapshot!.data)),
                      ElevatedButton(
                        onPressed: () {
                          addFoodProductField(snapshot!.data);
                          setState(() {

                          });
                        },
                        child: const Text("Novi prehrambeni proizvod"),
                      ),
                      ElevatedButton(
                        onPressed: () {
                          if (_formKey.currentState!.validate()) {
                            create();
                          }
                        },
                        child: const Text("Unos"),
                      )
                    ]))));
              }
            }));
  }
}
