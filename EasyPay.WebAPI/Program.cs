using EasyPay.Data.GeneratedModels; // <-- Ye EasyPayDbContext ke liye hai
using EasyPay.Data.GeneratedModels.Logs;
using EasyPay.Logic;                // <-- Ye TransactionManager ke liye hai
using EasyPay.WebAPI.Middlewares;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Transactions;

var builder = WebApplication.CreateBuilder(args);

// 1. Controllers aur Validation add karna
builder.Services.AddControllers();
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// 2. Database Connection (Ab hum Scaffolded Context use kar rahe hain)
builder.Services.AddDbContext<EasyPayDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<EasyPayLogsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LogsConnection")));

// 3. Logic Layer (Dependency Injection)
builder.Services.AddScoped<ITransactionManager, EasyPay.Logic.TransactionManager>();

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

// 1. Pehle CORS (Agar hai)
app.UseCors("AllowAll");

// 2. Phir Routing
app.UseRouting();

// 3. Authentication (ID Card Check) <-- YE NAYA HAI
app.UseAuthentication();

// 4. Authorization (Gate Pass Check) <-- YE NAYA HAI
app.UseAuthorization();

// 5. Logging (Jasoosi)
app.UseMiddleware<LoggingMiddleware>();

app.MapControllers();
app.Run();