using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HackSystem.WebAPI.Model.Identity;
using HackSystem.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

namespace HackSystem.WebAPI.DataAccess.SeedData
{
    public static class IdentityDatabaseInitializer
    {
        public static IHost InitializeIdentityData(this IHost host)
        {
            InitializeIdentityDataAsync(host).ConfigureAwait(false);

            return host;
        }

        /// <summary>
        /// Initinalize identity data
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static async Task InitializeIdentityDataAsync(IHost host)
        {
            var serviceScope = host.Services.CreateScope();
            var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<HackSystemDBContext>>();
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<HackSystemUser>>();
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<HackSystemRole>>();

            // Initinal roles
            foreach (string roleName in new string[]
            {
                CommonSense.Roles.CommanderRole,
                CommonSense.Roles.HackerRole
            })
            {
                try
                {
                    if (!await roleManager.RoleExistsAsync(roleName))
                    {
                        var role = new HackSystemRole(roleName) { NormalizedName = roleName };
                        var result = await roleManager.CreateAsync(role);

                        if (result.Succeeded)
                        {
                            logger.LogInformation($"Add role => {role.Name}");
                        }
                        else
                        {
                            logger.LogError($"Failed to add role {role.Name}. \tException: \n\t{string.Join("\n\t", result.Errors.Select(error => error.Description))}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError($"Exception when add role: {ex.Message}");
                }
            }

            // Initinalize users
            foreach ((string userName, string email, string password, string[] roles) in new[]
            {
                ("CMD", "commander@hack.com", "12345@Qq", new[]{ CommonSense.Roles.CommanderRole }),
                ("Leon", "leon@hack.com", "12345@Qq", new[]{ CommonSense.Roles.CommanderRole, CommonSense.Roles.HackerRole }),
                ("Mathilda", "mathilda@hack.com", "12345@Qq", new[]{ CommonSense.Roles.HackerRole })
            })
            {
                try
                {
                    if (await userManager.FindByNameAsync(userName) == null)
                    {
                        var user = new HackSystemUser(userName) { Email = email };
                        var result = await userManager.CreateAsync(user);
                        if (result.Succeeded)
                        {
                            logger.LogInformation($"Add user => {user.UserName}");
                        }
                        else
                        {
                            logger.LogWarning($"Failed to add user {user.UserName}. \tException: \n\t{string.Join("\n\t", result.Errors.Select(error => error.Description))}");
                        }

                        result = await userManager.AddPasswordAsync(user, password);
                        if (result.Succeeded)
                        {
                            logger.LogInformation($"Update user password successfully.");
                        }
                        else
                        {
                            logger.LogWarning($"Failed to update password. Exception: \n\t{string.Join("\n\t", result.Errors.Select(error => error.Description))}");
                        }

                        result = await userManager.AddToRolesAsync(user, roles);
                        if (result.Succeeded)
                        {
                            logger.LogInformation($"Add user {user.UserName} to roles {string.Join(",", roles)} successfully");
                        }
                        else
                        {
                            logger.LogWarning($"Add user {user.UserName} to roles {string.Join(",", roles)} failed. Exception: \n\t{string.Join("\n\t", result.Errors.Select(error => error.Description))}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.LogWarning($"Exception when initinalize users: {ex.Message}");
                }
            }

            // Ass user claims
            foreach ((string userName, Claim[] claims) in new[]
            {
                ("Leon", new[]{ new Claim(CommonSense.Claims.ProfessionalClaim, "true") }),
            })
            {
                try
                {
                    var user = await userManager.FindByNameAsync(userName);
                    if (user != null)
                    {
                        var userClaims = (await userManager.GetClaimsAsync(user))
                            .GroupBy(claim => claim.Type)
                            .Select(group => new Tuple<string, HashSet<string>>(group.Key, new HashSet<string>(group.Select(claim => claim.Value))))
                            .ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);

                        var newClaims = claims
                            .GroupBy(claim => claim.Type)
                            .Select(group => new Tuple<string, HashSet<string>>(group.Key, new HashSet<string>(group.Select(claim => claim.Value))))
                            .ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);

                        foreach (var type in userClaims.Keys)
                        {
                            if (newClaims.TryGetValue(type, out var values))
                            {
                                values.ExceptWith(userClaims[type]);
                            }
                        }

                        var addClaims = newClaims.SelectMany(pair => pair.Value.Select(value => new Claim(pair.Key, value))).ToList();
                        if (addClaims.Count > 0)
                        {
                            var result = await userManager.AddClaimsAsync(user, addClaims);
                            if (result.Succeeded)
                            {
                                logger.LogInformation($"Add user claims successfully: {user.UserName}");
                            }
                            else
                            {
                                logger.LogWarning($"Failed to add user claims {user.UserName}. \tException: \n\t{string.Join("\n\t", result.Errors.Select(error => error.Description))}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.LogWarning($"Exception when add user claims: {ex.Message}");
                }
            }

            // Add role claims
            foreach ((string roleName, Claim[] claims) in new[]
            {
                (CommonSense.Roles.CommanderRole, new[]{ new Claim(CommonSense.Claims.ProfessionalClaim, "true") }),
            })
            {
                try
                {
                    var role = await roleManager.FindByNameAsync(roleName);
                    if (role != null)
                    {

                        var roleClaims = (await roleManager.GetClaimsAsync(role))
                            .GroupBy(claim => claim.Type)
                            .Select(group => new Tuple<string, HashSet<string>>(group.Key, new HashSet<string>(group.Select(claim => claim.Value))))
                            .ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);

                        var newClaims = claims
                            .GroupBy(claim => claim.Type)
                            .Select(group => new Tuple<string, HashSet<string>>(group.Key, new HashSet<string>(group.Select(claim => claim.Value))))
                            .ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);

                        foreach (var type in roleClaims.Keys)
                        {
                            if (newClaims.TryGetValue(type, out var values))
                            {
                                values.ExceptWith(roleClaims[type]);
                            }
                        }

                        var addClaims = newClaims.SelectMany(pair => pair.Value.Select(value => new Claim(pair.Key, value))).ToList();
                        if (addClaims.Count > 0)
                        {
                            foreach (var claim in addClaims)
                            {
                                var result = await roleManager.AddClaimAsync(role, claim);
                                if (result.Succeeded)
                                {
                                    logger.LogInformation($"Add role claims successfully: {role.Name}");
                                }
                                else
                                {
                                    logger.LogWarning($"Failed to add role claims {role.Name}. \tException: \n\t{string.Join("\n\t", result.Errors.Select(error => error.Description))}");
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.LogWarning($"Exception when initinalize role claims: {ex.Message}");
                }
            }
        }
    }
}
