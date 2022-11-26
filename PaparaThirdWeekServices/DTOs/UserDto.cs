

using Newtonsoft.Json;

namespace PaparaThirdWeek.Services.DTOs
{
    public class UserDto
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("email")]
        public string email { get; set; }
        [JsonProperty("address")]
        public string address { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }

    }
}
