# BlogEngine

Blog engine is an small project created in visual studio with c# and .net 6 to expose some API to manage posts and his comments.

<b>Additional information</b>

- Hours to complete: 16 hours.
- Users in the system (hardcoded) are in the root folder in the users.txt file.

## Prerequisites

Either if you are going to run the project locally or is going to request the available exposed services in the internet, the one prerequisites for both ways is PostMan.

Please download Postman to request the apis and se the response. You can download the postman or run postman directly in the browser. For these, please use this link:
https://www.postman.com/downloads/

# Test online

The Base URL to test and use the api is: https://blogengineapi20230227155243.azurewebsites.net

### <b><i>Configuring postman</i></b>

1. Import the swagger.json file into Postman so it can create the APIs. The swagger file is located in the project root folder.

![import-swagger](https://blogenginesta.blob.core.windows.net/images/import-swagger-api.PNG)

2. With this, we are going to have al the endpoints of the project and can replace the {{baseUrl}} for the one described earlier or you can configure postman variable to understand {{baseURL}}.

3. There is only one endpoint you need to add manually to request the token needed to communicate with the other endpoints. -- if testing localy, change the url for the local host project url.

![get-token](https://blogenginesta.blob.core.windows.net/images/request-token.PNG)

4. Finally, you need to pass the authorization header for the apis, taking the access token provided in the step before, so the new endpoint can validate the user and role of the users.

![send-token](https://blogenginesta.blob.core.windows.net/images/add-token-to-request.PNG)


# Test Locally
### <b><i>Prerequisites</i></b>

This is the list of required software needed to run the project and test the different apis running locally and using the Swagger page display after running the project:

* .Net 6. Download the SDK for the latest version: https://dotnet.microsoft.com/en-us/download/dotnet/6.0

* Visual Studio Community 2022. Go to visual studio page to download the latest stable version: https://visualstudio.microsoft.com/vs/community/

* A SQL database server of your choice, for this project we used the version SQL Server Developer version:
 https://www.microsoft.com/es-es/sql-server/sql-server-downloads

* To connect to the database server, download a client of your preference, you can use the SQL Server Management Studio (SSMS). Also recommend when you finished install the SQL Server Developer version.

### <b><i>Configure the project for running</i></b>

After installing all the required software and tools, you need to do the following steps before running the application:

1. Inside the Project Directory, goes to (downloadedpath)\BlogEngine\BlogEngineDb\Scripts and open the file InitialCreate.sql.

2. Connect to your database server with the SSMS and create a new database. (choose the name that you want).

3. In the recently created database, run the scripts of the step 1.

4. Open the file BlogEngine.sln located in the downloaded root folder so it can open with VisualStudio.

5. In VisualStudio, expand the BlogEngineApi project and locate the appsettings.Development.json file and change the Default connection string to the connection string of your database.

6. Run the project.

### <b><i>Test the project using postman</i></b>

For the test of the apis in the project, we are going to use POSTMAN, the steps are the same as the one described in the Test Online secction, with the difference that the base Url is the one in the browser for localhost.


