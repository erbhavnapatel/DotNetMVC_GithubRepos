using DotNetMVC_GithubRepos.Application.Interfaces;
using DotNetMVC_GithubRepos.Application.Services;
using DotNetMVC_GithubRepos.Infrastructure.Data;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using RestSharp;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<IGithubService, GitHubService>();
builder.Services.AddScoped<IRepositoryService, RepositoryService>();

builder.Services.AddSingleton<RestClient>(provider =>
{
    var client = new RestClient("https://api.github.com/");
    client.AddDefaultHeader("User-Agent", "GitHubRepoApp");
    return client;
});

builder.Services.AddHangfire(configuration =>
    configuration.UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection")));
builder.Services.AddHangfireServer();


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Configure Hangfire dashboard
app.UseHangfireDashboard("/hangfire");

// Configure Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

RecurringJob.AddOrUpdate<IRepositoryService>(
    "fetch-repositories",
    service => service.FetchAndSaveDataAsync(),
    Cron.Minutely);

app.Run();