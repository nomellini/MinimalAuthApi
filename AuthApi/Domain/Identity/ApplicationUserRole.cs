﻿using Microsoft.AspNetCore.Identity;

namespace AuthApi.Domain.Identity
{
    public class ApplicationUserRole: IdentityUserRole<int>
    {
        public ApplicationUser? User { get; set; }
        public ApplicationRole? Role { get; set; }
    }
}
