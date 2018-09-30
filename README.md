**Patterns used**
- CQRS with MediatR 
- Some DDD concepts 

**Tech**
- .Net Framework 4.6.2
- Asp.net API Core targeting .net 4.6.2
- Entity Framework core 2.1.3 using SQLServer

**Configuration**
-	MSSQL DB Connection string is in the Config at VirtualPet.WebApi. appsettings.json
-	Database should create the first time the app is run by the context.Database.EnsureCreated(); 
within the Startup.cs or alternatively you can run the migration file from the VirtualPet.Application https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/

**Not implemented**
-	User Authentication or Authorisation (userId/OwnerId passed from client) 
-	Integration Tests
-	Events Controller (though the events are created for all Inserts/updates to the DB
-	logging

**Endpoints**
- Get all pets for user
```https://localhost:44319/virtual-pet/pet/pets/{UserId}```
- Get pet by id
```https://localhost:44319/virtual-pet/pet/{PetId}```
- Stroke pet by id
```https://localhost:44319/virtual-pet/Stroke/{PetId}```
- Feed pet by id
```https://localhost:44319/virtual-pet/Feed/{PetId}``
- Create a new pet 
```https://localhost:44319/virtual-pet/pet/```

**Post Data**
```
{
	"Name":"Fido",
	"ProfileId":1,
	"PetType":"Dog",
	"PetTypeId":1,
	"OwnerId":1
}```


