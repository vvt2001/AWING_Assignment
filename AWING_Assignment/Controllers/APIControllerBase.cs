using Microsoft.AspNetCore.Mvc;

namespace AWING_Assignment_API.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        private readonly ILogger _logger;

        public ApiControllerBase()
        {

        }

        public ApiControllerBase(ILogger logger)
        {
            _logger = logger;
        }

        protected ActionResult HandleException(Exception ex)
        {
            _logger?.LogError(ex, "Exception while processing: {@message}", ex.Message);

            if (ex is InvalidProgramException)
            {
                return Problem(ex.Message, null, 400);
            }

            return Problem("Xảy ra lỗi trong quá trình xử lý", null, 500);
        }
    }
}