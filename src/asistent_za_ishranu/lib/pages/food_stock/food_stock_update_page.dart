import 'package:asistent_za_ishranu/models/food_product_request.dart';
import 'package:asistent_za_ishranu/models/food_stock_request.dart';
import 'package:asistent_za_ishranu/services/api_service.dart';
import 'package:asistent_za_ishranu/services/auth_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter_datetime_picker/flutter_datetime_picker.dart';
import 'package:intl/intl.dart';

class FoodStockUpdatePage extends StatefulWidget {
  const FoodStockUpdatePage({Key? key}) : super(key: key);

  static const routeName = '/food_stock_update';

  @override
  _FoodStockUpdatePageState createState() => _FoodStockUpdatePageState();
}

class _FoodStockUpdatePageState extends State<FoodStockUpdatePage> {
  late Future<FoodStockRequest> foodStock;
  final _formKey = GlobalKey<FormState>();
  var id = 0;
  var unitOfMeasureId = 0;
  var firstLoad = true;
  TextEditingController amount = TextEditingController();
  TextEditingController upperAmount = TextEditingController();
  TextEditingController lowerAmount = TextEditingController();

  DateTime bestUseByDate = DateTime.now();
  DateTime dateOfPurchase = DateTime.now();
  int dietPlanId = 0;

  List<FoodProductRequest>? foodProducts;

  Future<List<FoodProductRequest>> getFoodProducts() async {
    var apiService = ApiService();
    var result = await apiService.get("api/foodproduct?pageSize=1000&index=0&userId=${AuthService().userId}");
    return FoodProductRequest.resultListFromJson(result);
  }

  Future<FoodStockRequest> getItem(id) async {
    var apiService = ApiService();
    var result = await apiService.get("api/foodstock/$id");
    foodProducts = await getFoodProducts();
    return FoodStockRequest.resultFromJson(result);
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
      foodStock = getItem(id);
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: Text("Izmjeni stanje zalihe"),
        ),
        body: FutureBuilder<FoodStockRequest>(
            future: foodStock,
            builder: (BuildContext context,
                AsyncSnapshot<FoodStockRequest> snapshot) {
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
                if (firstLoad) {
                  bestUseByDate = snapshot.data!.bestUseByDate!;
                  dateOfPurchase = snapshot.data!.dateOfPurchase!;
                  dietPlanId = snapshot.data!.foodProductId!;
                  firstLoad = false;
                  amount.text = snapshot.data!.amount.toString();
                  upperAmount.text = snapshot.data!.upperAmount.toString();
                  lowerAmount.text = snapshot.data!.lowerAmount.toString();
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
                            child: DropdownButtonFormField(
                              items: foodProducts!
                                  .map((e) => DropdownMenuItem(
                                        child: Text(e.name!),
                                        value: e.id,
                                      ))
                                  .toList(),
                              hint: Text('Plan ishrane'),
                              onChanged: (value) {
                                setState(() {
                                  dietPlanId = value! as int;
                                });
                              },
                              value: dietPlanId,
                              validator: (int? value) {
                                if (value == null || value == 0) {
                                  return 'Odaberite plan ishrane';
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
                                    'Rok trajanja prehrambenog proizvoda',
                                  ),
                                ))),
                        Text(
                            "${DateFormat('dd.MM.yyyy').format(bestUseByDate)}"),
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
                            "${DateFormat('dd.MM.yyyy').format(dateOfPurchase)}"),
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
                                decoration: InputDecoration(
                                    labelText: "Gornja granica"),
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
                        Center(
                          child: ElevatedButton(
                            child: Text("Izmijeni"),
                            onPressed: () async {
                              if (_formKey.currentState!.validate()) {
                                var apiService = ApiService();
                                var dietPlanPeriodRequest = FoodStockRequest(
                                        dietPlanId,
                                        bestUseByDate,
                                        dateOfPurchase,
                                        double.parse(amount.text),
                                        double.parse(upperAmount.text),
                                        double.parse(lowerAmount.text),
                                        id)
                                    .modelToJson();
                                var result = await apiService.put(
                                    "api/foodstock/", dietPlanPeriodRequest);
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
