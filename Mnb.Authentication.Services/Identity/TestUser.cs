using Microsoft.AspNetCore.Identity;

namespace Mnb.Authentication.Services.Identity
{
    public class TestUser : IdentityUser
    {
        public string TestDescrition { get; set; }
    }
}
