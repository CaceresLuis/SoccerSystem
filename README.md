# SoccerSystem

## Table of Contents

1. [General Info](#general-info)
2. [Technologies](#technologies)
3. [Installation](#installation)

### General Info

---

A system to create soccer tournaments, register teams and organize them by groups, register matches, control punctuation and table of positions.
The system allows you to register administrators to supervise the tournaments and also users who can follow the results of the matches

### Screenshot

![Image text](/Resources/api.jpg "Api Project")
![Image text](/Resources/web.jpg "Web Project")

## Technologies And NuGets

---

- [NetCore](https://dotnet.microsoft.com/download/dotnet/3.1): Version 3.1
- [SqlServer Express](https://www.microsoft.com/en-us/Download/details.aspx?id=101064): Version 15.0.20

- [Swashbuckle.AspNetCore](https://swagger.io/): Version 6.2.1
- [Newtonsoft.Json](https://www.newtonsoft.com/json): Version 13.01
- [FluentValidation.AspNetCore](https://fluentvalidation.net/): Version 10.3.3
- [Microsoft.EntityFrameworkCore.Design](https://github.com/dotnet/efcore): Version 3.1.18
- [Microsoft.Extensions.Logging.Abstractions](https://github.com/dotnet/runtime): Version 5.0
- [AutoMapper.Extensions.Microsoft.DependencyInjection](https://automapper.org/): Version 10.3.3
- [Microsoft.EntityFrameworkCore.Tools](https://docs.microsoft.com/es-es/ef/core/): Version 3.1.18
- [Microsoft.AspNetCore.Http.Abstractions](https://github.com/aspnet/HttpAbstractions): Version 2.2
- [Microsoft.EntityFrameworkCore.SqlServer](https://docs.microsoft.com/es-es/ef/core/): Version 3.1.18
- [Microsoft.AspNetCore.Authentication.JwtBearer](https://github.com/dotnet/aspnetcore): Version 3.1.19
- [Microsoft.AspNetCore.Identity.EntityFrameworkCore](https://github.com/dotnet/aspnetcore): Version 3.1.18
- [Microsoft.AspNetCore.Identity](https://github.com/aspnet/Identity/tree/99f352a92f98af1059c87de07556719f1a22ce39): Version 2.2
- [MediatR.Extensions.Microsoft.DependencyInjection](https://github.com/jbogard/MediatR.Extensions.Microsoft.DependencyInjection): Version 9.0

## Installation

---

A little intro about the installation.

```
$ git clone https://github.com/CaceresLuis/SoccerSystem.git
```

Side information:

In the web project and api configure your connection to the database server in appsettings.json
},
    "AllowedHosts": "\*",
    "ConnectionStrings": {
    "DefaultConnection": "server=**SERVERNAME**;Database=SoccerSystem;Trusted_Connection=True;MultipleActiveResultSets=true"
}

In Startup.cs configure SecretKey for token creation :
string keySecret = "YOURSECRETKEY";
SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keySecret));
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = key,
        ValidateAudience = false,
        ValidateIssuer = false
    };
}).AddCookie();

You can use the WEB project or API individually or simultaneously
the WEB project and API share the code of the other projects of the system
