using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCreator.Helpers.ClassesGenerators
{
    internal class IDapperContextCreator
    {
        public static string GetIDapperContextConfiguration(string projectName)
        {
            return @$"using System.Data;

namespace {projectName}.Repository.Interface
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
