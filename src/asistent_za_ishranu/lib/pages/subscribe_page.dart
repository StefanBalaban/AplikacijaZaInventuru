import 'package:asistent_za_ishranu/models/subscribe_model.dart';
import 'package:asistent_za_ishranu/services/api_service.dart';
import 'package:asistent_za_ishranu/services/auth_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter_stripe/flutter_stripe.dart';

class SubscribePage extends StatefulWidget {
  @override
  _SubscribePageState createState() => _SubscribePageState();
}

class _SubscribePageState extends State<SubscribePage> {
  CardFieldInputDetails? _card;
  String _email = '';
  bool? _saveCard = false;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(),
      body: Column(
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: [
          Padding(
            padding: EdgeInsets.all(16),
            child: TextField(
              decoration: InputDecoration(
                  hintText: 'Email', labelText: "30 KM", helperText: "Cijena"),
              readOnly: true,
            ),
          ),
          Padding(
            padding: EdgeInsets.all(16),
            child: CardField(
              onCardChanged: (card) {
                setState(() {
                  _card = card;
                });
              },
            ),
          ),
          Padding(
            padding: EdgeInsets.all(16),
            child: ElevatedButton(
              onPressed: _card?.complete == true ? _handlePayPress : null,
              child: Text('Uplati'),
            ),
          )
        ],
      ),
    );
  }

  Future<void> _handlePayPress() async {
    if (_card == null) {
      return;
    }
    var apiService = ApiService();
    var authService = AuthService();
    await apiService.post(
        "api/usersubscription",
        SubscribeModel(
                authService.userId,
                DateTime.now().toIso8601String(),
                DateTime.now().toIso8601String(),
                DateTime(DateTime.now().year, DateTime.now().month,
                    DateTime.now().day).toIso8601String()
        )
            .modelToJson());
    Navigator.of(context).pushReplacementNamed("/home");
  }
}
