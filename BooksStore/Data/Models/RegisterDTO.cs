using System.ComponentModel.DataAnnotations;

namespace BooksStore.Data.Models
{
    public class RegisterDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]

        public string Password { get; set; }
        public string? PhoneNumber { get; set; }

    }
}
