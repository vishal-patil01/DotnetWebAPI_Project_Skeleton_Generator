using ProjectCreator.Enums;

namespace ProjectCreator.Helpers.ClassesGenerators.Repository.Helpers
{
    internal class IDBHelperCreator
    {
        public static string GetIDBHelperConfiguration(string projectName, DatabaseType databaseType)
        {
            string method = string.Empty;
            string dbImports = string.Empty;
            if (databaseType == DatabaseType.MSSqlServer)
            {
                method = @"IDbConnection CreateConnection();";
            }
            else if (databaseType == DatabaseType.MongoDB)
            {
                dbImports = @"using MongoDB.Driver;";
                method = @"IMongoDatabase GetDatabase();";
            }
            else if (databaseType == DatabaseType.MySql)
            {
                method = @"IDbConnection CreateConnection();";
            }
            else
            {
                method = @"IDbConnection CreateConnection();";
            }

            return @$"using System.Data;
{dbImports}

namespace {projectName}.Repository.Helpers
{{
    public interface IDBHelper
    {{
        {method}
    }}
}}

";
        }
    }
}
