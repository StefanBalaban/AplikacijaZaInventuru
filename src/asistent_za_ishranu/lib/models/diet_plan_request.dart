import 'dart:convert';
import 'package:asistent_za_ishranu/models/base_model.dart';
import 'diet_plan_meal_model.dart';

class DietPlanRequest extends BaseModel {
  int? id;
  String? name;
  List<DietPlanMealModel>? dietPlanMeals;

  DietPlanRequest(this.name, this.dietPlanMeals, [this.id]);

  DietPlanRequest.forListResponse(this.id, this.name);

  @override
  Map<String, dynamic> toJson() {
    return {
      "id": id,
      "name": name,
      "dietPlanMeals": dietPlanMeals
    };
  }

  factory DietPlanRequest.fromJson(Map<String, dynamic> map) {
    List<DietPlanMealModel> meals =  map['dietPlan']['dietPlanMeals'].map<DietPlanMealModel>((e) => DietPlanMealModel.fromJson(e)).toList();
    return DietPlanRequest(
        map['dietPlan']['name'], meals);
  }

  factory DietPlanRequest.fromListJson(Map<String, dynamic> map) {
    return DietPlanRequest.forListResponse(
        map['id'], map['name']);
  }

  static List<DietPlanRequest> fromJsonList(Map<String, dynamic> map) {
    return map["dietPlans"].map<DietPlanRequest>((foodProduct) => DietPlanRequest.fromListJson(foodProduct)).toList();
  }

  static List<DietPlanRequest> resultListFromJson(String json) {
    final data = JsonDecoder().convert(json);    
    return DietPlanRequest.fromJsonList(data);
  }

  static DietPlanRequest resultFromJson(String json) {
    final data = JsonDecoder().convert(json);
    return DietPlanRequest.fromJson(data);
  }
}
