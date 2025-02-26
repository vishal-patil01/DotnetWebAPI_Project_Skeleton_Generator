namespace ProjectCreator.Helpers.ClassesGenerators.Services.Implementation
{
    internal class SampleServiceCreator
    {
        public static string GetServiceConfiguration(string projectName)
        {
            return @$"using {projectName}.Models.Contracts;
using {projectName}.Repository.Interface;
using {projectName}.Services.Helpers;
using {projectName}.Services.Interface;

namespace {projectName}.Services.Implementation
{{
    public class SampleService : ISampleService
    {{
        private readonly ISampleRepository _sampleRepository;
        private readonly ResponseHelper _responseHelper;

        public SampleService(ISampleRepository sampleRepository)
        {{
            this._sampleRepository = sampleRepository;
            this._responseHelper = new ResponseHelper();
        }}

        public BaseResponse GetSampleData()
        {{
            var data = _sampleRepository.GetSampleData();
            return data != null ? _responseHelper.HandleSuccess(data, ""Sample data fetched successfully"") :
                _responseHelper.HandleNotFound(""Sample"");
        }}
    }}
}}

";
        }
    }
}
