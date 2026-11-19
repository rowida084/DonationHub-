using Application.Options;
using Domain.Constatnts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security;
using System.Text;


namespace Infrastructure.Identity
{
    public class IdentitySeeder
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOptions<SeedAdminOptions> _seedAdminOptions;

        public IdentitySeeder(RoleManager<IdentityRole<Guid>> roleManager
            ,UserManager<ApplicationUser> userManager
            ,IOptions<SeedAdminOptions> seedAdminOptions)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
            this._seedAdminOptions = seedAdminOptions;
        }

        public async Task SeedRolesAsync()
        {
            string[] roles =
             {
                ApplicationRoles.Admin,
                ApplicationRoles.CampaignManager,
                ApplicationRoles.Donor
            };

            foreach(string role in roles)
            {
                if(! await _roleManager.RoleExistsAsync(role))
                {
                    var identityRole = new IdentityRole<Guid>(role);
                    await _roleManager.CreateAsync(identityRole);
                }
            }
        }

        public async Task SeedAdminAsync()
        {
            SeedAdminOptions options = _seedAdminOptions.Value;

            // Check if admin already exists
            var admin = await _userManager.FindByEmailAsync(options.Email);

            // Create admin if it doesn't exist
            if (admin == null)
            {
                admin = ApplicationUser.Create(
                    email: options.Email,
                    fullName: options.FullName,
                    userName: options.Email,
                    createdAt: DateTime.UtcNow
                );
               
                var result = await _userManager.CreateAsync(admin, options.Password);

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(
                        string.Join(", ", result.Errors.Select(e => e.Description))
                    );
                }
            }

            // Ensure admin has the Admin role
            if (!await _userManager.IsInRoleAsync(admin, ApplicationRoles.Admin))
            {
                var roleResult = await _userManager.AddToRoleAsync(admin, ApplicationRoles.Admin);

                if (!roleResult.Succeeded)
                {
                    throw new InvalidOperationException(
                        string.Join(", ", roleResult.Errors.Select(e => e.Description))
                    );
                }
            }
        }

    }
}
