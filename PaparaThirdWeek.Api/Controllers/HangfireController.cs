using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PaparaThirdWeek.Services.Abstracts;
using PaparaThirdWeek.Services.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PaparaThirdWeek.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangfireController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IConfiguration _config;

        public HangfireController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;

        }

        [HttpGet("RetrieveDataFromAPI")]
        public void RetrieveDataFromAPI()
        {
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(string.Format("https://jsonplaceholder.typicode.com/users"));
            webReq.Method = "GET";
            HttpWebResponse webRes = (HttpWebResponse)webReq.GetResponse();
            Console.WriteLine(webRes.StatusCode);
            Console.WriteLine(webRes.Server);

            string jsonString;
            using (Stream stream = webRes.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }

            List<UserDto> items = (List<UserDto>)JsonConvert.DeserializeObject(jsonString, typeof(List<UserDto>));
            //_userService.Add(items[0]);
            Console.WriteLine(items);
            Console.WriteLine($"data has been retrieved from external API");
        }

        [HttpGet("RetrieveData")]
        public IActionResult RetrieveData()
        {
            RecurringJob.AddOrUpdate(() => RetrieveDataFromAPI(), _config.GetConnectionString("CronTime"));
            return Ok($"Data will be retrieved from external API every 5 minutes ");
        }
    }
}
