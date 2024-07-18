using AuthApi.Domain.Dto;
using AuthApi.Domain.Dto.Auth;
using AuthApi.Domain.Entities;
using AuthApi.Services;

namespace AuthApi.Interfaces
{
    public interface ITenantService
    {
        Task<ServiceResponse<Tenant>> TenantCreate(Tenant tenant);

    }
}
