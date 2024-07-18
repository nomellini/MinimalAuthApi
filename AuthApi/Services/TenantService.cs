using AuthApi.Domain.Dto;
using AuthApi.Domain.Entities;
using AuthApi.Domain.Identity;
using AuthApi.Interfaces;
using AuthApi.Repository;
using Microsoft.AspNetCore.Identity;

namespace AuthApi.Services
{
    public class TenantService : ITenantService
    {
        public async Task<ServiceResponse<Tenant>> TenantCreate(Tenant tenant)
        {
            throw new NotImplementedException();
        }
    }
}

