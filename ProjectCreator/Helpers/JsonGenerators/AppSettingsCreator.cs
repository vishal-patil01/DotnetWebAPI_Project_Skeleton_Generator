using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
