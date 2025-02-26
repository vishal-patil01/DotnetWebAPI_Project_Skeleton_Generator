using ProjectCreator.Enums;

namespace ProjectCreator.Helpers.ClassesGenerators.Repository.Helpers
{
    internal class DapperContextCreator
    {
        public static string GetDapperContextConfiguration(string projectName, DatabaseType databaseType)
        {
            string method = string.Empty;
            string dbImports = string.Empty;
            if (databaseType == DatabaseType.MSSqlServer)
            {
                dbImports = @"using Microsoft.Data.SqlClient;";
                method = @"public IDbConnection CreateConnection() => new SqlConnection(_connString);";
            }
            else if (databaseType == DatabaseType.MongoDB)
            {
                dbImports = @"using MongoDB.Bson;
using MongoDB.Driver;";
                method = @" public IMongoDatabase GetDatabase()
        {
            var mongoUrl = new MongoUrl(_connString);
            return new MongoClient(_connString).GetDatabase(mongoUrl.DatabaseName);
        }
";
            }
            else if (databaseType == DatabaseType.MySql)
            {
                dbImports = @"using MySql.Data.MySqlClient;";
                method = @"public IDbConnection CreateConnection() => new MySqlConnection(_connString);";
            }
            else
            {
                dbImports = @"using Oracle.ManagedDataAccess.Client;";
                method = @"public IDbConnection CreateConnection() => new OracleConnection(_connString);";
            }

            return @$"using System.Data;
{dbImports}
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
        {method}
    }}
}}

";
        }
    }
}
