namespace ProjectCreator.Helpers.ClassesGenerators.Repository.Implementation
{
    internal class SampleRepositoryCreator
    {
        public static string GetRepositoryConfiguration(string projectName)
        {
            return @$"using {projectName}.Repository.Helpers;
using {projectName}.Repository.Interface;
using Dapper;

namespace {projectName}.Repository.Implementation
{{
    public class SampleRepository : ISampleRepository
    {{
        private readonly IDapperContext _context;

        public SampleRepository(IDapperContext context)
        {{
            _context = context;
        }}

        public List<string> GetSampleData()
        {{
            var connection = _context.CreateConnection();
            //write your query
            //connection.ExecuteAsync("""");
            return new List<string> {{ ""Test1"", ""Test2"" }};
        }}
    }}
}}
";
        }
    }
}
