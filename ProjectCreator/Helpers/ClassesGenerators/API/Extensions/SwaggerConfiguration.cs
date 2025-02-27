namespace ProjectCreator.Helpers.ClassesGenerators.API.Extensions
{
    public class SwaggerConfiguration
    {
        public static string GetSwaggerConfiguration(string projectName)
        {
            return @$"
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;


namespace {projectName}.Extensions
{{
    public static class SwaggerConfiguration
    {{
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection serviceCollection, IConfiguration configuration)
        {{
            serviceCollection.AddEndpointsApiExplorer();
            serviceCollection.AddSwaggerGen(opt =>
            {{
                opt.SwaggerDoc(""v1"", new OpenApiInfo {{ Title = ""Bornan API"", Version = ""v1"" }});
                opt.AddSecurityDefinition(""Bearer"", new OpenApiSecurityScheme
                {{
                    In = ParameterLocation.Header,
                    Description = ""Please enter token"",
                    Name = ""Authorization"",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = ""JWT"",
                    Scheme = ""bearer""
                }});

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {{
                    {{
                        new OpenApiSecurityScheme
                        {{
                            Reference = new OpenApiReference
                            {{
                                Type=ReferenceType.SecurityScheme,
                                Id=""Bearer""
                            }}
                        }},
                        new string[]{{}}
                    }}
                }});
            }});

            return serviceCollection;
        }}
    }}
}}";
        }
    }
}
