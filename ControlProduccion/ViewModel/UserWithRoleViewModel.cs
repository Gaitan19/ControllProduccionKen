using Microsoft.AspNetCore.Identity;

namespace ControlProduccion.ViewModel
{
    public class UserWithRoleViewModel
    {
        public IdentityUser User { get; set; }
        public string Role { get; set; } = string.Empty;
    }
}