using System.Collections.Generic;

namespace StarbucksMobileApp.Api.ResponseModels
{
    public class BaseResponseModel
    {
        public List<ErrorRequestModel> Errors { get; set; }
    }

    public class ErrorRequestModel
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
