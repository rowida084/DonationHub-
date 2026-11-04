using Application;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider
        .GetRequiredService<AppDbContext>();

    var now = DateTime.UtcNow;
    var uniqueId = Guid.NewGuid().ToString("N");

    var category = Category.Create(
        "تصنيف " + uniqueId,
        "Category " + uniqueId,
        "وصف تجريبي",
        "Test Description",
        now);

    var user = ApplicationUser.Create(
        $"test{uniqueId}@gmail.com",
        $"test{uniqueId}",
        "Test User",
        now);

    var campaign = Campaign.Create(
        "حملة تجريبية",
        "Test Campaign",
        "وصف الحملة",
        "Campaign Description",
        10000,
        now.AddDays(30),
        category.Id,
        user.Id,
        now);

    context.AddRange(category, user, campaign);

    await context.SaveChangesAsync();

    var campaignId = campaign.Id;

    // Soft Delete
    context.Campaigns.Remove(campaign);
    await context.SaveChangesAsync();

    // Normal Query
    var normalQuery = await context.Campaigns
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == campaignId);

    // Ignore Global Filters
    var deletedCampaign = await context.Campaigns
        .IgnoreQueryFilters()
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == campaignId);

    Console.WriteLine("========== Soft Delete Test ==========");
    Console.WriteLine($"Normal Query: {normalQuery == null}");
    Console.WriteLine($"Ignore Filters: {deletedCampaign != null}");
    Console.WriteLine($"IsDeleted: {deletedCampaign?.IsDeleted}");
    Console.WriteLine($"DeletedAtUtc: {deletedCampaign?.DeletedAtUtc}");
}

app.Run();
