namespace ProjectCreator.Helpers.JsonGenerators
{
    public class AppSettingsCreator
    {
        public static string GetJsonConfiguration(string projectName)
        {
            return @$"{{
  ""Logging"": {{
    ""LogLevel"": {{
      ""Default"": ""Information"",
      ""Microsoft.AspNetCore"": ""Warning""
    }}
  }},
  ""ConnectionStrings"": {{
    ""CustomConnection"": """",
    ""DbConnection"": """"
  }}
}}";
        }
    }
}
