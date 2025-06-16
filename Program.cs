using System;
using System.IO;
using System.Linq; // Added for AsEnumerable().Select() in factory
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using StudentEventManagementSystem.Data;
using StudentEventManagementSystem.Services;
using StudentEventManagementSystem.Services.Interfaces;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        string projectRootPath = Directory.GetCurrentDirectory();
        string appSettingsFileName = "appsettings.json";
        string appSettingsPath = Path.Combine(projectRootPath, appSettingsFileName);

        int maxDepth = 3;
        int currentDepth = 0;
        string initialBasePath = Path.GetDirectoryName(appSettingsPath) ?? projectRootPath; // Ensure initialBasePath is not null

        while (!File.Exists(appSettingsPath) && currentDepth < maxDepth)
        {
            DirectoryInfo parentDir = Directory.GetParent(initialBasePath);
            if (parentDir == null) break;
            appSettingsPath = Path.Combine(parentDir.FullName, appSettingsFileName);
            initialBasePath = parentDir.FullName; // Update base path for next iteration
            currentDepth++;
        }

        if (!File.Exists(appSettingsPath) && projectRootPath.Contains(Path.Combine("bin", "Debug")))
        {
            string testPath = Path.GetFullPath(Path.Combine(projectRootPath, "..", "..", "..", "StudentEventManagementSystem", appSettingsFileName));
            if (File.Exists(testPath)) appSettingsPath = testPath;
        }

        if (!File.Exists(appSettingsPath) && projectRootPath.Contains(Path.Combine("bin", "Release")))
        {
            string testPath = Path.GetFullPath(Path.Combine(projectRootPath, "..", "..", "..", "StudentEventManagementSystem", appSettingsFileName));
            if (File.Exists(testPath)) appSettingsPath = testPath;
        }


        if (!File.Exists(appSettingsPath))
        {
            throw new FileNotFoundException($"Could not locate '{appSettingsFileName}'. Last path tried: '{appSettingsPath}'. Current Directory for tools: '{projectRootPath}'. Ensure the file exists and 'Copy to Output Directory' is set for appsettings.json.");
        }

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Path.GetDirectoryName(appSettingsPath))
            .AddJsonFile(Path.GetFileName(appSettingsPath), optional: false, reloadOnChange: true)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            var availableKeys = string.Join(", ", configuration.AsEnumerable().Select(kvp => kvp.Key));
            throw new InvalidOperationException($"Could not find a connection string named 'DefaultConnection' in '{appSettingsPath}'. Available keys: {availableKeys}");
        }

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer(connectionString);
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}