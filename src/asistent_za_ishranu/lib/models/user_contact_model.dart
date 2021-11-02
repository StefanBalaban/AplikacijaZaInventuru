import 'dart:convert';

import 'package:asistent_za_ishranu/models/base_model.dart';

class UserContactModel extends BaseModel {
  String contact;
  int? id;

  UserContactModel(this.contact);
  UserContactModel._fromResponse(this.contact, this.id);

  @override
  Map<String, dynamic> toJson() {
    return {"contact": contact};
  }

  factory UserContactModel.fromJson(Map<String, dynamic> map) {
    return UserContactModel._fromResponse(map['contact'], map["id"]);
  }

  static UserContactModel resultFromJson(String json) {
    final data = JsonDecoder().convert(json);
    return UserContactModel.fromJson(data);
  }
}