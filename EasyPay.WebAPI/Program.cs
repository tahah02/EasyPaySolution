using EasyPay.Data.GeneratedModels; // <-- Ye EasyPayDbContext ke liye hai
using EasyPay.Logic;                // <-- Ye TransactionManager ke liye hai
using EasyPay.WebAPI.Middlewares;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

var builder = WebApplication.CreateBuilder(args);

// 1. Controllers aur Validation add karna
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// 2. Database Connection (Ab hum Scaffolded Context use kar rahe hain)
builder.Services.AddDbContext<EasyPayDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

// 5. Jasoos Camera ON karna (Logging Middleware)
app.UseMiddleware<LoggingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();