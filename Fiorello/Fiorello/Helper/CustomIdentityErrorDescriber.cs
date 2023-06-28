using Microsoft.AspNetCore.Identity;

namespace Fiorello.Helper
{
    public class CustomIdentityErrorDescriber:IdentityErrorDescriber
    {
        public virtual IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                Description = "simvol olmalidir"
            };
        }
        
    }
}
