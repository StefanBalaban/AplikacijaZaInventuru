import 'dart:convert';

class BaseModel {

  BaseModel();

  Map<String, dynamic> toJson() {
    return {"": 1};
  }

  String modelToJson() {
    final jsonData = this.toJson();
    return json.encode(jsonData);
  }

  factory BaseModel.fromJson(Map<String, dynamic> map) {
    return BaseModel();
  }
}