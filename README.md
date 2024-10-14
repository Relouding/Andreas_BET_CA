### Application setup instructions
1. Download and open the project in visual studio code.
2. Install necessary packages listed under "additional external libraries/packages used".
- ```dotnet tool install --global dotnet-ef```
- ```dotnet add package MySQL.EntityFrameworkCore```
- ```dotnet add package Microsoft.EntityFrameworkCore.Design```
- ```dotnet add package Swashbuckle.AspNetCore.Annotations```
3. Edit appsettings.json "DefaultConnections" on line 10 as needed.
- Server
- Database
- User
- Password
4. Open MySQL workbench.
5. Run the command ```"dotnet ef database update"``` in the terminal.

### Instructions to run the application
1. Run the command ```"dotnet run"``` in the terminal to start the project.
2. Go to the URL "https://localhost:PORT/swagger/" to view all endpoints in the Swagger UI.

### Instructions to create needed Migrations
1. Change appsettings.json "DefaultConnections" (line 10) as needed.
2. Run the command ```"dotnet ef database update"``` in the terminal.

#### If the previous steps dosen't work try:
1. Delete "Migrations" folder.
2. Run the command ```"dotnet ef migrations add -c DataContext Initial"``` in the terminal.
3. Run the command ```"dotnet ef database update"``` in the terminal.

### Connection String structure for MySQL Database connection
- Server: the address of the MySQL server.
- Database: the name of the database you want to connect to.
- User: the username used to connect to the database.
- Password: the password for the user.

In the "appsettings.json" file, it looks like this:

```"DefaultConnection": "server=localhost;database=devhouse;user=USERNAME;password=PASSWORD"```

In the "Program.cs" file, it looks like this:

```var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");```

### Endpoint/s JSON requests
ProjectTypes data:
```
{
  "id": 1,
  "name": "Game"
}
```
```
{
  "id": 2,
  "name": "Program"
}
```

Roles data:
```
{
  "id": 1,
  "name": "Frontend Developer"
}
```
```
{
  "id": 2,
  "name": "Backend Developer"
}
```

Teams data:
```
{
  "id": 1,
  "name": "A Team"
}
```
```
{
  "id": 2,
  "name": "B Team"
}
```

Projects data:
```
{
  "id": 1,
  "name": "Project A",
  "projectTypeId": 1,
  "teamId": 1
}
```
```
{
  "id": 2,
  "name": "Project B",
  "projectTypeId": 2,
  "teamId": 2
}
```

Developer data:
```
{
  "id": 1,
  "firstname": "John",
  "lastname": "Doe",
  "roleId": 1,
  "teamId": 1
}
```
```
{
  "id": 2,
  "firstname": "Jane",
  "lastname": "Doe",
  "roleId": 2,
  "teamId": 2
}
```

### Additional external libraries/packages used
- Microsoft.EntityFrameworkCore.Design      ```8.0.5```
- MySQL.EntityFrameworkCore                 ```8.0.2```
- Swashbuckle.AspNetCore                    ```6.4.0```
- Swashbuckle.AspNetCore.Annotations        ```6.6.2```

### Swagger UI overview image
![](https://imgur.com/lwcV8NN.jpg)