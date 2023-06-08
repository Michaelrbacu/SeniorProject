using Microsoft.AspNetCore.Identity;

namespace NetCoreIdentity.Data.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public DateTime CreatedTime { get; set; }
    }
}
