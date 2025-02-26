using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCreator.Helpers.ClassesGenerators.API
{
    public class SampleControllerCreator
    {
        public static string GetSampleControllerConfiguration(string projectName, bool isSinglelayer = true)
        {
            var import = isSinglelayer ? projectName : projectName.Replace(".API", "");
            return $@"
using {import}.Services.Helpers;
using {import}.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace {projectName}.Controllers
{{
    [ApiController]
    [Route(""[controller]"")]
    public class SampleController : ControllerBase
    {{
        private readonly ISampleService _sampleService;
        private readonly ResponseHelper _responseHelper;

        public SampleController(ISampleService sampleService)
        {{
            _sampleService = sampleService;
            _responseHelper = new ResponseHelper();
        }}

        [HttpGet]
        public IActionResult Get()
        {{
            var data = _sampleService.GetSampleData();
            return _responseHelper.HandleResponse(this, data);
        }}
    }}
}}
";
        }
    }
}
