## FuelWerx

### Solution Structure

The solution is based on an ASP.NET Boilerplate template and has several projects:

* FuelWerx.Application - contains business logic
* FuelWerx.Core - domain objects
* FuelWerx.EntityFramework - data access and EF code first migrations
* FuelWerx.Web - the main web application
* FuelWerx.WebApi - the web api module
    * This module automatically maps ASP.NET WebApi endpoints to all public methods on the business logic classes in FuelWerx.Application

### ASP.NET Boilerplate (ABP)

The project is based on an older version of [ABP](http://aspnetboilerplate.com). Upgrading it to the latest breaks a lot of stuff. This will have to be done at a later date.

### Entity Framework

ASP.NET Boilerplate has a custom DbContext class that will only retrieve items belonging to the logged in Tenant. If you're writing a part of the application that requires access to all data you must use the `FuelWerxVanillaDbContext`.
