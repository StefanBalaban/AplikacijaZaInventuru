import 'package:asistent_za_ishranu/models/auth_result.dart';
import 'package:asistent_za_ishranu/models/auth_request.dart';
import 'package:asistent_za_ishranu/models/user_model.dart';
import 'package:asistent_za_ishranu/services/api_service.dart';
import 'package:asistent_za_ishranu/services/auth_service.dart';
import 'package:flutter/material.dart';

class RegisterPage extends StatefulWidget {
  const RegisterPage({Key? key}) : super(key: key);

  @override
  _RegisterPageState createState() => _RegisterPageState();
}

class _RegisterPageState extends State<RegisterPage> {
  final _formKey = GlobalKey<FormState>();
  TextEditingController _usernameController = TextEditingController();
  TextEditingController _passwordController = TextEditingController();
  TextEditingController _errorMessageController = TextEditingController();
  ApiService _apiService = ApiService();

  Future<void> _login() async {
    {
      var authService = AuthService();
      AuthResult? response = await authService.registerAction(
          AuthRequest.register(_usernameController.text, _passwordController.text, _usernameController.text));
      if (response!.result) {

        var result = await _apiService.post("api/user", UserModel(_usernameController.text, _usernameController.text).modelToJson());
        authService.userId = UserModel.resultFromJson(result).id!;
        Navigator.of(context).pushReplacementNamed("/subscribe");
      }
      else _errorMessageController.text = "Invalid credentials";
    }
  }
  @override
  Widget build(BuildContext context) {
    return Form(
          child: Scaffold(
              appBar: AppBar(
                title: Text("Registracija"),
              ),
              body: Column(
                key: _formKey,
                children: <Widget>[
                  TextFormField(
                    // The validator receives the text that the user has entered.
                    validator: (value) {
                      if (value == null || value.isEmpty) {
                        return 'Email korisnika';
                      }
                    },
                    controller: _usernameController,
                  ),
                  TextFormField(
                    // The validator receives the text that the user has entered.
                    validator: (value) {
                      if (value == null || value.isEmpty) {
                        return 'Lozinka';
                      }
                    },
                    controller: _passwordController,
                  ),
                  ElevatedButton(
                    onPressed: () {
                      _login();
                    },
                    child: const Text('Unos'),
                  )
                ],
              ))
    );
  }
}
