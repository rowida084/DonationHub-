using Application;
using Application.Options;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.Json;
var builder = WebApplication.CreateBuilder(args);
var configRoot = (IConfigurationRoot)builder.Configuration;

foreach (var provider in configRoot.Providers)
{
    if (provider.TryGet("SeedAdmin:Password", out var value))
    {
        Console.WriteLine(
            $"{provider}: Length = {value?.Length}, Special = {value?.Any(c => !char.IsLetterOrDigit(c))}"
        );
    }
}
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
//builder.Services.AddScoped<IdentityUser>();

builder.Services
    .AddOptions<JwtOptions>()
    .BindConfiguration(JwtOptions.SectionName)
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services
    .AddOptions<DonationOptions>()
    .BindConfiguration(DonationOptions.SectionName)
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services
    .AddOptions<SeedAdminOptions>()
    .BindConfiguration(SeedAdminOptions.SectionName)
    .ValidateDataAnnotations()
    .ValidateOnStart();

var password = builder.Configuration["SeedAdmin:Password"];

Console.WriteLine($"Config password length: {password?.Length}");
Console.WriteLine($"Config has special character: {password?.Any(c => !char.IsLetterOrDigit(c))}");

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<IdentitySeeder>();

    await seeder.SeedRolesAsync();
    await seeder.SeedAdminAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();


app.Run();
