using Microsoft.AspNetCore.Identity;
using System;

namespace Timelog.WebApp.Models
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
