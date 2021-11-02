import 'package:asistent_za_ishranu/models/meal_request.dart';
import 'package:asistent_za_ishranu/util/wrapped_boolean.dart';
import 'package:flutter/material.dart';

class CheckBoxWithIdMeal extends StatefulWidget {
  MealRequest? mealRequest = null;
  WrappedBoolean? wrappedBoolean = WrappedBoolean(false);

  CheckBoxWithIdMeal(this.mealRequest);

  @override
  _CheckBoxWithIdMealState createState() => _CheckBoxWithIdMealState(mealRequest, wrappedBoolean);
}

class _CheckBoxWithIdMealState extends State<CheckBoxWithIdMeal> {
  MealRequest? mealRequest = null;
  WrappedBoolean? wrappedBoolean;

  _CheckBoxWithIdMealState(this.mealRequest, this.wrappedBoolean);
  @override
  Widget build(BuildContext context) {
    return Container(
      child: CheckboxListTile(        
        onChanged: (bool? value) {
          setState(() {
            this.wrappedBoolean!.value = value!;
          });
        },
        value: this.wrappedBoolean!.value,
        title: Text(mealRequest!.name!),
      ),
    );
  }
}

