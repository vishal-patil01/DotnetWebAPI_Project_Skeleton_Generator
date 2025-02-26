namespace ProjectCreator.Helpers.ClassesGenerators.Services.Interface
{
    internal class ISampleServiceCreator
    {
        public static string GetIServiceConfiguration(string projectName)
        {
            return @$"using {projectName}.Models.Contracts;

namespace {projectName}.Services.Interface
{{
    public interface ISampleService
    {{
        BaseResponse GetSampleData();
    }}
}}
";
        }
    }
}
