import 'dart:convert';

class AuthRequest {
  String username;
  String password;
  String? email;


  AuthRequest(this.username, this.password);

  AuthRequest.register(this.username, this.password, this.email);

  @override
  Map<String, dynamic> toJson() {
    return {"username": username, "password": password, "email": email};
  }

  static String loginToJson(AuthRequest login) {
    final jsonData = login.toJson();
    return json.encode(jsonData);
  }
}
