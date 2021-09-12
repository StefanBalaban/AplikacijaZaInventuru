import 'dart:convert';

import 'package:asistent_za_ishranu/models/base_model.dart';

class SubscribeModel extends BaseModel {
  int userId;
  String paymentDate;
  String begginDate;
  String endDate;

  SubscribeModel(this.userId, this.paymentDate, this.begginDate, this.endDate);

  @override
  Map<String, dynamic> toJson() {
    return {"userId": userId, "paymentDate": paymentDate, "begginDate": begginDate, "endDate": endDate};
  }

  factory SubscribeModel.fromJson(Map<String, dynamic> map) {
    return SubscribeModel(
        map['userId'], map['paymentDate'], map["begginDate"], map["endDate"]);
  }
}
