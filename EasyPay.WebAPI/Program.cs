using EasyPay.Data.GeneratedModels; // <-- Ye EasyPayDbContext ke liye hai
using EasyPay.Data.GeneratedModels.Core;
using EasyPay.Data.GeneratedModels.Logs;
using EasyPay.Logic;                // <-- Ye TransactionManager ke liye hai
using EasyPay.WebAPI.Middlewares;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer; // Zaroori packages
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Transactions;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// 1. Controllers aur Validation add karna
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// 2. Database Connection (Ab hum Scaffolded Context use kar rahe hain)
builder.Services.AddDbContext<EasyPayCoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CoreConnection")));

// 2. LOGS DB (ApiLogs)
builder.Services.AddDbContext<EasyPayLogsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LogsConnection")));

// 3. Logic Layer (Dependency Injection)
builder.Services.AddScoped<ITransactionManager, EasyPay.Logic.TransactionManager>();

// JWT Authentication Scheme Register
var key = Encoding.ASCII.GetBytes(builder.Configuration["JwtSettings:Key"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // Development ke liye false
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// 4. Swagger Setup (Testing ke liye)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// CORS aur Routing
app.UseCors("AllowAll");
app.UseRouting();

// 1. Pehle Authentication (Token ki pehchan)
app.UseAuthentication();

// 2. Phir Authorization (Access ki ijazat)
app.UseAuthorization();

app.UseMiddleware<LoggingMiddleware>();
app.MapControllers();

app.Run();