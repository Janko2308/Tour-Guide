# SWEN2 - Tour Planner Protocol
## Group
Ormianin Maksymilian, if21b233@technikum-wien.at, https://github.com/torz69  
Hu Janko, if21b056@technikum-wien.at, https://github.com/janko2308

## App architecture
### Main project
TODO

### Business Layer
TODO

### Data Access Layer
TODO

### Database connection
TODO

### Models
TODO

### Diagrams
TODO

## Use cases
TODO

## User Experience - Wireframes and final design
### Wireframes
TODO

### Final Design
TODO


## Library decisions
Throughout the project we used an abundance of libraries, the most important (with the motivation ) are listed below:
- itext7 - used whenever a report in pdf format, with a specific selected tour and/or all tours, needs to be generated. In the project it is used in the main view model.
-  log4net - used for logging activities, which creates a plain-text file, which contains every time whenever a log was called, with the exact time, level of severity and a message. Helps to analyse how the program works, and as such also helps to solve bugs. Implemented in Business Layer and primarily used inside of it, however it is also called upon in the main project.
-  System.Drawing.Common - used to parse images into a byte array, so that an image can be saved directly in a database, meaning no need in saving images locally. Byte array is used in the TourItem in .Model project, and worked upon in the business layer - to create a map from MapQuest API and later save it into an object of class TourItem, to save it in DAL into the database.
-  System.Configuration.ConfigurationManager - used to create config files which help to store information like keys or connectionstrings without the need to keep them hard-coded. Implemented in main project, used in DAL and BL.
-  Npgsql.EntityFrameworkCore.PostgreSQL - TODO
-  NUnit - used to create unit tests, allowing for an insight over different program functionalities. Allows for much easier analysis of program's behaviour, and as such makes development easier.
-  TODO

## Lessons learned
TODO

## Design patterns
TODO

## Unit testing
TODO

## Unique feature
For our unique feature we have decided to implement a dark mode functionality to allow for better user experience overall. TODO

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
