namespace AuthApi.Domain.Dto
{
    public class TenantAssignRequest
    {
        public string? Email { get; set; }
        public string? TenantName { get; set; }
    }
}
