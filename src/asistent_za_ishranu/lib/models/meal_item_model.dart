import 'package:asistent_za_ishranu/models/base_model.dart';

class MealItemModel extends BaseModel {
  int? foodProductId;
  double? amount;
  int? id;
  String? foodProduct;

  MealItemModel(this.foodProductId, this.amount);

  @override
  Map<String, dynamic> toJson() {
    return {"foodProductId": foodProductId, "amount": amount};
  }

  factory MealItemModel.fromJson(Map<String, dynamic> map) {
    return MealItemModel(map["foodProductId"], map["amount"].toDouble());
  }
}