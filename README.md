## Features Included

### ASP.NET Core 5.0 MVC Project
- Slim Controllers using MediatR Library
- Permissions Management based on Role Claims
- Toast Notification (includes support for AJAX Calls too)
- Serilog
- ASP.NET Core Identity
- AdminLTE Bootstrap Template (Clean & SuperFast UI/UX)
- AJAX for CRUD (Blazing Fast load times)
- jQuery Datatables
- Select2
- Image Optimization
- Includes Sample CRUD Controllers / Views
- Active Route Tag Helper for UI
- RTL Support
- Complete Localization Support / Multilingual
- Clean Areas Implementation
- Dark Mode!
- Default Users / Roles Seeding at Startup
- Supports Audit Logging / Activity Logging for Entity Framework Core
- Automapper

### ASP.NET Core 5.0 WebAPI
- JWT & Refresh Tokens
- Swagger

(will be updated soon)



## About the Authors

### Mukesh Murugan
- Facebook - [lhp.tunglam](https://www.facebook.com/lhp.tunglam)



add-migration initial2 -context ApplicationDbContext
add-migration initialIdentity2 -context IdentityContext

update-database -context IdentityContext
update-database -context ApplicationDbContext