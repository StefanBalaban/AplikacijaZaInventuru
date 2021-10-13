import 'package:asistent_za_ishranu/models/meal_request.dart';
import 'package:flutter/material.dart';

class CheckBoxWithId extends StatefulWidget {
  MealRequest? mealRequest = null;
  WrappedBoolean? wrappedBoolean = WrappedBoolean(false);

  CheckBoxWithId(this.mealRequest);

  @override
  _CheckBoxWithIdState createState() => _CheckBoxWithIdState(mealRequest, wrappedBoolean);
}

class _CheckBoxWithIdState extends State<CheckBoxWithId> {
  MealRequest? mealRequest = null;
  WrappedBoolean? wrappedBoolean;

  _CheckBoxWithIdState(this.mealRequest, this.wrappedBoolean);
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

class WrappedBoolean {
  bool value;

  WrappedBoolean(this.value);
}