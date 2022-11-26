using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaparaThirdWeek.Services.DTOs;
using System;

namespace PaparaThirdWeek.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        [HttpPost("Developer")]
        public IActionResult Post(DeveloperDto model)
        {
            //throw new ArgumentNullException("Alanlar boş geçilemez");


            return Ok(model);
        }
    }
}
