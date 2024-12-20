using AWING_Assignment_API.Services;
using AWING_Assignment_API.View;
using AWING_Assignment_API.Wrappers;
using AWING_Assignment_Data.Model;
using Microsoft.AspNetCore.Mvc;

namespace AWING_Assignment_API.Controllers
{
    [Route("main")]
    [ApiController]
    public class PirateNavigationController : ApiControllerBase
    {
        private readonly IPirateNavigationServices _pirateNavigationServices;
        public PirateNavigationController(IPirateNavigationServices pirateNavigationServices)
        {
            _pirateNavigationServices = pirateNavigationServices;
        }

        [HttpPost("navigate")]
        public async Task<ActionResult> Navigate([FromBody] Input input)
        {
            try
            {
                return Ok(new Response<string>(await _pirateNavigationServices.Navigate(input)));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet("get-history")]
        public async Task<ActionResult> GetHistories()
        {
            try
            {
                return Ok(new Response<List<InputHistory>>(await _pirateNavigationServices.GetHistories()));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPost("save-to-database")]
        public async Task<ActionResult> SaveToDataBase(Input input)
        {
            try
            {
                return Ok(new Response<bool>(await _pirateNavigationServices.SaveToDataBase(input)));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}
