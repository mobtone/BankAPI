using System.Globalization;
using System.Text;
using BankApp.Core.Interfaces;
using BankApp.Core.Services;
using BankApp.Data.DataModels;
using BankApp.Data.DTOs;
using BankApp.Data.Interfaces;
using BankApp.Data.Repositories.Repos;
using BankApp.Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;


var builder = WebApplication.CreateBuilder(args); //service-container

builder.Services.AddScoped<IBankAppContext, BankAppDbContext>();

builder.Services.AddScoped<IAdminRepo, AdminRepo>();
builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
builder.Services.AddScoped<IAccountRepo, AccountRepo>();
builder.Services.AddScoped<IAccountTypeRepo, AccountTypeRepo>();
builder.Services.AddScoped<ITransactionRepo, TransactionRepo>();

builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
builder.Services.AddScoped<IAccountTypeService, AccountTypeService>();


builder.Services.Configure<EncryptionSettingsDto>(builder.Configuration.GetSection("EncryptionSettings"));
builder.Services.AddTransient<IEncryptionHelper, EncryptionHelper>();
builder.Services.AddTransient<ISecurityKeyGenerator, SecurityKeyGenerator>();
builder.Services.AddSingleton<IJwtService, JwtService>();


//Här konfigureras jwt-tokens som en service
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
        };
    });

//builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
   // c.ExampleFilters();
//Lägger till säkerhetsdefinition för Bearer-token
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Ange 'Bearer' följt av ett mellanslag och din token. Exempel: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    //Lägger till säkerhetskrav för att använda Bearer-token
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    //annpassar ordningen på endpoints baserat på taggarna
    c.OrderActionsBy(apiDesc =>
    {
        var tagOrder = new List<string> { "Login", "Admin", "Customer", "Accounts", "Payments" };

        //sortera endpoints baserat på tagarnas ordning
        var tags = apiDesc.ActionDescriptor.EndpointMetadata.OfType<TagsAttribute>().SelectMany(t => t.Tags);
        return Convert.ToString(tags.Select(tag => tagOrder.IndexOf(tag)).FirstOrDefault());
    });

});

//builder.Services.AddSwaggerExamplesFromAssemblyOf<CustomerDtoExampleUI>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddHttpContextAccessor();
//var jwtSettings = builder.Configuration.GetSection("JwtSettings");


var app = builder.Build(); //pipeline

app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints => { endpoints.MapControllers();});

app.Run();
