﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCreator.Helpers.ClassesGenerators
{
    public class BaseResponseCreator
    {
        public static string GetBaseResponseConfiguration(string projectName)
        {
            return @$"
using System.Net;

namespace {projectName}.Models.Contracts
{{
    public class BaseResponse
    {{
        public virtual string Message {{ get; set; }} = string.Empty;
        public virtual string CorrelationId {{ get; set; }} = string.Empty;
        public virtual bool Success {{ get; set; }} = false;
        public virtual object Data {{ get; set; }} = false;
        public virtual HttpStatusCode StatusCode {{ get; set; }} = HttpStatusCode.OK;
    }}
}}
";
        }
    }
}
