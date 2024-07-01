# Project Details

This is an MVC project using .Net 6. 

## Database Setup
This project uses Entity Framework for a SQL database. A migration has been provided, as well as a connection string to use.

Before setting up the database please make sure you have Entity Framework installed on your machine. The version used in this project is v7.0.20, which is the last version that supports .Net 6.

To deploy the migration from visual studio:
- Navigate to the Package Manager Console
- Set both the Default Project in the console window, and the Startup Project for the solution to be the Database Project
- Input the command Update-Database
- The migration should run and setup a database in your localhost called fourteen-fish which has some seed data in the tables

To deploy the migration from a bash or cmd window:
- Navigate to the Database project in your window
- Run the command `dotnet ef database update`
- The migration should run and setup a database in your localhost called fourteen-fish which has some seed data in the tables


## Integration Tests
This project has integration tests that rely on the database being setup before running.
Please ensure the database has been set up before running the tests, otherwise they will all fail.

If they are still failing, check the following:
- The database has been successfully deployed to your machine
- The connection string in appsettings.json in the DALTests project is the same as the DB_CONNECTION_STRING variable in the launchsettings.json file in the FullStackTechTest project
- You have .Net6 installed on your machine