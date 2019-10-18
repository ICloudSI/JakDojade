using System;
using JakDojade.Infrastructure.Dto;

namespace JakDojade.Infrastructure.Services
{
    public interface IJwtHandler
    {
        JwtDto CreateToken(Guid userId, string role);
    }
}