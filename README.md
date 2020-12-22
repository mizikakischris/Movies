## Table of Contents
1. [General Info](#general-info)
2. [Installation](#installation)
3. [Notes](#notes)
### General Info
***
This project is a simple REST Web Api which implements CRUD operations in order to obtain movies and actors records.<br/>
The project is developed in Visual Studio 2019 with .NET Core 3.1 Framework and EF Core for database queries.<br/>
SQL Server is used as a database provider.<br/>
Repository pattern is used to access databases from the domain model layer.<br/>
Windows Event Log is used to log events.<br/>
Swagger UI is used to expose the respective endpoints.

## Installation
***
Download the project <br/>
Before you begin change the connection string in appSettings.json file.<br/>
Run the following command in Package Manager Console
```
PM> update-database
```
This will create the database **Movie_Dev** and apply all migrations. Migrations will seed the data as well.<br/>
**Set up the loggers:** <br/>
Open Powershell as administrator and run the following commands. You can find the commands in the appSettings.json file.<br/>


```
PS C:\WINDOWS\system32> New-EventLog -LogName MoviesApi -Source MoviesApi
```
Build the project.
## Notes
***
You have to be authorized in order to get successful response when you are trying to access the **"/api/Movies/{movieId}"** resource.<br/>
Run the project, In the Swagger UI you have to register first <br/>
Get a bearer token from the **"/api/User/authenticate"** <br/>
Then go up to the right of the page and click the Authorize button.<br/>
In the text input below enter 'Bearer' [space] and then your token 
```
Example: "Bearer 12345678f "
```
You can use Postman collection and environment to test the endpoints. Open fileExplorer and navigate to the the project.Open folder **..Movies\Docs\Postman**



