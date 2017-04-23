# Running program
This program reads all json files with the relative path. So, you should run the program in VS or run it directly in the folder which exe file resides. 

# Prerequisite
- .NET 4.0 or higher version (I used VS2013 for this project)

# Architecture & Project structure
- I followed Onion Architecture and typically use this layer pattern. http://jeffreypalermo.com/blog/the-onion-architecture-part-1/
- Kiosk : presentation layer. 
- Domain : domain model, services implements all business logics
  * Models : all entities, VO
  * Repositories : generic repository
  * Services : service layer facade
- Infrastructure : the code has dependency on external environments. This layer implements a domain interface.

# Business Logic & Design choice
- I found all domain models. Based on the domain models, I desigend and implemented all source codes.
- Domain service layer uses Facade pattern. This service layer works like a bridge between Presentation and Domain model, also the repositoy access and domain model access happen in here.
- Repository pattern is used to access data. Because I used generic repository interface and all basic concrete classes are implemented, the data storage can be easily changed from file system to DB or REST server.
- Strategy pattern and factory pattern is used to determin pricing policy. **If there's multiple promotions, i.e. on-sale and group-sale per product, the application will choose the lowest price strategy as default.** This strategy can be changed from App.Config file for simplicity.
- I didn't use DI framework, but it could be used from Kiosk application. In this case, IRepository and ISaleService implementation can be injected.

![domain model](http://ec2-35-163-38-171.us-west-2.compute.amazonaws.com:3000/images/model1.png)
![sequence add product](http://ec2-35-163-38-171.us-west-2.compute.amazonaws.com:3000/images/model2.png)
![sequence check out](http://ec2-35-163-38-171.us-west-2.compute.amazonaws.com:3000/images/model3.png)

# Assumption & Limitation
- Product price, sub-total, and grand-total are between decimal.MinValue and decimal.MaxValue
- Product name is Unique Identifier. (Typically, this is not the best practice but was selected for readability). The product name is case-sensitive.
- I assumed that there's a only console receipt printer. If there's more printer types, other type printers inherit Receipt class. Also, the printing function will be moved to the domain service layer facade, and printing facade method can be provided.
- Each product can have multiple promotions. For example, Apple can have an on-sale promotion and group-sale promotion. But, system will gurantee the lowest price promotion. It can be changed to the highest price promotion based on the configuration. You can change the configuration in the App.Config file.
- Each product can have a lot of promotions on each promomtion type. But the promotion date should not overlay.
- All marketing team input data is correct. However, I added a couple of validation logics.
- All input file should be provided in the right place. If not, system will display an error message.

# NuGet
- Newtonsoft.Json : read and parse JSON data
- NLog : logging
- Moq : unit test mocking framework
