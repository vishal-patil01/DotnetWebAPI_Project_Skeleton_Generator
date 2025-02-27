using ProjectCreator.Enums;

namespace ProjectCreator.Helpers.ClassesGenerators.Repository.Implementation
{
    internal class SampleRepositoryCreator
    {
        public static string GetRepositoryConfiguration(string projectName, DatabaseType databaseType)
        {
            string methodCode = string.Empty;
            string dbImports = string.Empty;
            if (databaseType == DatabaseType.MSSqlServer)
            {
                dbImports = @"using Dapper;";
                methodCode = @"var connection = _context.CreateConnection();
            //write your query
            //connection.ExecuteAsync("""");
            return new List<string> { ""Test1"", ""Test2"" };";
            }
            else if (databaseType == DatabaseType.MongoDB)
            {
                dbImports = @"";
                methodCode = @"var database = _context.GetDatabase();
            //write your mongo query
            //var collection = database.GetCollection<ModelClass>($""CollectionName"");
            return new List<string> { ""Test1"", ""Test2"" };";
            }
            else if (databaseType == DatabaseType.MySql)
            {
                dbImports = @"using Dapper;";
                methodCode = @"var connection = _context.CreateConnection();
            //write your query
            //connection.ExecuteAsync("""");
            return new List<string> { ""Test1"", ""Test2"" };";
            }
            else
            {
                dbImports = @"using Dapper;";
                methodCode = @"var connection = _context.CreateConnection();
            //write your query
            //connection.ExecuteAsync("""");
            return new List<string> { ""Test1"", ""Test2"" };";
            }

            return @$"using {projectName}.Repository.Helpers;
using {projectName}.Repository.Interface;
{dbImports}

namespace {projectName}.Repository.Implementation
{{
    public class SampleRepository : ISampleRepository
    {{
        private readonly IDBHelper _context;

        public SampleRepository(IDBHelper context)
        {{
            _context = context;
        }}

        public List<string> GetSampleData()
        {{
          {methodCode}
        }}
    }}
}}
";
        }
    }
}
