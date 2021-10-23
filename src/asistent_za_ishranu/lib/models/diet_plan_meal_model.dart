import 'package:asistent_za_ishranu/models/base_model.dart';

class DietPlanMealModel extends BaseModel {
  int? dietPlanId;
  int? mealId;  

  DietPlanMealModel(this.mealId, [this.dietPlanId]);

  @override
  Map<String, dynamic> toJson() {
    return {"dietPlanId": dietPlanId, "mealId": mealId};
  }

  factory DietPlanMealModel.fromJson(Map<String, dynamic> map) {
    return DietPlanMealModel(map["mealId"], map["dietPlanId"]);
  }
}