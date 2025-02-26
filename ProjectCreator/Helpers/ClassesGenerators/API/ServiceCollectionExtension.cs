namespace ProjectCreator.Helpers.ClassesGenerators.API
{
    public static class ServiceCollectionExtension
    {
        public static string GetServiceCollectionConfiguration(string projectName, bool isSinglelayer = true)
        {
            var import = isSinglelayer ? projectName : projectName.Replace(".API", "");
            return $@"using {import}.Repository.Implementation;
using {import}.Repository.Interface;
using {import}.Repository.Helpers;
using {import}.Services.Interface;
using {import}.Services.Implementation;
using {projectName}.Middlewares;
using Newtonsoft.Json;


namespace {projectName}.Extensions
{{
    public static class ServiceExtension
    {{
        public static IServiceCollection AddApplicationServiceExtension(this IServiceCollection serviceCollection, IConfiguration configuration)
        {{
            serviceCollection.AddControllers()
              .AddJsonOptions(jsonOptions =>
              {{
                  jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
              }})
              .AddNewtonsoftJson(options =>
              {{
                  options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                  options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
              }});

            // add service extensions
            serviceCollection.AddHttpContextAccessor();
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddSingleton<IDapperContext,DapperContext>();
            serviceCollection.AddSingleton<ISampleRepository,SampleRepository>();
            serviceCollection.AddSingleton<ISampleService,SampleService>();
            serviceCollection.AddSwaggerGen();
            return serviceCollection;
        }}

        public static IApplicationBuilder UseApplicationServiceExtension(this WebApplication app)
        {{
            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment(""dev""))
            {{
                app.UseSwagger();
                app.UseSwaggerUI();
            }}
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseHttpsRedirection();

            app.MapControllers();

            app.UseCors(x =>
            {{
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
            }});
            return app;
        }}
    }}
}}
";
        }
    }
}
