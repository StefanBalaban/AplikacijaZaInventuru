import 'package:asistent_za_ishranu/models/auth_result.dart';
import 'package:asistent_za_ishranu/models/auth_request.dart';
import 'package:http/http.dart' show Client;

class AuthService {
  final String baseUrl = "http://192.168.0.20:5001";
  Client client = Client();
  AuthResult _authResult = AuthResult(false, "", "");
  static final AuthService _instance = AuthService._privateConstructor();
  int userId = 0;

  AuthService._privateConstructor();

  factory AuthService() {
    return _instance;
  }

  Future<AuthResult?> loginAction(AuthRequest authRequest) async {
    final response = await client.post(Uri.parse("$baseUrl/api/auth/login"),
        headers: {"content-type": "application/json"},
        body: AuthRequest.loginToJson(authRequest));
    if (response.statusCode == 200) {
      _authResult = AuthResult.authResultFromJson(response.body);
      return _authResult;
    } else {
      throw Exception(response.body);
    }
  }
  Future<AuthResult?> registerAction(AuthRequest authRequest) async {
    final response = await client.post(Uri.parse("$baseUrl/api/auth/register"),
        headers: {"content-type": "application/json"},
        body: AuthRequest.loginToJson(authRequest));
    if (response.statusCode == 200) {
      _authResult = AuthResult.authResultFromJson(response.body);
      return _authResult;
    } else {
      throw Exception(response.body);
    }
  }

  bool isLoggedIn() {
    return _authResult.result;
  }

  String getToken() {
    return _authResult.token;
  }
}
