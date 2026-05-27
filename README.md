# APBDtut7-ang-19c

Communication in this project relies on DTO's structure


Madels.cs
  Classes in this file directly map to tables in a relational database. I put it all in one file for easier read and modification which was very handy
  Instead of writing raw SQL, I used navigation properties(ICollections<T>) for simpler understanding the relationships between entities. 

Context.cs
  Inside we use class "AppDbContext" class to inherit from DbContext, It acts as bridge between the application and the SQL server
    Mechanics:
    Composites Primary Key (.HasKey(pc => new { pc.PCID, pc.ComponentCode }))
    Constraints
    Sending Data


DTO.cs
  Classes format the input and output data of the  API. It protects agains "over-posting" and infinite loop issues during JSON serialization.
    Mechanics:
    Division into specific perspectives: "CreatePcDto" for receiving data and "PcWithComponentsDto for casting full nested structure for GET request

  
Service.cs
  Seperates database operations logic from the controllers. It is injected throu the IPcService inteface. 
    Mechanics: All queries are asynchronous, which prevents blocking the main application thread while waiting for the DataBase response


Controller.cs
  The PCsController exposes endpoints for clients. Thanks to [Route("api/[controller]")] it reacts specyficly to HTTP verbs
    Mechanics:
      GET
      POST
      DELETE


Program.cs
  Starting class for application. Registers dependencies here to enable Dependency Injection throughout the rest of the system
    Mechanics:
      Database connection
      Service Injection (AddScope<IPcService, PcService>)



  
