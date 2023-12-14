using System.ComponentModel.DataAnnotations;

namespace BooksStore.Data.Models
{
    public class LoginDTO
    {

        [Required]
        public string UserName { get; set; }
        [Required]

        public string Password { get; set; }
    }
}
