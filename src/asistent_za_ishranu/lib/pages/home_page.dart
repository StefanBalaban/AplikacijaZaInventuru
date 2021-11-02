import 'package:asistent_za_ishranu/pages/diet_plan/diet_plan_list_page.dart';
import 'package:asistent_za_ishranu/pages/diet_plan_period/diet_plan_period_list_page.dart';
import 'package:asistent_za_ishranu/pages/food_product/food_product_list_page.dart';
import 'package:asistent_za_ishranu/pages/food_stock/food_stock_list_page.dart';
import 'package:asistent_za_ishranu/pages/meal/meal_list_page.dart';
import 'package:asistent_za_ishranu/pages/notification_rule/notification_rule_list_page.dart';
import 'package:asistent_za_ishranu/pages/user_weight_evidentation/user_weight_evidentation_list_page.dart';
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
      appBar: AppBar(title: Text("Asistent za ishranu")),
      drawer: Drawer(
        child: ListView(
          children: [
            ListTile(
              title: Text(
                "Meni",
                style: TextStyle(color: Colors.white),
              ),
              tileColor: Colors.blue,
              onTap: () {},
            ),
            ListTile(
              title: Text("Prehrambeni proizvodi"),
              onTap: () {
                Navigator.of(context).pushNamed(FoodProductListPage.routeName);
              },
            ),
            ListTile(
              title: Text("Obroci"),
              onTap: () {
                Navigator.of(context).pushNamed(MealListPage.routeName);
              },
            ),
            ListTile(
              title: Text("Planovi ishrane"),
              onTap: () {
                Navigator.of(context).pushNamed(DietPlanListPage.routeName);
              },
            ),
            ListTile(
              title: Text("Period planova ishrane"),
              onTap: () {
                Navigator.of(context)
                    .pushNamed(DietPlanPeriodListPage.routeName);
              },
            ),
            ListTile(
              title: Text("Zaliha prehrambenih proizvoda"),
              onTap: () {
                Navigator.of(context)
                    .pushNamed(FoodStockListPage.routeName);
              },
            ),
            ListTile(
              title: Text("Notifikacijska pravila"),
              onTap: () {
                Navigator.of(context)
                    .pushNamed(NotificationRuleListPage.routeName);
              },
            ),
            ListTile(
              title: Text("Evidencija kilaže"),
              onTap: () {
                Navigator.of(context)
                    .pushNamed(UserWeightEvidentationListPage.routeName);
              },
            ),
          ],
        ),
      ),
      body: Center(
          child: Icon(
        Icons.food_bank_outlined,
        size: 100,
      )),
    );
  }
}
