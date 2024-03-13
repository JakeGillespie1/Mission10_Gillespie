using Microsoft.EntityFrameworkCore;
using Mission10_GillespieAPI.Data;

/*
This creates an instance of the WebApplication class using the command-line arguments 'args' 
*/
var builder = WebApplication.CreateBuilder(args);

/*
Add services to the container by accessing the dependency injection container
The dependency injection (DI) container is key to the DI design pattern. It is responsible for managing the installation and lifetime of objects (dependencies) in an application
and for providing those dependencies to other objects that require them. Here is how it works:
    Registration: container is configured and the developers register the types  (classes, interfaces, etc.) that represent dependencies and their lifetime's. Registration also tells 
        the container how to create instances of those types when they are requested.
    Resolution: When an object (in our case, a web app) requests a dependency, the container checks its configurationa and creates an instance of the requested dependency according
        to the specified registration.
    Injection: once the dependency is created, the container injects it into the requesting object (web page).
This DI design makes it easier to manage and update dependencies within an application. ASP.NET uses a built-in container.
 */
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*
Allow Cross Origing Resource Sharing
Builder: an instance of webapplication
Services: This property provides access to the dependency injection container.
AddCors: A method used to register the CORS services with the dependency injection container to make them available throughout your app.
    It is a security feature that prevents scripts from running in a web page from making requests to a different domain than the one that
    originally served the web page. Basically, it helps to prevent XSS...You can provide the CORS that specify specific allowed origins.
 */
builder.Services.AddCors();

/*
This line of code configures Entity Framework Core to use SQLite
builder: see above
services: see above
AddDbContext<>: Registers the DbContext type with the dependency injection container. DbContext is a class provided by Entity Framework Core
    that represents a session with the database and allows you to query and save data.
options => ... :BowlingConnection"])):
    builder.Configuration[]: this accesses the configuration settings of your application and retrieves the connection string from the configuration
        Connection strings usually contain information like the database provider, server address, database name, authentication creds, etc.
    UseSqlite: This method configures DbContext to use the specified database provider.
*/
builder.Services.AddDbContext<BowlingLeagueContext>(options =>
    options.UseSqlite(builder.Configuration["ConnectionStrings:BowlingConnection"])
);

/*
builder.Services: As before, this accesses the Services property of the WebApplication instance
AddScoped<>(): This method registers a services witht he dependency injection container. By registering the services in the angle brackets, you are telling
    the dependency injection container that whenever an object of type IBowlingRepository is requested it should instantiate and provide and object of type 
    EFBowlingRepository. The addition of AddScoped indicates that instances of EFBowlingRepository will be scoped to the lifetime of the HTTP request.
*/
builder.Services.AddScoped<IBowlingRepository, EFBowlingRepository>();

/*
This line of code finalizes the configuration of the web application using the 'builder' instance and builds the application
It returns an instance of 'WebApplication', which represents the configured and built web application.
Build(): this is a method of the WebApplication object that applies the configuration settings and services that have been registered with the builder instance
    through the DI container and then constructs the final WebApplication instance..
*/
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/*
This line tells the program that it can communicate and share data with this origin
(the origin here is our front end 'client-side.'
 */
app.UseCors(p => p.WithOrigins("http://localhost:3000"));

/*
If a user types in http:// on accident, then it will auto-redirect to https://
*/
app.UseHttpsRedirection();

/*
This adds middleware to the application pipeline that enables authorization checks for incoming HTTP requests. 
*/
app.UseAuthorization();

/*
This line is used to configure the routing system to map incoming HTTP requests to controller actions based on the URL path.
It auto-discovers and registers your controllers in the app. It sets up convention-based routing and handles requests for controller
actions in your application. 
 */
app.MapControllers();

/*
Run the application
*/
app.Run();
