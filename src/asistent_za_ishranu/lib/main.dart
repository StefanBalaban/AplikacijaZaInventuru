import 'package:asistent_za_ishranu/pages/diet_plan/diet_plan_create_page.dart';
import 'package:asistent_za_ishranu/pages/diet_plan/diet_plan_details_page.dart';
import 'package:asistent_za_ishranu/pages/diet_plan/diet_plan_list_page.dart';
import 'package:asistent_za_ishranu/pages/diet_plan/diet_plan_update_page.dart';
import 'package:asistent_za_ishranu/pages/diet_plan_period/diet_plan_period_create_page.dart';
import 'package:asistent_za_ishranu/pages/diet_plan_period/diet_plan_period_details_page.dart';
import 'package:asistent_za_ishranu/pages/diet_plan_period/diet_plan_period_list_page.dart';
import 'package:asistent_za_ishranu/pages/diet_plan_period/diet_plan_period_update_page.dart';
import 'package:asistent_za_ishranu/pages/food_product/food_product_create_page.dart';
import 'package:asistent_za_ishranu/pages/food_product/food_product_details_page.dart';
import 'package:asistent_za_ishranu/pages/food_product/food_product_update_page.dart';
import 'package:asistent_za_ishranu/pages/food_product/food_product_list_page.dart';
import 'package:asistent_za_ishranu/pages/food_stock/food_stock_create_page.dart';
import 'package:asistent_za_ishranu/pages/food_stock/food_stock_details_page.dart';
import 'package:asistent_za_ishranu/pages/food_stock/food_stock_list_page.dart';
import 'package:asistent_za_ishranu/pages/food_stock/food_stock_update_page.dart';
import 'package:asistent_za_ishranu/pages/home_page.dart';
import 'package:asistent_za_ishranu/pages/landing_page.dart';
import 'package:asistent_za_ishranu/pages/login_page.dart';
import 'package:asistent_za_ishranu/pages/meal/meal_create_page.dart';
import 'package:asistent_za_ishranu/pages/meal/meal_details_page.dart';
import 'package:asistent_za_ishranu/pages/meal/meal_list_page.dart';
import 'package:asistent_za_ishranu/pages/meal/meal_update_page.dart';
import 'package:asistent_za_ishranu/pages/notification_rule/notification_rule_create_page.dart';
import 'package:asistent_za_ishranu/pages/notification_rule/notification_rule_details_page.dart';
import 'package:asistent_za_ishranu/pages/notification_rule/notification_rule_list_page.dart';
import 'package:asistent_za_ishranu/pages/notification_rule/notification_rule_update_page.dart';
import 'package:asistent_za_ishranu/pages/register_page.dart';
import 'package:asistent_za_ishranu/pages/subscribe_page.dart';
import 'package:asistent_za_ishranu/pages/user_weight_evidentation/user_weight_evidentation_create_page.dart';
import 'package:asistent_za_ishranu/pages/user_weight_evidentation/user_weight_evidentation_details_page.dart';
import 'package:asistent_za_ishranu/pages/user_weight_evidentation/user_weight_evidentation_list_page.dart';
import 'package:asistent_za_ishranu/pages/user_weight_evidentation/user_weight_evidentation_update_page.dart';
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
    var routes = {
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
        DietPlanDetailsPage.routeName:(context)=>DietPlanDetailsPage(),
        DietPlanUpdatePage.routeName:(context)=>DietPlanUpdatePage(),
        DietPlanPeriodListPage.routeName:(context)=>DietPlanPeriodListPage(),
        DietPlanPeriodCreatePage.routeName:(context)=>DietPlanPeriodCreatePage(),
        DietPlanPeriodDetailsPage.routeName:(context)=>DietPlanPeriodDetailsPage(),
        DietPlanPeriodUpdatePage.routeName:(context)=>DietPlanPeriodUpdatePage(),
        FoodStockListPage.routeName:(context)=>FoodStockListPage(),
        FoodStockCreatePage.routeName:(context)=>FoodStockCreatePage(),
        FoodStockDetailsPage.routeName:(context)=>FoodStockDetailsPage(),
        FoodStockUpdatePage.routeName:(context)=>FoodStockUpdatePage(),
        NotificationRuleListPage.routeName:(context)=>NotificationRuleListPage(),
        NotificationRuleCreatePage.routeName:(context)=>NotificationRuleCreatePage(),
        NotificationRuleDetailsPage.routeName:(context)=>NotificationRuleDetailsPage(),
        NotificationRuleUpdatePage.routeName:(context)=>NotificationRuleUpdatePage(),
        UserWeightEvidentationListPage.routeName:(context)=>UserWeightEvidentationListPage(),
        UserWeightEvidentationCreatePage.routeName:(context)=>UserWeightEvidentationCreatePage(),
        UserWeightEvidentationDetailsPage.routeName:(context)=>UserWeightEvidentationDetailsPage(),
        UserWeightEvidentationUpdatePage.routeName:(context)=>UserWeightEvidentationUpdatePage()
      };
      
    return MaterialApp(
      title: 'Asistent za ishranu',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: LandingPage(),
      routes: routes,
    );
  }
}
