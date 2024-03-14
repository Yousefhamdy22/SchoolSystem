#region Old
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.OpenApi.Models;
//using NLog.Web;
//using SchoolSystem.Helpers;
//using SchoolSystem.School.DAL.Data.Context;
//using SchoolSystem.School.DAL.Data.Identity;
//using SchoolSystem.School.DAL.GenaricRepo;
//using SchoolSystem.Services;
//using System.Reflection;
//using System.Text;

//var builder = WebApplication.CreateBuilder(args);

//// NLog: configure NLog for Dependency injection
//builder.Logging.ClearProviders();
//builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
//builder.Host.UseNLog();

//// Configure JWT settings
//builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

//// CORS setup

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll",
//        builder =>
//        {
//            builder
//            .AllowAnyOrigin()
//            .AllowAnyMethod()
//            .AllowAnyHeader();
//        });
//});


//// Swagger setup
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestApiJWT", Version = "v1" });
//    // Consider adding security definitions here if your Swagger UI should be secured with JWT
//});

//// Entity Framework and Identity
//builder.Services.AddDbContext<SchoolContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"))
//);
//builder.Services.AddIdentity<AppUser, IdentityRole>()
//           .AddEntityFrameworkStores<SchoolContext>();

//// JWT Authentication
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(o =>
//{
//    o.RequireHttpsMetadata = false;
//    o.SaveToken = false;
//    o.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidIssuer = builder.Configuration["JWT:Issuer"],
//        ValidAudience = builder.Configuration["JWT:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
//    };
//});

//// AutoMapper and Services
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddScoped<IAuthoServices, AuthoServices>();
//builder.Services.AddScoped(typeof(IGenaricRepo<>), typeof(GenaricRepo<>));

//// Controllers and Memory Cache
//builder.Services.AddControllers();
//builder.Services.AddDistributedMemoryCache();




//var app = builder.Build();

//// HTTP request pipeline configuration
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error");
//    app.UseHsts();
//}

//app.UseSwagger();
//app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestApiJWT v1"));

//app.UseCors("AllowAll");
//app.UseHttpsRedirection();
//app.UseAuthentication();
//app.UseAuthorization();
//app.MapControllers();

//app.Run();

#endregion

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using SchoolSystem.Helpers;
using SchoolSystem.School.DAL.Data.Context;
using SchoolSystem.School.DAL.Data.Identity;
using SchoolSystem.School.DAL.GenaricRepo;
using SchoolSystem.Services;
using System.Text;



var builder = WebApplication.CreateBuilder(args);

// Configure JWT settings
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Identity services
builder.Services.AddIdentity<AppUser, IdentityRole>()
           .AddEntityFrameworkStores<SchoolContext>()
           .AddDefaultTokenProviders();

// Scoped services
builder.Services.AddScoped<IAuthoServices, AuthoServices>();

// Add DbContext
builder.Services.AddDbContext<SchoolContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"))
);


// Add Authentication with JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(o =>
{
    o.RequireHttpsMetadata = false;
    o.SaveToken = false;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

////Region Repos
builder.Services.AddScoped(typeof(IGenaricRepo<>), typeof(GenaricRepo<>));
builder.Services.AddAutoMapper(typeof(MappingProfiles));

// Add controllers
builder.Services.AddControllers();

// Add Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestApiJWT", Version = "v1" });
});


builder.Services.AddControllers();
builder.Services.AddDistributedMemoryCache();


// 

var app = builder.Build();


//Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");

    app.UseHsts();
}

app.UseSwagger();

app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestApiJWT v1"));

app.UseCors("AllowAll");
//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
//app.UseSession();
app.MapControllers();


app.Run();