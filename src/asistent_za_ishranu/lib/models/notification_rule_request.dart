import 'dart:convert';
import 'package:asistent_za_ishranu/models/base_model.dart';
import 'diet_plan_meal_model.dart';
import 'notification_rule_user_contact_model.dart';

class NotificationRuleRequest extends BaseModel {
  int? id;
  int? userId;
  int? foodProductId;
  List<NotificationRuleUserContactModel>? notificationRuleUserContacts;


  NotificationRuleRequest(this.foodProductId, this.notificationRuleUserContacts, [this.id, this.userId]);

  NotificationRuleRequest.forListResponse(this.id, this.foodProductId);

  @override
  Map<String, dynamic> toJson() {
    return {
      "id": id,
      "userId": userId,
      "foodProductId": foodProductId,
      "NotificationRuleUserContactInfos": notificationRuleUserContacts
    };
  }

  factory NotificationRuleRequest.fromJson(Map<String, dynamic> map) {
    List<NotificationRuleUserContactModel> userContacts =  map['notificationRule']['notificationRuleUserContactInfos'].map<NotificationRuleUserContactModel>((e) => NotificationRuleUserContactModel.fromJson(e)).toList();
    return NotificationRuleRequest(
        map['notificationRule']['foodProductId'], userContacts);
  }

  factory NotificationRuleRequest.fromListJson(Map<String, dynamic> map) {
    return NotificationRuleRequest.forListResponse(
        map['id'], map['foodProductId']);
  }

  static List<NotificationRuleRequest> fromJsonList(Map<String, dynamic> map) {
    return map["notificationRules"].map<NotificationRuleRequest>((foodProduct) => NotificationRuleRequest.fromListJson(foodProduct)).toList();
  }

  static List<NotificationRuleRequest> resultListFromJson(String json) {
    final data = JsonDecoder().convert(json);    
    return NotificationRuleRequest.fromJsonList(data);
  }

  static NotificationRuleRequest resultFromJson(String json) {
    final data = JsonDecoder().convert(json);
    return NotificationRuleRequest.fromJson(data);
  }
}
