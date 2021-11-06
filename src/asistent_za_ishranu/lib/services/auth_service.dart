import 'dart:io';

import 'package:asistent_za_ishranu/models/auth_result.dart';
import 'package:asistent_za_ishranu/models/auth_request.dart';
import 'package:http/http.dart' show Client;
import 'dart:convert';
import 'package:jwt_decoder/jwt_decoder.dart';

class AuthService {
  final String baseUrl = "http://10.0.2.2:5001";
  HttpClient client = HttpClient();
  AuthResult _authResult = AuthResult(false, "", "");
  static final AuthService _instance = AuthService._privateConstructor();
  int userId = 0;
  String _username = "";
  String _password = "";

  AuthService._privateConstructor();

  factory AuthService() {
    return _instance;
  }



  Future<AuthResult?> loginAction(AuthRequest authRequest) async {
    client.badCertificateCallback =
        ((X509Certificate cert, String host, int port) => true);

    HttpClientRequest request =
        await client.postUrl(Uri.parse("$baseUrl/api/auth/login"));
    request.headers.set('Content-Type', 'application/json');
    request.add(utf8.encode(AuthRequest.loginToJson(authRequest)));
    HttpClientResponse result = await request.close();

    if (result.statusCode == 200) {
      final contents = StringBuffer();
      await for (var data in result.transform(utf8.decoder)) {
        contents.write(data);
      }
      _username = authRequest.username;
      _password = authRequest.password;
      _authResult = AuthResult.authResultFromJson(contents.toString());
      return _authResult;
    }
    if (result.statusCode == 400) {
      return AuthResult(false, "", "");
    } else {
      final contents = StringBuffer();
      await for (var data in result.transform(utf8.decoder)) {
        contents.write(data);
      }

      throw HttpException(
          "Request failed. Status code: ${result.statusCode} ${contents}");
    }
  }

  Future<AuthResult?> registerAction(AuthRequest authRequest) async {
    
    client.badCertificateCallback =
        ((X509Certificate cert, String host, int port) => true);
    HttpClientRequest request =
        await client.postUrl(Uri.parse("$baseUrl/api/auth/register"));
    request.headers.set('Content-Type', 'application/json');
    request.add(utf8.encode(AuthRequest.loginToJson(authRequest)));
    HttpClientResponse result = await request.close();

    if (result.statusCode == 200) {
      final contents = StringBuffer();
      await for (var data in result.transform(utf8.decoder)) {
        contents.write(data);
      }
      _authResult = AuthResult.authResultFromJson(contents.toString());
      return _authResult;
    }
    if (result.statusCode == 400) {
      return AuthResult(false, "", "");
    } else {
      final contents = StringBuffer();
      await for (var data in result.transform(utf8.decoder)) {
        contents.write(data);
      }

      throw HttpException(
          "Request failed. Status code: ${result.statusCode} ${contents}");
    }
  }

  bool isLoggedIn() {
    return _authResult.result;
  }

  void verifySession() {
    if (isExpiredToken()) {
      loginAction(AuthRequest(_username, _password));
    }
  }

  bool isExpiredToken() {
    return JwtDecoder.isExpired(_authResult.token);
  }

  String getToken() {
    return _authResult.token;
  }
}
