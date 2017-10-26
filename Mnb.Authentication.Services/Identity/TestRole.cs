using Microsoft.AspNetCore.Identity;

namespace Mnb.Authentication.Services.Identity
{
    public class TestRole : IdentityRole
    {
        public string Description { get; set; }
    }
}
