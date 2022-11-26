

using System.ComponentModel.DataAnnotations.Schema;

namespace PaparaThirdWeek.Domain.Entities
{
    [Table("Users")]
    public class User : BaseEntity
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string Password { get; set; }
      
    }
}
