using Microsoft.AspNetCore.Identity;

namespace Persistance;

public partial class Users : IdentityUser
{
    public string? Address { get; set; }

    public DateTime? Birthday { get; set; }

    public virtual ICollection<UserCart> UserCarts { get; set; } = new List<UserCart>();
}
