using Microsoft.AspNetCore.Identity;

namespace Timelog.AspNetCore.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            UniqId = Guid.NewGuid();
        }
        public Guid UniqId { get; set; }
    }
}
