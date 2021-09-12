import 'dart:convert';

import 'package:asistent_za_ishranu/models/base_model.dart';

class UserModel extends BaseModel {
  String firstName;
  String lastName;
  int? id;

  UserModel(this.firstName, this.lastName);
  UserModel._fromResponse(this.firstName, this.lastName, this.id);

  @override
  Map<String, dynamic> toJson() {
    return {"firstName": firstName, "lastName": lastName};
  }

  factory UserModel.fromJson(Map<String, dynamic> map) {
    return UserModel._fromResponse(map['user']['firstName'],map['user']['lastName'], map['user']["id"]);
  }

  static UserModel resultFromJson(String json) {
    final data = JsonDecoder().convert(json);
    return UserModel.fromJson(data);
  }
}
