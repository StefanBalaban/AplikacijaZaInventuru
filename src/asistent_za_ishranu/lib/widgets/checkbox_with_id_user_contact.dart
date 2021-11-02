import 'package:asistent_za_ishranu/models/user_contact_model.dart';
import 'package:asistent_za_ishranu/util/wrapped_boolean.dart';
import 'package:flutter/material.dart';

import 'checkbox_with_id_meal.dart';

class CheckBoxWithIdUserContact extends StatefulWidget {
  UserContactModel? userContactModel = null;
  WrappedBoolean? wrappedBoolean = WrappedBoolean(false);

  CheckBoxWithIdUserContact(this.userContactModel);

  @override
  _CheckBoxWithIdUserContactState createState() => _CheckBoxWithIdUserContactState(userContactModel, wrappedBoolean);
}

class _CheckBoxWithIdUserContactState extends State<CheckBoxWithIdUserContact> {
  UserContactModel? userContactModel = null;
  WrappedBoolean? wrappedBoolean;

  _CheckBoxWithIdUserContactState(this.userContactModel, this.wrappedBoolean);
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
        title: Text(userContactModel!.contact!),
      ),
    );
  }
}