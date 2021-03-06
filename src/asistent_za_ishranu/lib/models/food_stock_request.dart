import 'dart:convert';

import 'package:asistent_za_ishranu/models/base_model.dart';

class FoodStockRequest extends BaseModel {
  int? id;
  int? userId;
  int? foodProductId;
  DateTime? bestUseByDate;
  DateTime? dateOfPurchase;
  double? amount;
  double? upperAmount;
  double? lowerAmount;

  FoodStockRequest(this.foodProductId, this.bestUseByDate, this.dateOfPurchase,
      this.amount, this.upperAmount, this.lowerAmount,
      [this.id, this.userId]);

  FoodStockRequest.forListResponse(this.id, this.foodProductId);

  @override
  Map<String, dynamic> toJson() {
    return {
      "id": id,
      "userId": userId,
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
    return FoodStockRequest.forListResponse(map["id"],map["foodProductId"]);
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
