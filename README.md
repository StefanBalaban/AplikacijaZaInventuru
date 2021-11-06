# Startup
To startup the Database Server, the API and the IdentityServer simply run `docker-compose up` from the root directory. This will start the SQL Server on port 1433, the API on port 5098 on localhost and the IdentityServer on port 5001. 
To build the app after changes run `docker-compose build` in the root directory.

The mobile application can be executed using android emulator and as such requires no additional setup. To use the mobile application on an actual device you will need to change the baseUrl in the auth_service.dart and api_service.dart classes to match the IP address of your local machine.

# Use case
## Mobile
To use the mobile app it is first necessary to start the docker image as describe above. After which, to use the application is is necessary to register an account. To do so simply press the button on the splash page and click on the "Resgistracija" text under the login form.

Enter email, name and password. Password should be at least 8 characters with one uppercase letter, one lowercase letter, one number and one symbol.
To subscribe use the following card number 4242424242424242, a valid future expiry date and any three numbers as the CV. This will complete the registration and subscription process and log in the user into the app.
