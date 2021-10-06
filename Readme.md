# Minimal API


## How I went about building it



## Entity Framework Core

```
dotnet add package Microsoft.EntityFrameworkCore --version 6.0.0-rc.1.21452.10
dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0.0-rc.1.21452.10
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 6.0.0-rc.1.21452.10
```
### Create DBContext
```
public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)        
{        
}
//Create the DataSet for the Employee         
public DbSet<Employee> Employee { get; set; }
```

### Add configuration to program.cs
```
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

 builder.Services.AddDbContext<DataContext>(x => 
            x.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
            );
```
### Install dotnet ef tool
dotnet tool install dotnet-ef -g
or upgrade
dotnet tool update --global dotnet-ef

### Add Migration and create database
```
dotnet ef migrations add initialCreate
dotnet ef database update
```


## Swagger
```
dotnet add package Swashbuckle.AspNetCore --version 6.2.2
```


## Dependancy Injection