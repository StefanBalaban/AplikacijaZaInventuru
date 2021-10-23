import 'dart:convert';

import 'package:asistent_za_ishranu/models/base_model.dart';

class FoodStockRequest extends BaseModel {
  int? id;
  int? foodProductId;
  DateTime? bestUseByDate;
  DateTime? dateOfPurchase;
  double? amount;
  double? upperAmount;
  double? lowerAmount;

  FoodStockRequest(this.foodProductId, this.bestUseByDate, this.dateOfPurchase,
      this.amount, this.upperAmount, this.lowerAmount,
      [this.id]);

  FoodStockRequest.forListResponse(this.id);

  @override
  Map<String, dynamic> toJson() {
    return {
      "id": id,
      "bestUseByDate": bestUseByDate!.toIso8601String(),
      "dateOfPurchase": dateOfPurchase!.toIso8601String(),
      "foodProductId": foodProductId,
      "amount": amount,
      "upperAmount": upperAmount,
      "lowerAmount": lowerAmount,
    };
  }

  factory FoodStockRequest.fromJson(Map<String, dynamic> map) {
    return FoodStockRequest(
        map['foodStock']['foodProductId'],
        DateTime.parse(map['foodStock']['bestUseByDate']),
        DateTime.parse(map['foodStock']['dateOfPurchase']),
        map['foodStock']['amount'].toDouble(),
        map['foodStock']['upperAmount'].toDouble(),
        map['foodStock']['lowerAmount'].toDouble());
  }

  factory FoodStockRequest.fromListJson(Map<String, dynamic> map) {
    return FoodStockRequest.fromListJson(map["id"]);
  }

  static List<FoodStockRequest> fromJsonList(Map<String, dynamic> map) {
    return map["foodStocks"]
        .map<FoodStockRequest>(
            (dietPlan) => FoodStockRequest.fromListJson(dietPlan))
        .toList();
  }

  static List<FoodStockRequest> resultListFromJson(String json) {
    final data = JsonDecoder().convert(json);
    return FoodStockRequest.fromJsonList(data);
  }

  static FoodStockRequest resultFromJson(String json) {
    final data = JsonDecoder().convert(json);
    return FoodStockRequest.fromJson(data);
  }
}
