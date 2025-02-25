using ProjectCreator.Helpers;
using ProjectCreator.Helpers.ClassesGenerators;
using ProjectCreator.Helpers.JsonGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCreator.MultiLayerProject
{
    public class MultiLayerProjectCreator
    {
        public static void CreateMultiLayerProject(string projectName, char dotnetVersion)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Creating Multilayer Web API Project...");
            Console.ForegroundColor = ConsoleColor.White;
            CommonHelper.ExecuteStep("Creating the solution", () => CommonHelper.RunCommand($"dotnet new sln -n {projectName}"));
            CommonHelper.ExecuteStep("Creating the projects", () => CreateProject(projectName));
            CommonHelper.ExecuteStep("Adding project references", () => AddProjectReference(projectName));
            CommonHelper.ExecuteStep("Creating folder structure", () => CreteNecessaryFolders(projectName));
            CommonHelper.ExecuteStep("Adding NuGet packages", () => AddNugetPackages(projectName, dotnetVersion));
            CommonHelper.ExecuteStep("Creating sample controller", () =>
            {
                //API config
                CommonHelper.CreateFile($"{projectName}.API/Controllers/SampleController.cs", GetSampleControllerCode($"{projectName}.API"));
                CommonHelper.CreateFile($"{projectName}.API/Extensions/SwaggerConfiguration.cs", SwaggerConfiguration.GetSwaggerConfiguration($"{projectName}.API"));
                CommonHelper.CreateFile($"{projectName}.API/Extensions/ServiceCollectionExtension.cs", ServiceCollectionExtension.GetServiceCollectionConfiguration($"{projectName}.API"));
                CommonHelper.CreateFile($"{projectName}.API/appsettings.json", AppSettingsCreator.GetJsonConfiguration(projectName));
                CommonHelper.CreateFile($"{projectName}.API/appsettings.Development.json", AppSettingsCreator.GetJsonConfiguration(projectName));
                CommonHelper.CreateFile($"{projectName}.API/appsettings.Production.json", AppSettingsCreator.GetJsonConfiguration(projectName));
                CommonHelper.CreateFile($"{projectName}.API/Middlewares/ExceptionHandlerMiddleware.cs", ExceptionMiddlewareCreator.GetMiddlewareConfiguration($"{projectName}.API"));

                //Model Config
                CommonHelper.CreateFile($"{projectName}.Models/Contracts/BaseResponse.cs", BaseResponseCreator.GetBaseResponseConfiguration(projectName));

                //Service Config
                CommonHelper.CreateFile($"{projectName}.Services/Helpers/ResponseHelper.cs", ResponseHelperCreator.GetResponseHelperConfiguration(projectName));

                //Repository config 
                CommonHelper.CreateFile($"{projectName}.Repository/Implementation/DapperContext.cs", DapperContextCreator.GetDapperContextConfiguration(projectName));
                CommonHelper.CreateFile($"{projectName}.Repository/Interface/IDapperContext.cs", IDapperContextCreator.GetIDapperContextConfiguration(projectName));

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
            ModifyProgramCs($"{projectName}.API",$"{projectName}.API/Program.cs");

            // Create folders in Models project
            CommonHelper.CreateDirectory($"{projectName}.Models/Entities");
            CommonHelper.CreateDirectory($"{projectName}.Models/Contracts");
            CommonHelper.CreateDirectory($"{projectName}.Models/DBModels");
            CommonHelper.CreateDirectory($"{projectName}.Models/Enums");

            // Create folders in Services project
            CommonHelper.CreateDirectory($"{projectName}.Services/Interface");
            CommonHelper.CreateDirectory($"{projectName}.Services/Implementation");
            CommonHelper.CreateDirectory($"{projectName}.Services/Helpers");

            // Create folders in Services project
            CommonHelper.CreateDirectory($"{projectName}.Repository/Interface");
            CommonHelper.CreateDirectory($"{projectName}.Repository/Implementation");
            CommonHelper.CreateDirectory($"{projectName}.Repository/Helpers");


            //Delete unwanted files
            CommonHelper.DeleteFile($"{projectName}.Models/Class1.cs");
            CommonHelper.DeleteFile($"{projectName}.Services/Class1.cs");
            CommonHelper.DeleteFile($"{projectName}.Repository/Class1.cs");

            // Update .csproj files to include the folders
            CommonHelper.UpdateCsprojFile($"{projectName}.API/{projectName}.API.csproj", new[] { "Controllers", "Middlewares", "Extensions" });
            CommonHelper.UpdateCsprojFile($"{projectName}.Models/{projectName}.Models.csproj", new[] { "Entities", "Contracts", "DBModels", "Enums" });
            CommonHelper.UpdateCsprojFile($"{projectName}.Services/{projectName}.Services.csproj", new[] { "Interface", "Implementation", "Helpers" });
            CommonHelper.UpdateCsprojFile($"{projectName}.Repository/{projectName}.Repository.csproj", new[] { "Interface", "Implementation", "Helpers" });
        }

        private static void AddProjectReference(string projectName)
        {
            // Add project references
            CommonHelper.RunCommand($"dotnet add {projectName}.Repository/{projectName}.Repository.csproj reference {projectName}.Models/{projectName}.Models.csproj");
            CommonHelper.RunCommand($"dotnet add {projectName}.Services/{projectName}.Services.csproj reference {projectName}.Repository/{projectName}.Repository.csproj");
            CommonHelper.RunCommand($"dotnet add {projectName}.API/{projectName}.API.csproj reference {projectName}.Services/{projectName}.Services.csproj");
        }

        private static void AddNugetPackages(string projectName, char DotNetVersionFirstChar)
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
            CommonHelper.RunCommand($"dotnet add {projectName}.Services/{projectName}.Services.csproj package Microsoft.AspNetCore.Mvc");

            // Add NuGet packages to Repository project
            CommonHelper.RunCommand($"dotnet add {projectName}.Repository/{projectName}.Repository.csproj package Microsoft.Data.SqlClient");
            CommonHelper.RunCommand($"dotnet add {projectName}.Repository/{projectName}.Repository.csproj package Microsoft.Extensions.Configuration");

        }

        private static void CreateProject(string projectName)
        {
            CommonHelper.RunCommand($"dotnet new webapi -n {projectName}.API");
            CommonHelper.RunCommand($"dotnet new classlib -n {projectName}.Services");
            CommonHelper.RunCommand($"dotnet new classlib -n {projectName}.Models");
            CommonHelper.RunCommand($"dotnet new classlib -n {projectName}.Repository");

            // Add the projects to the solution
            CommonHelper.RunCommand($"dotnet sln {projectName}.sln add {projectName}.API/{projectName}.API.csproj");
            CommonHelper.RunCommand($"dotnet sln {projectName}.sln add {projectName}.Services/{projectName}.Services.csproj");
            CommonHelper.RunCommand($"dotnet sln {projectName}.sln add {projectName}.Models/{projectName}.Models.csproj");
            CommonHelper.RunCommand($"dotnet sln {projectName}.sln add {projectName}.Repository/{projectName}.Repository.csproj");
        }
        static string GetSampleControllerCode(string projectName)
        {
            return $@"
using Microsoft.AspNetCore.Mvc;

namespace {projectName}.Controllers
{{
    [ApiController]
    [Route(""[controller]"")]
    public class SampleController : ControllerBase
    {{
        [HttpGet]
        public IActionResult Get()
        {{
            return Ok(""Hello, World!"");
        }}
    }}
}}";
        }

        static void ModifyProgramCs(string projectName, string path)
        {
            var programCsContent = @$"
using Newtonsoft.Json;
using Serilog;
using Serilog.Events;
using {projectName}.Extensions;
using {projectName}.Repository.Implementation;
using {projectName}.Repository.Interface;

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
builder.Services.AddApplicationServiceExtension(builder.Configuration);

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
app.UseApplicationServiceExtension();
app.Run();
#endregion
";
            File.WriteAllText(path, programCsContent);

        }
    }
}
