import 'package:asistent_za_ishranu/models/food_product_request.dart';
import 'package:asistent_za_ishranu/models/meal_item_model.dart';
import 'package:asistent_za_ishranu/models/meal_request.dart';
import 'package:asistent_za_ishranu/services/api_service.dart';
import 'package:flutter/material.dart';

class MealUpdatePage extends StatefulWidget {
  const MealUpdatePage({Key? key}) : super(key: key);

  static const routeName = '/meal_update';

  @override
  _MealUpdatePageState createState() => _MealUpdatePageState();
}

class _MealUpdatePageState extends State<MealUpdatePage> {
  late Future<MealRequest> meal;
  final _formKey = GlobalKey<FormState>();
  List<FoodProductRequest> foodProducts = [];
  var id = 0;
  var unitOfMeasureId = 0;
  var firstLoad = true;
  List<TextEditingController> _foodProductIds = [];
  List<TextEditingController> _amountControllers = [];
  List<Widget> _foodProductFields = <Widget>[];
  int _foodProductFieldIndex = 0;
  bool initialization = true;

  Future<MealRequest> getItem(id) async {
    var apiService = ApiService();
    var result = await apiService.get("api/meal/$id");
    return MealRequest.resultFromJson(result);
  }

  List<Widget> getMealFields(List<MealItemModel>? data) {
    if (initialization) {
      data!.forEach((element) {
        addMealField(element);
      });
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

  void addMealField(MealItemModel? data) {
    _foodProductIds.add(TextEditingController(text: data!.foodProductId.toString()));
    var _foodProductIdIndex = _foodProductFieldIndex;
    _amountControllers.add(TextEditingController(text: data!.amount.toString()));
    _foodProductFields!.add(ConstrainedBox(
        constraints: BoxConstraints.tight(const Size(200, 50)),
        child: Padding(
          child: Text("Prehrambeni proizvod: ${_foodProductFieldIndex + 1}"),
          padding: EdgeInsets.all(10),
        )));
    _foodProductFields!.add(ConstrainedBox(
        constraints: BoxConstraints.tight(const Size(200, 50)),
        child: DropdownButtonFormField(
          items: foodProducts!
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
          value: data!.foodProductId,
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
    Future.delayed(Duration.zero, () async {
      setState(() {
        id = (ModalRoute.of(context)!.settings.arguments as List<dynamic>)[0] as int;
        foodProducts = (ModalRoute.of(context)!.settings.arguments as List<dynamic>)[1] as List<FoodProductRequest>;
      });
      meal = getItem(id);
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: Text("Izmjeni prehrambeni proizvod"),
        ),
        body: FutureBuilder<MealRequest>(
            future: meal,
            builder: (BuildContext context,
                AsyncSnapshot<MealRequest> snapshot) {
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
                        Column(children: getMealFields(snapshot.data!.meals),),

              ElevatedButton(
              onPressed: () {
              addFoodProductField(foodProducts);
              setState(() {

              });
              },
              child: const Text("Novi prehrambeni proizvod"),
              ),
                        Center(
                          child: ElevatedButton(
                            child: Text("Izmijeni"),
                            onPressed: () async {
                              if (_formKey.currentState!.validate()) {
                                var apiService = ApiService();
                                var fieldIndex = 0;
                                var result = await apiService.put(
                                    "api/meal/",
                                    MealRequest(
                                        name.text,
                                        _foodProductIds.map((e) {
                                          return MealItemModel(int.parse(e.text),
                                              double.parse(_amountControllers[fieldIndex++].text));
                                        },).toList(), id)
                                        .modelToJson());
                                Navigator.of(context).pop();
                              }
                            },
                          ),
                        ),
                      ],
                    )));
              }}));
  }
}
