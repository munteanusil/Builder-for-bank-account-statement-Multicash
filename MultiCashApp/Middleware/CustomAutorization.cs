﻿using Microsoft.AspNetCore.Authorization;
using MultiCashApp.Helpers;
using System.Security.Claims;
using MultiCashApp.Data;

namespace MultiCashApp.Middleware
{
    public class CustomAutorization : AuthorizationHandler<RoleRequirement>
    {
        private readonly DatabaseContext _context; 

        public CustomAutorization(DatabaseContext context)
        {
            _context = context;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Name && c.Issuer == "AD AUTHORITY"))
            {
                return Task.FromResult(0);
            }

            var username = context.User.FindFirst(c => c.Type == ClaimTypes.Name && c.Issuer == "AD AUTHORITY").Value.Replace("MMRMAKITA\\", "");
            var roles = requirement.RoleNames;
            var rolesId = 999;
            var userId = _context.Users.First(r => r.UserName == username).Id;
            foreach( var role in roles )
            {
                rolesId = _context.Roles.First(r => r.Name == role).Id; 
                if (_context.Users.FirstOrDefault(u => u.Id == userId).RoleId == rolesId) 
                {
                    context.Succeed(requirement);
                    return Task.FromResult(0);
                }
            }
            return Task.FromResult(0);

        }

    }
}
