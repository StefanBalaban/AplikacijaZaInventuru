import 'package:asistent_za_ishranu/models/auth_result.dart';
import 'package:asistent_za_ishranu/models/auth_request.dart';
import 'package:asistent_za_ishranu/pages/home_page.dart';
import 'package:asistent_za_ishranu/services/auth_service.dart';
import 'package:flutter/material.dart';

class LoginPage extends StatefulWidget {
  const LoginPage({Key? key}) : super(key: key);

  @override
  _LoginPageState createState() => _LoginPageState();
}

class _LoginPageState extends State<LoginPage> {
  final _formKey = GlobalKey<FormState>();
  TextEditingController _usernameController = TextEditingController();
  TextEditingController _passwordController = TextEditingController();
  TextEditingController _errorMessageController = TextEditingController();

  Future<void> _login() async {
    {
      var authService = AuthService();
      AuthResult? response = await authService.loginAction(
          AuthRequest(_usernameController.text, _passwordController.text));
      if (response!.result) Navigator.of(context).pushReplacementNamed("/home");
      else _errorMessageController.text = "Invalid credentials";
    }
  }

  @override
  Widget build(BuildContext context) {
    return Form(
        child: Scaffold(
            appBar: AppBar(
              title: Text("Login"),
            ),
            body: Column(
              key: _formKey,
              children: <Widget>[
                TextFormField(
                  // The validator receives the text that the user has entered.
                  validator: (value) {
                    if (value == null || value.isEmpty) {
                      return 'Korisnik';
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
                ),
                TextButton(onPressed: () {
                  Navigator.of(context).pushReplacementNamed("/register");
                },
                  child: const Text("Registracija"),
                )
              ],
            )));
  }
}
