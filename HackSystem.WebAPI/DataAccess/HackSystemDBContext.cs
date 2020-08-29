using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HackSystem.WebAPI.DataAccess
{
    public class HackSystemDBContext : IdentityDbContext
    {
        public HackSystemDBContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
