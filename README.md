# _C#-Hair Salon MVC && Database _

#### _Single Webpage for Epicodus, 12.7.2018_

#### By _**Glen Sale**_

## Description

_Create a program that allow to CRUD databases for customers and allow new employee to input there information to be hired:_

  ### _Basic Structure of Directory for Database using MVC and Mysql
  ToDoList.Solution
  ├── ToDoList
  │   ├── Controllers
  │   │   ├── CategoriesController.cs
  │   │   ├── HomeController.cs
  │   │   └── ItemsController.cs
  │   ├── Models
  │   │   ├── Category.cs
  │   │   └── Item.cs
  │   ├── Program.cs
  │   ├── Startup.cs
  │   ├── ToDoList.csproj
  │   └── Views
  │       ├── Categories
  │       │   ├── Index.cshtml
  │       │   ├── New.cshtml
  │       │   └── Show.cshtml
  │       ├── Home
  │       │   └── Index.cshtml
  │       └── Items
  │           ├── DeleteAll.cshtml
  │           ├── New.cshtml
  │           └── Show.cshtml
  └── ToDoList.Tests
      ├── ControllerTests
      │   ├── HomeControllerTests.cs
      │   └── ItemsControllerTests.cs
      ├── ModelTests
      │   ├── CategoryTests.cs
      │   └── ItemTests.cs
      └── ToDoList.Tests.csproj


* For More Click: 
https://www.learnhowtoprogram.com/c/database-basics-ee7c9fd3-fcd9-4fff-8b1d-5ff7bfcbf8f0/creating-the-database-object




### Specs
| Spec | Employee | Client | Result |
| :-------------     | :------------- | :------------- | :------------- |
| ## Program allows user to add new Hire(Employee) and Clients** | Employee: "Test" | Client: "client1" |  Result: Alex has client1  |
| ## Program allows to delete employee ** | Employee: "Test2"  | Client: "client1" |  Result: "Test2 and Client1 are now deleted" |
| ## Program allows to Update new employee** | Employee : "New hire added" | --------- |  Result: New hire added |
| ## Program allows to Update new client** | Employee : ---------- | Client: New client added |  Result: New client added |



## Setup/Installation Requirements
* Clone this repository https://github.com/glenpogz36/HairSalon.Solutions
* _To edit the project, open the project in your preferred text editor._
*  Download
* "Microsoft.AspNetCore" Version="1.1.2"
*  "Microsoft.AspNetCore.Http" Version="1.1.2"
*  "Microsoft.AspNetCore.Mvc" Version="1.1.3".
*  "Microsoft.AspNetCore.StaticFiles" Version="1.1.3"
*  "MAMP"
* _To download create A file inside the Project Directory creating Program.cs and Startup.cs_>
* _To run the program, first navigate to the location of the Program.cs and Startup.cs file_:
* _Open the App MAMP and change the Preference Port Apache: 8888 and MySql: 8889_


## How To use MySql using (GITBASH, POWERSHELL, MONO)
$ Mysql -uroot -uproot -P(insert path)
$ CREATE DATABASE (name of database project);
$ USE (name of database project);
$ CREATE TABLE categories (id serial PRIMARY KEY, name VARCHAR(255));
$ CREATE TABLE tasks (id serial PRIMARY KEY, description VARCHAR(255));

## How to run and test
* $ dotnet restore download MVC packages : to download MVC packages
* $ dotnet restore : to download AspNetCore
* $ dotnet build : to start build and check for errors
* $ dotnet run : to run localhost:5000
* _To run the tests, use these commands inside your (ProjectName).Tests directory using: $ cd (ProjectName).Tests $ dotnet test_

## Technologies Used

* _C#_
* _.NET_
* _AspNETCore_
* _Atom_
* _GitHub_
* _MySql_
* _MAMP_


### License

*This software is licensed under the MIT license.*

Copyright (c) 2018 ** _Glen Sale_ **
