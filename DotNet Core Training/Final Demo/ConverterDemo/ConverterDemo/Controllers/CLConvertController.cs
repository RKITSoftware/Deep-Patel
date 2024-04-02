using ConverterDemo.Dto;
using ConverterDemo.Extension;
using ConverterDemo.Model;
using Microsoft.AspNetCore.Mvc;

namespace ConverterDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CLConvertController : ControllerBase
    {
        [HttpPost("ToPoco")]
        public ActionResult ToPoco(DTOUSR01 objDto)
        {
            USR01 objuser = objDto.ToConvert<USR01>();
            return Ok(objuser);
        }

        [HttpPost("ToDto")]
        public ActionResult ToDto(USR01 objUser)
        {
            DTOUSR01 objDto = objUser.ToConvert<DTOUSR01>();
            return Ok(objDto);
        }
    }
}
