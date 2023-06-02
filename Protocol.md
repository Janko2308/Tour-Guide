# SWEN2 - Tour Planner Protocol
## Group
Ormianin Maksymilian, if21b233@technikum-wien.at, https://github.com/torz69  
Hu Janko, if21b056@technikum-wien.at, https://github.com/janko2308

## App architecture
The app architecture uses the layered concept to allow for better understanding of app's functionalities, i.e. so that searching for specific methods is made easier, because they all have their own place in code.

### Main project
The main project contains all views (XAML-Files) and their view models, which contain the logic behind the frontend (like buttons, list views etc.). Main project also uses the business layer to pass advanced business logic and model project to create objects which it then uses to e.g. show tour items in list view and much more. The main project also contains a config file with connectionstring for DAL.

### Business Layer
Business layer contains advanced logic of the program including: creating maps, calculating child friendliness, logging activities, importing/exporting into csv, generating reports and so on. The Business Layer also contains a config file containing key for `MapQuestAPI`.

### Data Access Layer
Data access layer contains logic of database connection itself. The database itself is created within the implementation class, and all database queries to add/delete or modify items take place in that layer.

### Database connection
To build a connection to database, the `TourDbContext.cs` file creates 2 DbSets - `TourItems` and `TourLogItems`, and uses connection string from config file in main project. This allows to later use the database to utilize all of DAL's functionalities.

### Models
This project contains all models, structs and enums used in the project - tour items, tour logs, difficulty, transport and `TourCreation` which is a struct to help return computed values from `MapQuestAPI`'s response. Allows to use them on other layers of the program.

### Diagrams
**TODO**

## Use cases
**TODO**

## User Experience - Wireframes and final design
### Wireframes
**TODO**

### Final Design
**TODO**


## Library decisions
Throughout the project we used an abundance of libraries, the most important (with the motivation ) are listed below:
- itext7 - used whenever a report in pdf format, with a specific selected tour and/or all tours, needs to be generated. In the project it is used in the main view model.
-  log4net - used for logging activities, which creates a plain-text file, which contains every time whenever a log was called, with the exact time, level of severity and a message. Helps to analyse how the program works, and as such also helps to solve bugs. Implemented in Business Layer and primarily used inside of it, however it is also called upon in the main project.
-  System.Drawing.Common - used to parse images into a byte array, so that an image can be saved directly in a database, meaning no need in saving images locally. Byte array is used in the TourItem in .Model project, and worked upon in the business layer - to create a map from MapQuest API and later save it into an object of class TourItem, to save it in DAL into the database.
-  System.Configuration.ConfigurationManager - used to create config files which help to store information like keys or connectionstrings without the need to keep them hard-coded. Implemented in main project, used in DAL and BL.
-  NUnit - used to create unit tests, allowing for an insight over different program functionalities. Allows for much easier analysis of program's behaviour, and as such makes development easier.
-  **TODO**

## Lessons learned
- C#, .NET skills deepened, WPF skills developed and deepened.
- Using ORM to make working with the PostgreSQL easier.
- Creating config files
- Creating pdf files using c# code only
- Logging using a library log4net
- Using external API to create content (in this case the `MapQuestAPI`)
- Saving images into DB as byte array

## Design patterns
- Logger Factory - uses factory pattern to create loggers of many different types, as programming them individually would mean much more code.
- ObservableCollection - a collection using observer design pattern, which in return is invoked whenever a change is noted.

## Unit testing
The unit tests are divided into 4 different categories: Database mocks, Enum, Model and API tests.

### Database mocks
For the database mocks we decided to test simple functionalities like adding/removing or modifying an item in a mocked database/repository, to see whether the logic of DAL actually works as intended.

### Enum
Checks whether classes `Model.Enums.Difficulty` and `Model.Enums.Transport` are parsed and read from correctly.

### Model
Checks whether models are created correctly and that their assigned values can be read from. This means both `TourItem` and `TourLogs` get tested in this test file. The return struct used while creating maps is also used in this particular case (`Model.Structs.TourCreation`).

### API 
This test file checks whether a map is being created thanks to `MapQuestAPI`, asserting the returning struct `Model.Structs.TourCreation`. It checks also a situation once the API fails.

## Unique feature
For our unique feature we have decided to implement a dark mode functionality to allow for better user experience overall. **TODO**

## Tracked time
Overall we spent around 10h weekly since the half of March, which brings the total project tracked time to around 120 hours.  

Approximations for each item:
- GUI in General - 6h
- Tours - 20h
- TourLogs - 20h
- Full-text search - 10h
- Reports - 5h
- Import/Export - 10h
- Protocol - 10h
- Unit Tests - 10h
- Unique feature - 8h
- MapQuest implementation - 6h
- Logging - 5h
- Configuration files - 2h
- Creating models for the database - 2h
- Miscellaneous - around 3-4h

## Link to GIT repository
https://github.com/Janko2308/Tour-Planner
