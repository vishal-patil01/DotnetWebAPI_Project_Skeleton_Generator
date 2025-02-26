namespace ProjectCreator.Helpers.ClassesGenerators.Repository.Helpers
{
    internal class DapperContextCreator
    {
        public static string GetDapperContextConfiguration(string projectName)
        {
            return @$"using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace {projectName}.Repository.Helpers
{{
    public class DapperContext : IDapperContext
    {{
        private readonly IConfiguration _iConfiguration;
        private readonly string _connString;
        private readonly string _customConnString;
        public DapperContext(IConfiguration iConfiguration)
        {{
            _iConfiguration = iConfiguration;
            _connString = _iConfiguration.GetConnectionString(""DbConnection"");
        }}
        public IDbConnection CreateConnection() => new SqlConnection(_connString);
    }}
}}

";
        }
    }
}
