import 'dart:convert';

import 'package:asistent_za_ishranu/models/base_model.dart';

class DietPlanPeriodRequest extends BaseModel {
  int? id;
  int? userId;
  int? dietPlanId;
  DateTime? startDate;
  DateTime? endDate;

  DietPlanPeriodRequest(this.dietPlanId, this.startDate, this.endDate,
      [this.id, this.userId]);

  DietPlanPeriodRequest.forListResponse(this.id, this.startDate);

  @override
  Map<String, dynamic> toJson() {
    return {
      "id": id,
      "userId": userId,
      "startDate": startDate!.toIso8601String(),
      "endDate": endDate!.toIso8601String(),
      "dietPlanId": dietPlanId
    };
  }

  factory DietPlanPeriodRequest.fromJson(Map<String, dynamic> map) {
    return DietPlanPeriodRequest(
        map['dietPlanPeriod']['dietPlanId'],
        DateTime.parse(map['dietPlanPeriod']['startDate']),
        DateTime.parse(map['dietPlanPeriod']['endDate']));

  }

  factory DietPlanPeriodRequest.fromListJson(Map<String, dynamic> map) {
    return DietPlanPeriodRequest(map['dietPlanId'], DateTime.parse(map['startDate']), DateTime.parse(map['endDate']), map["id"]);
  }

  static List<DietPlanPeriodRequest> fromJsonList(Map<String, dynamic> map) {
    return map["dietPlanPeriods"]
        .map<DietPlanPeriodRequest>(
            (dietPlan) => DietPlanPeriodRequest.fromListJson(dietPlan))
        .toList();
  }

  static List<DietPlanPeriodRequest> resultListFromJson(String json) {
    final data = JsonDecoder().convert(json);
    return DietPlanPeriodRequest.fromJsonList(data);
  }

  static DietPlanPeriodRequest resultFromJson(String json) {
    final data = JsonDecoder().convert(json);
    return DietPlanPeriodRequest.fromJson(data);
  }
}
