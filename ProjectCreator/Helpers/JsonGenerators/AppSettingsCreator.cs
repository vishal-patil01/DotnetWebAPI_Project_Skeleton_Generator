using ProjectCreator.Enums;

namespace ProjectCreator.Helpers.JsonGenerators
{
    public class AppSettingsCreator
    {
        public static string GetJsonConfiguration(string projectName, DatabaseType databaseType)
        {
            string sampleConnection = string.Empty;
            if (databaseType != DatabaseType.MongoDB)
            {
                sampleConnection = @"""DbConnection"": """"";
            }
            else
            {
                sampleConnection = @"//please add mongo connection string in valid format eg. mongodb://localhost:27017/Test?retryWrites=true&w=majority&appName=abc
    //Here Test is DatabaseName
    ""DbConnection"": ""mongodb://localhost:27017/Test""";
            }
            return @$"{{
  ""Logging"": {{
    ""LogLevel"": {{
      ""Default"": ""Information"",
      ""Microsoft.AspNetCore"": ""Warning""
    }}
  }},
  ""ConnectionStrings"": {{
    ""CustomConnection"": """",
    {sampleConnection}
  }}
}}";
        }
    }
}
