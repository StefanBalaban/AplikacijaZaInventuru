import 'dart:convert';

import 'package:asistent_za_ishranu/models/base_model.dart';

class FoodProductRequest extends BaseModel {
  int? id;
  String? name;
  int? unitOfMeasureId;
  double? calories;
  double? protein;
  double? carbohydrates;
  double? fats;


  FoodProductRequest(this.name, this.unitOfMeasureId, this.calories,
      this.protein, this.carbohydrates, this.fats, [this.id]);

  FoodProductRequest.forListResponse(this.id, this.name);

  @override
  Map<String, dynamic> toJson() {
    return {
      "id": id,
      "name": name,
      "unitOfMeasureId": unitOfMeasureId,
      "calories": calories,
      "protein": protein,
      "carbohydrates": carbohydrates,
      "fats": fats
    };
  }

  factory FoodProductRequest.fromJson(Map<String, dynamic> map) {
    return FoodProductRequest(
        map['foodProduct']['name'], map['foodProduct']['unitOfMeasureId'],
        map['foodProduct']["calories"].toDouble(), map['foodProduct']["protein"].toDouble(),
    map['foodProduct']["carbohydrates"].toDouble(), map['foodProduct']["fats"].toDouble());
  }

  factory FoodProductRequest.fromListJson(Map<String, dynamic> map) {
    return FoodProductRequest.forListResponse(
        map['id'], map['name']);
  }

  static List<FoodProductRequest> fromJsonList(Map<String, dynamic> map) {
    return map["foodProducts"].map<FoodProductRequest>((foodProduct) => FoodProductRequest.fromListJson(foodProduct)).toList();
  }

  static List<FoodProductRequest> resultListFromJson(String json) {
    final data = JsonDecoder().convert(json);    
    return FoodProductRequest.fromJsonList(data);
  }

  static FoodProductRequest resultFromJson(String json) {
    final data = JsonDecoder().convert(json);
    return FoodProductRequest.fromJson(data);
  }
}
