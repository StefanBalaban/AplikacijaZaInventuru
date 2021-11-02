import 'dart:convert';

import 'package:asistent_za_ishranu/models/base_model.dart';

class UserWeightEvidentationRequest extends BaseModel {
  int? id;
  int? userId;
  DateTime? evidentationDate;
  double? weight;

  UserWeightEvidentationRequest(this.userId, this.evidentationDate, this.weight,
      [this.id]);

  UserWeightEvidentationRequest.forListResponse(this.id, this.evidentationDate);

  @override
  Map<String, dynamic> toJson() {
    return {
      "id": id,
      "userId": userId,
      "evidentationDate": evidentationDate!.toIso8601String(),
      "weight": weight
    };
  }

  factory UserWeightEvidentationRequest.fromJson(Map<String, dynamic> map) {
    return UserWeightEvidentationRequest(
        map['userWeightEvidention']['userId'],
        DateTime.parse(map['userWeightEvidention']['evidentationDate']),
        map['userWeightEvidention']['weight'].toDouble());

  }

  factory UserWeightEvidentationRequest.fromListJson(Map<String, dynamic> map) {
    return UserWeightEvidentationRequest(map['userId'], DateTime.parse(map['evidentationDate']), map["weight"].toDouble(), map["id"]);
  }

  static List<UserWeightEvidentationRequest> fromJsonList(Map<String, dynamic> map) {
    return map["userWeightEvidentions"]
        .map<UserWeightEvidentationRequest>(
            (dietPlan) => UserWeightEvidentationRequest.fromListJson(dietPlan))
        .toList();
  }

  static List<UserWeightEvidentationRequest> resultListFromJson(String json) {
    final data = JsonDecoder().convert(json);
    return UserWeightEvidentationRequest.fromJsonList(data);
  }

  static UserWeightEvidentationRequest resultFromJson(String json) {
    final data = JsonDecoder().convert(json);
    return UserWeightEvidentationRequest.fromJson(data);
  }
}
