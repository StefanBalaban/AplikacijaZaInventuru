import 'package:asistent_za_ishranu/pages/diet_plan/diet_plan_create_page.dart';
import 'package:asistent_za_ishranu/pages/diet_plan/diet_plan_details_page.dart';
import 'package:asistent_za_ishranu/pages/diet_plan/diet_plan_list_page.dart';
import 'package:asistent_za_ishranu/pages/food_product/food_product_create_page.dart';
import 'package:asistent_za_ishranu/pages/food_product/food_product_details_page.dart';
import 'package:asistent_za_ishranu/pages/food_product/food_product_update_page.dart';
import 'package:asistent_za_ishranu/pages/food_product/food_product_list_page.dart';
import 'package:asistent_za_ishranu/pages/home_page.dart';
import 'package:asistent_za_ishranu/pages/landing_page.dart';
import 'package:asistent_za_ishranu/pages/login_page.dart';
import 'package:asistent_za_ishranu/pages/meal/meal_create_page.dart';
import 'package:asistent_za_ishranu/pages/meal/meal_details_page.dart';
import 'package:asistent_za_ishranu/pages/meal/meal_list_page.dart';
import 'package:asistent_za_ishranu/pages/meal/meal_update_page.dart';
import 'package:asistent_za_ishranu/pages/register_page.dart';
import 'package:asistent_za_ishranu/pages/subscribe_page.dart';
import 'package:flutter/material.dart';
import 'package:flutter_stripe/flutter_stripe.dart';

Future<void> main() async {
  WidgetsFlutterBinding.ensureInitialized();
  Stripe.publishableKey = "pk_test_51JTpLzCR8h0CzCWUXdpv88XXQVBuAlXAdAATs6chkOHwCoRbXKAyxF7DCyuyVYPMIDHRmnCyj2BmomZ2rkBGBl0600WF1YEPhp";
  Stripe.merchantIdentifier = 'MerchantIdentifier';
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({Key? key}) : super(key: key);

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Asistent za ishranu',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: LandingPage(),
      routes: {
        "/landing":(context)=> LandingPage(),
        "/login":(context)=>LoginPage(),
        "/home":(context)=> HomePage(),
        "/register":(context)=> RegisterPage(),
        "/subscribe":(context)=> SubscribePage(),
        FoodProductCreatePage.routeName:(context)=> FoodProductCreatePage(),
        FoodProductListPage.routeName:(context)=> FoodProductListPage(),
        FoodProductDetailsPage.routeName:(context)=>FoodProductDetailsPage(),
        FoodProductUpdatePage.routeName:(context)=>FoodProductUpdatePage(),
        MealCreatePage.routeName:(context)=> MealCreatePage(),
        MealListPage.routeName:(context)=> MealListPage(),
        MealDetailsPage.routeName:(context)=>MealDetailsPage(),
        MealUpdatePage.routeName:(context)=>MealUpdatePage(),
        DietPlanListPage.routeName:(context)=>DietPlanListPage(),
        DietPlanCreatePage.routeName:(context)=> DietPlanCreatePage(),
        DietPlanDetailsPage.routeName:(context)=>DietPlanDetailsPage()
      },
    );
  }
}
