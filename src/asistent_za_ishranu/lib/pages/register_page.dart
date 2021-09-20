import 'package:asistent_za_ishranu/models/auth_result.dart';
import 'package:asistent_za_ishranu/models/auth_request.dart';
import 'package:asistent_za_ishranu/models/user_model.dart';
import 'package:asistent_za_ishranu/services/api_service.dart';
import 'package:asistent_za_ishranu/services/auth_service.dart';
import 'package:email_validator/email_validator.dart';
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

  Future<void> register() async {
    {
      var authService = AuthService();
      AuthResult? response = await authService.registerAction(
          AuthRequest.register(_usernameController.text,
              _passwordController.text, _usernameController.text));
      if (response!.result) {
        var result = await _apiService.post(
            "api/user",
            UserModel(_usernameController.text, _usernameController.text)
                .modelToJson());
        authService.userId = UserModel.resultFromJson(result).id!;
        Navigator.of(context).pushReplacementNamed("/subscribe");
      } else
        _errorMessageController.text = "Invalid credentials";
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: Text("Registracija"),
        ),
        body: Form(
            key: _formKey,
            child: SingleChildScrollView(
                child: Center(
                    child: Column(
              children: <Widget>[
                ConstrainedBox(
                    constraints: BoxConstraints.tight(const Size(200, 50)),
                    child: TextFormField(
                      // The validator receives the text that the user has entered.
                      validator: (value) {
                        if (value == null || value.isEmpty || !EmailValidator.validate(value)) {
                          return 'Unesite email adresu';
                        }
                      },
                      controller: _usernameController,
                    )),
                ConstrainedBox(
                    constraints: BoxConstraints.tight(const Size(200, 50)),
                    child: TextFormField(
                      // The validator receives the text that the user has entered.
                      validator: (value) {
                        if (value == null || value.isEmpty) {
                          return 'Unesite lozinku';
                        }
                      },
                      controller: _passwordController,
                      keyboardType: TextInputType.visiblePassword,
                      obscureText: true,
                    )),
                ElevatedButton(
                  onPressed: () {
                    if (_formKey.currentState!.validate()) {
                      register();
                    }
                  },
                  child: const Text('Unos'),
                )
              ],
            )))));
  }
}
