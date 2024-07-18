namespace AuthApi.Domain.Entities
{
    public class Tenant: EntityBaseComId
    {
        public string TenantName { get; set; } = string.Empty;
        public bool isActive { get; set; }  = false;

    }
}
