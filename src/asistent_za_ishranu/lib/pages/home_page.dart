import 'package:flutter/material.dart';

class HomePage extends StatefulWidget {
  const HomePage({Key? key}) : super(key: key);

  @override
  _HomePageState createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text("Asistent za ishranu")
      ),
      drawer: Drawer(
        child: ListView(
          children: [
            ListTile(
              title: Text("Meni", style: TextStyle(color: Colors.white),),
              tileColor: Colors.blue,

              onTap: () {
              },
            ),
            ListTile(
              title: Text("Prehrambeni proizvodi"),
              onTap: () {
                Navigator.of(context).pushNamed("/food_product_list");
              },
            ),

          ],
        ),
      ),
      body:  Center(child: Icon(Icons.food_bank_outlined, size: 100,)),
    );
  }
}
