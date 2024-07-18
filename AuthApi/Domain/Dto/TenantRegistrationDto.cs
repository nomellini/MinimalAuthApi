using AuthApi.Domain.Entities;

namespace AuthApi.Domain.Dto
{
    public class TenantRegistrationDto 
    {
        public string? TenantName { get; set; }
        public bool isActive { get; set; } = false;
    }
}
