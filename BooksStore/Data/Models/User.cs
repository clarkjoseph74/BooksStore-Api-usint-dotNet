using Microsoft.AspNetCore.Identity;

namespace BooksStore.Data.Models
{
    public class User : IdentityUser
    {
        public int Id { get; set; }
    }
}
