/* import 'package:asistent_za_ishranu/models/food_product_request.dart';
import 'package:asistent_za_ishranu/models/diet_plan_item_model.dart';
import 'package:asistent_za_ishranu/models/diet_plan_request.dart';
import 'package:asistent_za_ishranu/services/api_service.dart';
import 'package:flutter/material.dart';

class DietPlanUpdatePage extends StatefulWidget {
  const DietPlanUpdatePage({Key? key}) : super(key: key);

  static const routeName = '/diet_plan_update';

  @override
  _DietPlanUpdatePageState createState() => _DietPlanUpdatePageState();
}

class _DietPlanUpdatePageState extends State<DietPlanUpdatePage> {
  late Future<DietPlanRequest> dietplan;
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

  Future<DietPlanRequest> getItem(id) async {
    var apiService = ApiService();
    var result = await apiService.get("api/dietplan/$id");
    return DietPlanRequest.resultFromJson(result);
  }

  List<Widget> getDietPlanFields(List<DietPlanItemModel>? data) {
    if (initialization) {
      data!.forEach((element) {
        addDietPlanField(element);
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

  void addDietPlanField(DietPlanItemModel? data) {
    _foodProductIds.add(TextEditingController(text: data!.dietPlanId.toString()));
    var _foodProductIdIndex = _foodProductFieldIndex;
    _amountControllers.add(TextEditingController(text: data!.mealId.toString()));
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
          value: data!.dietPlanId,
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
      dietplan = getItem(id);
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: Text("Izmjeni prehrambeni proizvod"),
        ),
        body: FutureBuilder<DietPlanRequest>(
            future: dietplan,
            builder: (BuildContext context,
                AsyncSnapshot<DietPlanRequest> snapshot) {
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
                        Column(children: getDietPlanFields(snapshot.data!.dietplans),),

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
                                    "api/dietplan/",
                                    DietPlanRequest(
                                        name.text,
                                        _foodProductIds.map((e) {
                                          return DietPlanItemModel(int.parse(e.text),
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
 */