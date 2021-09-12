import 'package:asistent_za_ishranu/services/auth_service.dart';
import 'package:flutter/material.dart';
import 'home_page.dart';

class LandingPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text("Asistent za ishranu")),
      body: Center(
        child: ElevatedButton(
          onPressed: () {
            var authService = AuthService();
            if (authService.isLoggedIn()) {
              Navigator.of(context).pushReplacementNamed("/home");
            } else {
              Navigator.of(context).pushReplacementNamed("/login");
            }
          },
          child: Text("Pristupi"),
        ),
      ),
    );
  }
}
