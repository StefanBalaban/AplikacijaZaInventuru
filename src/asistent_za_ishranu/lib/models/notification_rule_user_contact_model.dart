import 'package:asistent_za_ishranu/models/base_model.dart';

class NotificationRuleUserContactModel extends BaseModel {
  int? userContactInfosId;
  int? notificationRuleId;  

  NotificationRuleUserContactModel(this.userContactInfosId, [this.notificationRuleId]);

  @override
  Map<String, dynamic> toJson() {
    return {"userContactInfosId": userContactInfosId, "notificationRuleId": notificationRuleId};
  }

  factory NotificationRuleUserContactModel.fromJson(Map<String, dynamic> map) {
    return NotificationRuleUserContactModel(map["userContactInfosId"], map["notificationRuleId"]);
  }
}