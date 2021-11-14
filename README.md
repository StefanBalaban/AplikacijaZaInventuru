# Startup
## Backend
1. `cd [App root dir]`
2. `docker-compose build`
3. `docker-compose up`


This will start the SQL Servers, the API and the IdentityServer simply run `docker-compose up` from the root directory. The SQL Servers are on ports 1401 (used by the API) and 1402 (used by the IS), the API is on port 5098 and the IdentityServer is on port 5001. 

## Mobile
1. `cd src/asistent_za_ishranu`
2. `flutter pub get`
3. `flutter emulator --launch [Emulator device Id]`
4. `flutter run --no-sound-null-safety`

The mobile application can be executed using android emulator and as such requires no additional setup. To get the emulator device id simply run `flutter emulators`. To use the mobile application on an actual device you will need to change the baseUrl in the auth_service.dart and api_service.dart classes to match the IP address of your local machine.

## Desktop
1. `cd src\WinUI`
2. `dotnet publish -c Release`
3. `dotnet bin\Release\net6.0-windows\publish\WinUI.dll`

This will start the Winforms Desktop application.

# Usage
## Mobile
To use the mobile app it is first necessary to start the docker image as describe above. After which, to use the application is is necessary to register an account. To do so simply press the button on the splash page and click on the "Resgistracija" text under the login form.

Enter email, name and password. Password should be at least 8 characters with one uppercase letter, one lowercase letter, one number and one symbol.
To subscribe use the following card number 4242424242424242, a valid future expiry date and any three numbers as the CV. This will complete the registration and subscription process and log in the user into the app.

The features are located under the hamburger menu.

## Desktop
To use the desktop application you first have to register with the mobile application. The first user registered on the mobile application will have the Administrator role and will be able to use the desktop application features.