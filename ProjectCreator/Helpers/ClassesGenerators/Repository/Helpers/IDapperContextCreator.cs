namespace ProjectCreator.Helpers.ClassesGenerators.Repository.Helpers
{
    internal class IDapperContextCreator
    {
        public static string GetIDapperContextConfiguration(string projectName)
        {
            return @$"using System.Data;

namespace {projectName}.Repository.Helpers
{{
    public interface IDapperContext
    {{
        IDbConnection CreateConnection();
    }}
}}

";
        }
    }
}
