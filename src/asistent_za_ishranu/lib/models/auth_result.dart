import 'dart:convert';

class AuthResult {
   bool result;
   String token;
   String username;


   AuthResult(this.result, this.token, this.username);

   factory AuthResult.fromJson(Map<String, dynamic> map) {
      return AuthResult(map['result'],map['token'], map['username']);
   }

   static AuthResult authResultFromJson(String json) {
      final data = JsonDecoder().convert(json);
      return AuthResult.fromJson(data);
   }
}