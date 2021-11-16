import 'package:asistent_za_ishranu/models/food_product_request.dart';
import 'package:asistent_za_ishranu/models/food_stock_request.dart';
import 'package:asistent_za_ishranu/models/diet_plan_request.dart';
import 'package:asistent_za_ishranu/services/api_service.dart';
import 'package:asistent_za_ishranu/services/auth_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter_datetime_picker/flutter_datetime_picker.dart';
import 'package:intl/intl.dart';

class FoodStockCreatePage extends StatefulWidget {
  const FoodStockCreatePage({Key? key}) : super(key: key);

  static const routeName = "/food_stock_create";

  @override
  _FoodStockCreatePageState createState() => _FoodStockCreatePageState();
}

class _FoodStockCreatePageState extends State<FoodStockCreatePage> {
  final _formKey = GlobalKey<FormState>();
  int? foodProductId;
  DateTime bestUseByDate = DateTime.now();
  DateTime dateOfPurchase =  DateTime.now();
  TextEditingController amount = TextEditingController();
  TextEditingController upperAmount = TextEditingController();
  TextEditingController lowerAmount = TextEditingController();
  late Future<List<FoodProductRequest>> foodProducts;

  Future<void> create() async {
    {
      var apiService = ApiService();
      await apiService.post(
          "api/foodstock",
          FoodStockRequest(
                  foodProductId,
                  bestUseByDate,
                  dateOfPurchase,
                  double.parse(amount.text),
                  double.parse(upperAmount.text),
                  double.parse(lowerAmount.text),
                  0,
                  AuthService().userId)
              .modelToJson());

      Navigator.of(context).pop(context);
    }
  }

  Future<List<FoodProductRequest>> getFoodProducts() async {
    var apiService = ApiService();
    var result = await apiService.get("api/foodproduct?pageSize=1000&index=0&userId=${AuthService().userId}");
    return FoodProductRequest.resultListFromJson(result);
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
          title: Text("Unos zalihe"),
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
                          child: DropdownButtonFormField(
                            items: snapshot.data!
                                .map((e) => DropdownMenuItem(
                                      child: Text(e.name!),
                                      value: e.id,
                                    ))
                                .toList(),
                            hint: Text('Prehrambeni proizvod'),
                            onChanged: (value) {
                              setState(() {
                                foodProductId = value! as int;
                              });
                            },
                            validator: (int? value) {
                              if (value == null || value == 0) {
                                return 'Odaberite prehrambeni proizvod';
                              }
                              return null;
                            },
                          )),
                      ConstrainedBox(
                          constraints:
                              BoxConstraints.tight(const Size(200, 50)),
                          child: Padding(
                              padding: EdgeInsets.all(10),
                              child: ElevatedButton(
                                onPressed: () {
                                  DatePicker.showDatePicker(context,
                                      showTitleActions: true,
                                      onConfirm: (date) {
                                    setState(() {
                                      bestUseByDate = date;
                                    });
                                  }, currentTime: bestUseByDate);
                                },
                                child: Text(
                                  'Rok trajanja',
                                ),
                              ))),
                      Text(
                          "${DateFormat('dd.MM.yyyy').format(bestUseByDate ?? DateTime.now())}"),
                      ConstrainedBox(
                          constraints:
                              BoxConstraints.tight(const Size(200, 50)),
                          child: Padding(
                              padding: EdgeInsets.all(10),
                              child: ElevatedButton(
                                onPressed: () {
                                  DatePicker.showDatePicker(
                                    context,
                                    showTitleActions: true,
                                    onConfirm: (date) {
                                      setState(() {
                                        dateOfPurchase = date;
                                      });
                                    },
                                    currentTime: dateOfPurchase,
                                  );
                                },
                                child: Text(
                                  'Datum kupovine',
                                ),
                              ))),
                      Text(
                          "${DateFormat('dd.MM.yyyy').format(dateOfPurchase ?? DateTime.now())}"),
                      ConstrainedBox(
                          constraints:
                              BoxConstraints.tight(const Size(200, 50)),
                          child: TextFormField(
                              decoration:
                                  InputDecoration(labelText: "Količina"),
                              controller: amount,
                              validator: (String? value) {
                                if (value == null ||
                                    value.isEmpty ||
                                    double.tryParse(value) == null) {
                                  return 'Vrijednost je prazna ili nije broj';
                                }
                                return null;
                              },
                              keyboardType: TextInputType.numberWithOptions(
                                  decimal: true))),
                      ConstrainedBox(
                          constraints:
                              BoxConstraints.tight(const Size(200, 50)),
                          child: TextFormField(
                              decoration:
                                  InputDecoration(labelText: "Gornja granica"),
                              controller: upperAmount,
                              validator: (String? value) {
                                if (value == null ||
                                    value.isEmpty ||
                                    double.tryParse(value) == null) {
                                  return 'Vrijednost je prazna ili nije broj';
                                }
                                return null;
                              },
                              keyboardType: TextInputType.numberWithOptions(
                                  decimal: true))),
                      ConstrainedBox(
                          constraints:
                              BoxConstraints.tight(const Size(200, 50)),
                          child: TextFormField(
                              decoration:
                                  InputDecoration(labelText: "Donja granica"),
                              controller: lowerAmount,
                              validator: (String? value) {
                                if (value == null ||
                                    value.isEmpty ||
                                    double.tryParse(value) == null) {
                                  return 'Vrijednost je prazna ili nije broj';
                                }
                                return null;
                              },
                              keyboardType: TextInputType.numberWithOptions(
                                  decimal: true))),
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
