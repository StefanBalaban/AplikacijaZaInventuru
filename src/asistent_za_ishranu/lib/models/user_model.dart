import 'dart:convert';

import 'package:asistent_za_ishranu/models/base_model.dart';
import 'package:asistent_za_ishranu/models/user_contact_model.dart';

class UserModel extends BaseModel {
  String firstName;
  String lastName;
  List<UserContactModel>? userContacts;
  int? id;

  UserModel(this.firstName, this.lastName);
  UserModel._fromResponse(this.firstName, this.lastName, this.id,
      [this.userContacts]);

  @override
  Map<String, dynamic> toJson() {
    return {"firstName": firstName, "lastName": lastName};
  }

  factory UserModel.fromJson(Map<String, dynamic> map) {
    return UserModel._fromResponse(
        map['user']['firstName'],
        map['user']['lastName'],
        map['user']["id"],
        map["user"]["userContactInfos"]
            .map<UserContactModel>((e) => UserContactModel.fromJson(e)).toList());
  }

  static UserModel resultFromJson(String json) {
    final data = JsonDecoder().convert(json);
    return UserModel.fromJson(data);
  }
}
