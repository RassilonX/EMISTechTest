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