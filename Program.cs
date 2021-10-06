
using Microsoft.EntityFrameworkCore;
using SimpleApi.Data;
using SimpleApi.Extensions;
using SimpleApi.Interfaces;
using SimpleApi.Models;
using SimpleApi.Repositories;

var builder = WebApplication.CreateBuilder(args);
// Customer have nothing to do with what is scan but the assembly that is being scan
builder.Services.AddEndpointDefinitions(typeof(Customer));

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

 builder.Services.AddDbContext<DataContext>(x => 
            x.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
            );


var app = builder.Build();


app.UseEndpointDefinitions();

app.Run();

