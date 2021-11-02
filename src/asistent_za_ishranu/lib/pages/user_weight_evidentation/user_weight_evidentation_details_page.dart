import 'package:asistent_za_ishranu/models/user_weight_evidentation_request.dart';
import 'package:asistent_za_ishranu/models/diet_plan_request.dart';
import 'package:asistent_za_ishranu/services/api_service.dart';
import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

import 'user_weight_evidentation_update_page.dart';

class UserWeightEvidentationDetailsPage extends StatefulWidget {
  const UserWeightEvidentationDetailsPage({Key? key}) : super(key: key);

  static const routeName = '/user_weight_evidentation_details';

  @override
  _UserWeightEvidentationDetailsPageState createState() => _UserWeightEvidentationDetailsPageState();
}

class _UserWeightEvidentationDetailsPageState extends State<UserWeightEvidentationDetailsPage> {  
  Future<UserWeightEvidentationRequest> getItem(id) async {
    return UserWeightEvidentationRequest.resultFromJson(await ApiService().get("api/userweightevidention/$id"));
  }

  Future<void> deleteItem(id) async {
    var apiService = ApiService();
    await apiService.delete("api/userweightevidention/$id");
  }

  @override
  Widget build(BuildContext context) {
    final id = ModalRoute.of(context)!.settings.arguments as int;
    return Scaffold(
        appBar: AppBar(
          title: Text("Detalji evidencije kilaže"),
        ),
        body: FutureBuilder<UserWeightEvidentationRequest>(
            future: getItem(id),
            builder: (BuildContext context,
                AsyncSnapshot<UserWeightEvidentationRequest> snapshot) {
              if (snapshot.connectionState == ConnectionState.waiting) {
                return Form(
                    child: Column(
                  children: [],
                ));
              } else {
                return SingleChildScrollView(
                  child: Form(
                    child: Column(
                  children: [                    
                    TextFormField(
                      initialValue:  snapshot.data!.weight.toString(),
                      decoration: InputDecoration(labelText: "Kilaža"),
                      readOnly: true,
                    ),
                    TextFormField(
                      initialValue: "${DateFormat('dd.MM.yyyy').format(snapshot.data!.evidentationDate!)}",
                      decoration: InputDecoration(labelText: "Datum evidencije"),
                      readOnly: true,
                    ),
                    Center(
                      child: ElevatedButton(
                        child: Text("Izmijeni"),
                        onPressed: () {
                          Navigator.of(context).pushNamed(
                              UserWeightEvidentationUpdatePage.routeName,
                              arguments: id).then((value) => setState((){}));
                        },
                      ),
                    ),
                    Center(
                      child: ElevatedButton(
                        child: Text("Izbriši"),
                        onPressed: () => showDialog<String>(
                          context: context,
                          builder: (BuildContext context) => AlertDialog(
                            title: const Text('Potvrda brisanja'),
                            content: const Text('Da li ste sigurni da želite obrisati stavku?'),
                            actions: <Widget>[
                              TextButton(
                                onPressed: () =>
                                    Navigator.pop(context, 'Ne'),
                                child: const Text('Ne'),
                              ),
                              TextButton(
                                onPressed: () async {
                                  await deleteItem(id);
                                  Navigator.pop(context, 'Da');
                                  Navigator.pop(context);
                                },
                                child: const Text('Da'),
                              ),
                            ],
                          ),
                        ),
                      ),
                    )
                  ],
                )));
              }
            }));
  }
}
