
using Microsoft.AspNetCore.Mvc;
using VirtualPet.Application.HandlerResponse;

namespace VirtualPet.Api.Controllers
{
    [ResponseCache(CacheProfileName = "Never")]
    public abstract class BaseApiController : ControllerBase
    {
        public IActionResult ReturnResponse<T>(HandlerResponse<T> response)
        {
            if (response.ResultType == ResultType.NotFound)
            {
                return NotFound();
            }

            return Ok(response.Result);
        }


        public IActionResult ReturnResponse<T>(T responseObj)
        {

            if (responseObj == null)
            {
                return NotFound();
            }

            return Ok(responseObj);
        }

    }
}
