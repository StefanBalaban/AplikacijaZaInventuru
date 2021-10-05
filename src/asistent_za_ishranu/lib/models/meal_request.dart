import 'dart:convert';

import 'package:asistent_za_ishranu/models/base_model.dart';
import 'package:asistent_za_ishranu/models/meal_item_model.dart';

class MealRequest extends BaseModel {
  int? id;
  String? name;
  List<MealItemModel>? meals;

  MealRequest(this.name, this.meals, [this.id]);

  MealRequest.forListResponse(this.id, this.name);

  @override
  Map<String, dynamic> toJson() {
    return {
      "id": id,
      "name": name,
      "meals": meals
    };
  }

  factory MealRequest.fromJson(Map<String, dynamic> map) {
    List<MealItemModel> meals =  map['meal']['meals'].map<MealItemModel>((e) => MealItemModel.fromJson(e)).toList();
    return MealRequest(
        map['meal']['name'], meals);
  }

  factory MealRequest.fromListJson(Map<String, dynamic> map) {
    return MealRequest.forListResponse(
        map['id'], map['name']);
  }

  static List<MealRequest> fromJsonList(Map<String, dynamic> map) {
    return map["meals"].map<MealRequest>((foodProduct) => MealRequest.fromListJson(foodProduct)).toList();
  }

  static List<MealRequest> resultListFromJson(String json) {
    final data = JsonDecoder().convert(json);    
    return MealRequest.fromJsonList(data);
  }

  static MealRequest resultFromJson(String json) {
    final data = JsonDecoder().convert(json);
    return MealRequest.fromJson(data);
  }
}
