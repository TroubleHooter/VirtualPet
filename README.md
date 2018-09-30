**Solution** <br /><br />

 *Decreasing mood, increasing Hunger* <br />
  The increasing hunger and decreasing mood/happiness are calculated on the fly by using the LastUpdated field and the current DateTime. The client can calculate the stats     locally from the date it last received the pet so it can continue offline.<br /><br />
  On any action (Stroke/feed) the server uses the LastUpdated DateTime and calculates the pet’s stat before implementing the stat modifier from the pets associated profile     before saving the new stats and updating the LastUpdated Datetime with the current DateTime.

 *Profiles*<br />
  There is currently 1 profile but more can be added and associated with a pet to change the mood and hunger time multipliers and the action (Stroke and Feeding) modifiers.

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
- GET all pets for user 
  -```https://localhost:44319/virtual-pet/pet/pets/{UserId}```
- GET pet by id 
  -```https://localhost:44319/virtual-pet/pet/{PetId}```
- POST Stroke pet by id 
  -```https://localhost:44319/virtual-pet/pet/Stroke/{PetId}```
- POST Feed pet by id 
  -```https://localhost:44319/virtual-pet/pet/Feed/{PetId}```
- POST Create a new pet 
  -```https://localhost:44319/virtual-pet/pet/```

**Create Pet Post Data**
```
{
	"Name":"Fido",
	"ProfileId":1,
	"PetType":"Dog",
	"PetTypeId":1,
	"OwnerId":1
}```


