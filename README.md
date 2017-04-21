# Running program
This program reads all json file with relative path. So, you should run the program in VS or run it directly in the folder which exe file resides. 

# Prerequisite
- .NET 4.0 or higher version (I used VS2013 for this project)

# Architecture & Project structure
- I followed Onion Architecture. I always use this layer pattern. http://jeffreypalermo.com/blog/the-onion-architecture-part-1/
- Kiosk : presentation layer. 
- Domain : domain model, services implements all business logics
  * Models : all entities, VO
  * Repositories : generic repository
  * Services : service layer facade
- Infrastructure : code has dependency on external environments. this layer implements an domain interface.

# Business Logic & Design choice
- I found all domain models. Based on the domain models, I desigend and implemented all source codes.
- Domain service layer uses Facade pattern. This service layer works like a bridge between Presentation and Domain model, also repositoy access and domain model access happen in here.
- Repository pattern is used to access data. Because I used generic repository interface and all basic concrete classes are implemented, the data storage can be easily changed from file system to DB or REST server.
- Strategy pattern and factory pattern is used to determin pricing policy. **If there's multiple promotions, e.g. on-sale and group-sale per product, the application will choose the lowest price strategy as a default.** This strategy can be changed from App.Config file for simplicity.

# Assumption & Limitation
- Product price, sub-total, grand-total are between decimal.MinValue and decimal.MaxValue
- Product name is Unique Identifier. (Of course, it is not good idea, but it's for readability). The product name is case-sensitive.
- Each product can have multiple promotions. e.g. Apple can have the on-sale promotion and group-sale promotion. But, system will gurantee the lowest price promotion. It can be changed to the highest price promotion based on configuration. You can change the configuration on App.Config file.
- Each product can have a lot of promotions on each promomtion type. But the promotion date should not overlay.
- All marketing team input data is correct. However, I added a couple of validation logics.
- All input file should be provided in the right place. If not, system will say error message.

# NuGet
- Newtonsoft.Json : read and parse JSON data
- NLog : logging
- Moq : unit test mocking framework
