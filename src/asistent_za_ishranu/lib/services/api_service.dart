import 'dart:convert';
import 'dart:io';
import 'package:asistent_za_ishranu/services/auth_service.dart';
import 'package:http/http.dart' as http;

class ApiService {
  AuthService _authService = AuthService();
  String _baseApiUrl = "https://192.168.0.20:5099";

  Future<String> post(String path, String body) async {
    var client = new HttpClient();
    client.badCertificateCallback = ((X509Certificate cert, String host, int port) => true);

    HttpClientRequest request = await client.postUrl(Uri.parse("$_baseApiUrl/$path"));
    request.headers.set('Content-Type', 'application/json');
    request.headers.set('Authorization', 'Bearer ${_authService.getToken()}');
    request.add(utf8.encode(body));
    HttpClientResponse result = await request.close();


    if (result.statusCode == 200) {
      final contents = StringBuffer();
      await for (var data in result.transform(utf8.decoder)) {
        contents.write(data);
      }
      return contents.toString();
    }
    else {
      throw HttpException("Request failed. Status code: ${result.statusCode }");
    }
  }

  Future<String> put(String path, String body) async {
    var client = new HttpClient();
    client.badCertificateCallback = ((X509Certificate cert, String host, int port) => true);

    HttpClientRequest request = await client.putUrl(Uri.parse("$_baseApiUrl/$path"));
    request.headers.set('Content-Type', 'application/json');
    request.headers.set('Authorization', 'Bearer ${_authService.getToken()}');
    request.add(utf8.encode(body));
    HttpClientResponse result = await request.close();


    if (result.statusCode == 200) {
      final contents = StringBuffer();
      await for (var data in result.transform(utf8.decoder)) {
        contents.write(data);
      }
      return contents.toString();
    }
    else {
      final contents = StringBuffer();
      await for (var data in result.transform(utf8.decoder)) {
        contents.write(data);
      }
      throw HttpException("Request failed. Status code: ${result.statusCode } ${contents}");
    }
  }

  Future<String> get(String path) async {
    var client = new HttpClient();
    client.badCertificateCallback = ((X509Certificate cert, String host, int port) => true);

    HttpClientRequest request = await client.getUrl(Uri.parse("$_baseApiUrl/$path"));
    request.headers.set('Content-Type', 'application/json');
    request.headers.set('Authorization', 'Bearer ${_authService.getToken()}');
    HttpClientResponse result = await request.close();

    if (result.statusCode == 200) {
      final contents = StringBuffer();
      await for (var data in result.transform(utf8.decoder)) {
        contents.write(data);
      }
      return contents.toString();
    }
    else {
      throw HttpException("Request failed. Status code: ${result.statusCode }");
    }
  }

  Future<String> delete(String path) async {
    var client = new HttpClient();
    client.badCertificateCallback = ((X509Certificate cert, String host, int port) => true);

    HttpClientRequest request = await client.deleteUrl(Uri.parse("$_baseApiUrl/$path"));
    request.headers.set('Content-Type', 'application/json');
    request.headers.set('Authorization', 'Bearer ${_authService.getToken()}');
    HttpClientResponse result = await request.close();

    if (result.statusCode == 200) {
      final contents = StringBuffer();
      await for (var data in result.transform(utf8.decoder)) {
        contents.write(data);
      }
      return contents.toString();
    }
    else {
      throw HttpException("Request failed. Status code: ${result.statusCode }");
    }
  }

}