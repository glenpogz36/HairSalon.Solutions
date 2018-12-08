# _C#-Hair Salon MVC && Database _

#### _Single Webpage for Epicodus, 12.7.2018_

#### By _**Glen Sale**_

## Description

_Create a program that allow to CRUD databases for customers and allow new employee to input there information to be hired:_


### _Basic Structure of Directory for Program and Testing_

__Word-Counter.Solution
├── WordCounter
│   ├── WordCounter.csproj
│   └── obj
│       ├── WordCounter.csproj.nuget.cache
│       ├── WordCounter.csproj.nuget.g.props
│       ├── WordCounter.csproj.nuget.g.targets
│       └── project.assets.json
└── WordCounter.Tests
    ├── WordCounter.Tests.csproj
    └── obj
        ├── WordCounter.Tests.csproj.nuget.cache
        ├── WordCounter.Tests.csproj.nuget.g.props
        ├── WordCounter.Tests.csproj.nuget.g.targets
        └── project.assets.json__

* _For More click the link :_  https://www.learnhowtoprogram.com/c/c-basics-and-testing/mstest-configuration-and-setup_        


### _Basic Structure of Directory for MVC and Controll Testing
_ToDoList.Solution
├── ToDoList
│   ├── Controllers
│   │   ├── HomeController.cs
│   │   └── ItemsController.cs
│   ├── Models
│   │   ├── Category.cs
│   │   └── Item.cs
│   ├── Program.cs
│   ├── Startup.cs
│   ├── ToDoList.csproj
│   └── Views
│       ├── Home
│       │   └── Index.cshtml
│       └── Items
│           ├── DeleteAll.cshtml
│           ├── Index.cshtml
│           ├── New.cshtml
│           └── Show.cshtml
└── ToDoList.Tests
    ├── ControllerTests
    │   ├── HomeControllerTests.cs
    │   └── ItemsControllerTests.cs
    ├── ModelTests
    │   ├── CategoryTests.cs
    │   └── ItemTests.cs
    └── ToDoList.Tests.csproj_

  * _For More click the link :_ https://www.learnhowtoprogram.com/c/mvc-web-applications/objects-within-objects-setup_

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

    * _For More click the link :_ https://www.learnhowtoprogram.com/c/database-basics-ee7c9fd3-fcd9-4fff-8b1d-5ff7bfcbf8f0/creating-the-database-object





### Specs
| Spec | Input | Output | Result |
| :-------------     | :------------- | :------------- | :------------- |
| ** Program allows user to search Employee name** | Input: "Alex" | Output: "Alex Gonzales, Alex Rodriguez" |  Result: 2 Alex found |
| ** Program allows to delete employee ** | Input: "Alex Gonzales"  | Output: "Alex Gonzales, Alex Rodriguez" |  Result: "Alex Gonzales deleted" |
| ** Program allows to Update new employee** | Input : "New hire added" | Output: "New hire added"|  Result: New hire added |



## Setup/Installation Requirements
* Clone this repository https://github.com/glenpogz36/HairSalon.Solutions
* _To edit the project, open the project in your preferred text editor._
* _Download
 "Microsoft.AspNetCore" Version="1.1.2"
  "Microsoft.AspNetCore.Http" Version="1.1.2"
  "Microsoft.AspNetCore.Mvc" Version="1.1.3".
  "Microsoft.AspNetCore.StaticFiles" Version="1.1.3"
  "MAMP"_
* _To download create A file inside the Project Directory creating Program.cs and Startup.cs>
* _To run the program, first navigate to the location of the Program.cs and Startup.cs file:
* _Open the App MAMP and change the Preference Port Apache: 8888 and MySql: 8889

_For more info to Install and Use MAMP Mysql commands to create your databases : https://www.learnhowtoprogram.com/c/database-basics-ee7c9fd3-fcd9-4fff-8b1d-5ff7bfcbf8f0/sql-basics-599a9044-bbe7-4b58-bfe3-ba9a3502f761


$ dotnet restore download MVC packages : to download MVC packages
$ dotnet restore : to download AspNetCore
$ dotnet build : to start build and check for errors
$ dotnet run : to run localhost:5000
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
