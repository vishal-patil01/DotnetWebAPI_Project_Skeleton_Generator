using ProjectCreator.Enums;
using ProjectCreator.Helpers;
using ProjectCreator.Helpers.ClassesGenerators.API;
using ProjectCreator.Helpers.ClassesGenerators.Models;
using ProjectCreator.Helpers.ClassesGenerators.Repository.Helpers;
using ProjectCreator.Helpers.ClassesGenerators.Repository.Implementation;
using ProjectCreator.Helpers.ClassesGenerators.Repository.Interface;
using ProjectCreator.Helpers.ClassesGenerators.Services.Helper;
using ProjectCreator.Helpers.ClassesGenerators.Services.Implementation;
using ProjectCreator.Helpers.ClassesGenerators.Services.Interface;
using ProjectCreator.Helpers.JsonGenerators;

namespace ProjectCreator.SingleLayerProject
{
    public class SingleLayerProjectCreator
    {
        public static void CreateSingleLayerProjectCreator(string projectName, char dotnetVersion, DatabaseType databaseType)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Creating Single Layer Web API Project...");
            Console.ForegroundColor = ConsoleColor.White;
            CommonHelper.ExecuteStep("Creating the solution", () => CommonHelper.RunCommand($"dotnet new sln -n {projectName}"));
            CommonHelper.ExecuteStep("Creating the projects", () => CreateProject(projectName));
            CommonHelper.ExecuteStep("Creating folder structure", () => CreteNecessaryFolders(projectName));
            CommonHelper.ExecuteStep("Adding NuGet packages", () => AddNugetPackages(projectName, dotnetVersion, databaseType));
            CommonHelper.ExecuteStep("Creating sample controller", () =>
            {
                //API config
                CommonHelper.CreateFile($"{projectName}.API/Controllers/SampleController.cs", SampleControllerCreator.GetSampleControllerConfiguration($"{projectName}.API"));
                CommonHelper.CreateFile($"{projectName}.API/Extensions/SwaggerConfiguration.cs", SwaggerConfiguration.GetSwaggerConfiguration($"{projectName}.API"));
                CommonHelper.CreateFile($"{projectName}.API/Extensions/ServiceCollectionExtension.cs", ServiceCollectionExtension.GetServiceCollectionConfiguration($"{projectName}.API"));
                CommonHelper.CreateFile($"{projectName}.API/appsettings.json", AppSettingsCreator.GetJsonConfiguration(projectName, databaseType));
                CommonHelper.CreateFile($"{projectName}.API/appsettings.Development.json", AppSettingsCreator.GetJsonConfiguration(projectName, databaseType));
                CommonHelper.CreateFile($"{projectName}.API/appsettings.Production.json", AppSettingsCreator.GetJsonConfiguration(projectName, databaseType));
                CommonHelper.CreateFile($"{projectName}.API/Middlewares/ExceptionHandlerMiddleware.cs", ExceptionMiddlewareCreator.GetMiddlewareConfiguration($"{projectName}.API"));

                projectName = $"{projectName}.API";

                //Model Config
                CommonHelper.CreateFile($"{projectName}/Models/Contracts/BaseResponse.cs", BaseResponseCreator.GetBaseResponseConfiguration(projectName));

                //Service Config
                CommonHelper.CreateFile($"{projectName}/Services/Helpers/ResponseHelper.cs", ResponseHelperCreator.GetResponseHelperConfiguration(projectName));
                CommonHelper.CreateFile($"{projectName}/Services/Interface/ISampleService.cs", ISampleServiceCreator.GetIServiceConfiguration(projectName));
                CommonHelper.CreateFile($"{projectName}/Services/Implementation/SampleService.cs", SampleServiceCreator.GetServiceConfiguration(projectName));

                //Repository config
                CommonHelper.CreateFile($"{projectName}/Repository/Interface/ISampleRepository.cs", ISampleRepositoryCreator.GetIRepositoryConfiguration($"{projectName}"));
                CommonHelper.CreateFile($"{projectName}/Repository/Implementation/SampleRepository.cs", SampleRepositoryCreator.GetRepositoryConfiguration($"{projectName}", databaseType));

                CommonHelper.CreateFile($"{projectName}/Repository/Helpers/DapperContext.cs", DapperContextCreator.GetDapperContextConfiguration($"{projectName}", databaseType));
                CommonHelper.CreateFile($"{projectName}/Repository/Helpers/IDapperContext.cs", IDapperContextCreator.GetIDapperContextConfiguration($"{projectName}", databaseType));

            });
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Project setup complete.");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        private static void CreteNecessaryFolders(string projectName)
        {

            // Create folders in API project
            CommonHelper.CreateDirectory($"{projectName}.API/Middlewares");
            CommonHelper.CreateDirectory($"{projectName}.API/Extensions");
            CommonHelper.CreateDirectory($"{projectName}.API/Controllers"); // Explicitly create the Controllers directory
                                                                            // Modify Program.cs to use AddServices
            ModifyProgramCs($"{projectName}.API", $"{projectName}.API/Program.cs");

            // Create folders in Models project
            CommonHelper.CreateDirectory($"{projectName}.API/Models/Entities");
            CommonHelper.CreateDirectory($"{projectName}.API/Models/Contracts");
            CommonHelper.CreateDirectory($"{projectName}.API/Models/DBModels");
            CommonHelper.CreateDirectory($"{projectName}.API/Models/Enums");

            // Create folders in Services project
            CommonHelper.CreateDirectory($"{projectName}.API/Services/Interface");
            CommonHelper.CreateDirectory($"{projectName}.API/Services/Implementation");
            CommonHelper.CreateDirectory($"{projectName}.API/Services/Helpers");

            // Create folders in Services project
            CommonHelper.CreateDirectory($"{projectName}.API/Repository/Interface");
            CommonHelper.CreateDirectory($"{projectName}.API/Repository/Implementation");
            CommonHelper.CreateDirectory($"{projectName}.API/Repository/Helpers");

            // Update .csproj files to include the folders
            CommonHelper.UpdateCsprojFile($"{projectName}.API/{projectName}.API.csproj", new[]
            {
                "Controllers", "Middlewares", "Extensions",
                "Models/Entities", "Models/Contracts", "Models/DBModels", "Models/Enums" ,
                "Services/Interface", "Services/Implementation", "Services/Helpers",
                "Repository/Interface", "Repository/Implementation", "Repository/Helpers"
            });
        }

        private static void AddNugetPackages(string projectName, char DotNetVersionFirstChar, DatabaseType databaseType)
        {
            // Add NuGet packages to API project
            CommonHelper.RunCommand($"dotnet add {projectName}.API/{projectName}.API.csproj package Microsoft.Extensions.Configuration");
            CommonHelper.RunCommand($"dotnet add {projectName}.API/{projectName}.API.csproj package Microsoft.Extensions.DependencyInjection");
            CommonHelper.RunCommand($"dotnet add {projectName}.API/{projectName}.API.csproj package Swashbuckle.AspNetCore");
            CommonHelper.RunCommand($"dotnet add {projectName}.API/{projectName}.API.csproj package Microsoft.AspNetCore.Mvc");
            CommonHelper.RunCommand($"dotnet add {projectName}.API/{projectName}.API.csproj package Microsoft.AspNetCore.Mvc.NewtonsoftJson --version {DotNetVersionFirstChar}.*");
            CommonHelper.RunCommand($"dotnet add {projectName}.API/{projectName}.API.csproj package Serilog.AspNetCore");
            CommonHelper.RunCommand($"dotnet add {projectName}.API/{projectName}.API.csproj package Serilog.Extensions.Hosting");
            CommonHelper.RunCommand($"dotnet add {projectName}.API/{projectName}.API.csproj package Serilog.Sinks.Console");
            CommonHelper.RunCommand($"dotnet add {projectName}.API/{projectName}.API.csproj package Serilog.Sinks.File");

            // Add NuGet packages to Service project
            CommonHelper.RunCommand($"dotnet add {projectName}.API/{projectName}.API.csproj package Microsoft.AspNetCore.Mvc");

            // Add NuGet packages to Repository project
            CommonHelper.RunCommand($"dotnet add {projectName}.API/{projectName}.API.csproj package Microsoft.Data.SqlClient");
            CommonHelper.RunCommand($"dotnet add {projectName}.API/{projectName}.API.csproj package Microsoft.Extensions.Configuration");
            CommonHelper.RunCommand($"dotnet add {projectName}.API/{projectName}.API.csproj package Dapper");

            if (databaseType == DatabaseType.MSSqlServer)
            {
                CommonHelper.RunCommand($"dotnet add {projectName}.API/{projectName}.API.csproj package Microsoft.Data.SqlClient");
                CommonHelper.RunCommand($"dotnet add {projectName}.API/{projectName}.API.csproj package Dapper");
            }
            else if (databaseType == DatabaseType.MongoDB)
            {
                CommonHelper.RunCommand($"dotnet add {projectName}.API/{projectName}.API.csproj package MongoDB.Driver");
            }
            else if (databaseType == DatabaseType.MySql)
            {
                CommonHelper.RunCommand($"dotnet add {projectName}.API/{projectName}.API.csproj package Dapper");
                CommonHelper.RunCommand($"dotnet add {projectName}.API/{projectName}.API.csproj package MySql.Data");
            }
            else
            {
                CommonHelper.RunCommand($"dotnet add {projectName}.API/{projectName}.API.csproj package Dapper");
                CommonHelper.RunCommand($"dotnet add {projectName}.API/{projectName}.API.csproj package Oracle.ManagedDataAccess.Core");
            }
        }
        private static void CreateProject(string projectName)
        {
            CommonHelper.RunCommand($"dotnet new webapi -n {projectName}.API");

            // Add the projects to the solution
            CommonHelper.RunCommand($"dotnet sln {projectName}.sln add {projectName}.API/{projectName}.API.csproj");
        }

        static void ModifyProgramCs(string projectName, string path)
        {
            var programCsContent = @$"
using {projectName}.Extensions;
using Newtonsoft.Json;
using Serilog;
using Serilog.Events;

#region Configuring builder
var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseKestrel(options =>
{{
    options.AddServerHeader = false;
}});
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
             .AddJsonFile(""appsettings.json"", optional: true, reloadOnChange: true)
             .AddJsonFile($""appsettings.{{builder.Environment.EnvironmentName}}.json"", optional: false, reloadOnChange: true)
             .AddEnvironmentVariables();
#endregion

#region Add Services to the Container.
builder.Services.SetupDependency(builder.Configuration);

//Logging Configuration
builder.Logging.ClearProviders();
builder.Host.UseSerilog((ctx, lc) => lc
                 .WriteTo.Logger(lc => lc
                     .Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Error)
                     .WriteTo.File(""logs/Error_.txt"", LogEventLevel.Error, rollingInterval: RollingInterval.Day))
                 .WriteTo.Logger(lc => lc
                         .Filter.ByIncludingOnly(evt => evt.Level <= LogEventLevel.Fatal)
                         .WriteTo.File(""logs/logs_.txt"", rollingInterval: RollingInterval.Day)));
#endregion

#region Configure the HTTP request pipeline.
var app = builder.Build();
app.ConfigureMiddlewares();
app.Run();
#endregion
";
            File.WriteAllText(path, programCsContent);

        }
    }
}
