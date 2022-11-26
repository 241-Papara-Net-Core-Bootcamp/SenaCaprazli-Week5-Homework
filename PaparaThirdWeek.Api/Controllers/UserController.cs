using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaparaThirdWeek.Services.Abstracts;
using PaparaThirdWeek.Services.DTOs;
using System.Collections.Generic;

namespace PaparaThirdWeek.Api.Controllers
{
    [Authorize]  // bu attrbiute en başa yazdığımız için tüm actionlar için geçerli normalde ve erişimi engeller
                 // yetkilendirme dışında bırakmak istediğimiz actionun başına [AllowAnonymous] eklemeliyiz.

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ITokenServices tokenServices;

        public UserController(ITokenServices tokenServices)
        {
            this.tokenServices = tokenServices;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate(UserDto userDto)
        {
            var token = tokenServices.Authenticate(userDto);
            if (token == null)
            {
                return Unauthorized("Yetkisiz giriş");
            }
            return Ok(token);
        }

        [HttpGet("Users")]
        public IActionResult Get()
        {
            var users = new List<string>
            {
                "Fatma",
                "Samet",
                "Elif",
                "Damle",
                "Ayşe"
            };
            return Ok(users);
        }
    }
}
