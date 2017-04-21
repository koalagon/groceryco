# Running program
This program reads all json file with relative path. So, you should run the program in VS or run it directly in the folder which exe file resides. 

# Prerequisite
- VS2013 or higher version
- .NET 4.0 or higher version

# Architecture & Project structure
- I follow Onion Architecture. You could see the onion architecture from http://jeffreypalermo.com/blog/the-onion-architecture-part-1/
- Kiosk : presentation layer
- Domain : domain model, services implements all business logics
  * Models : all entities, VO
  * Repositories : generic repository
  * Services : servcie layer facade
- Infrastructure : code that implements an interface

# Business Logic & Design choice


# Assumption & Limitation
- Product price, sub-total, grand-total are between decimal.MinValue and decimal.MaxValue
- Each product can have multiple promotions. e.g. Apple can have the on-sale promotion and group-sale promotion. But, system will gurantee the lowest price promotion. It can be changed to the highest price promotion based on configuration. You can change the configuration on App.Config file.
- Each product can have a lot of promotions on each promomtion type. But the promotion date should not overlay.
- All marketing team input data is correct. However, I added a couple of validation logics.
- All input file should be provided in the right place. If not, system will say error message.

# NuGet
- Newtonsoft.Json : read and parse JSON data
- NLog : logging
- Moq : unit test mocking framework
