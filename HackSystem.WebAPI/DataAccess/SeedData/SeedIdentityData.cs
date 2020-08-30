using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HackSystem.WebDTO.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HackSystem.WebAPI.DataAccess.SeedData
{
    public class SeedIdentityData
    {
        /// <summary>
        /// 初始化身份信息
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static async Task InitializeAsync(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var logger = services.GetRequiredService<ILogger<SeedIdentityData>>();

            // 初始化角色
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
                        var role = new IdentityRole(roleName) { NormalizedName = roleName };
                        var result = await roleManager.CreateAsync(role);

                        if (result.Succeeded)
                        {
                            logger.LogInformation($"添加角色成功：{role.Name}");
                        }
                        else
                        {
                            logger.LogError($"添加角色失败：{role.Name} \t异常如下：\n\t{string.Join("\n\t", result.Errors.Select(error => error.Description))}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError($"初始化角色遇到异常：{ex.Message}");
                }
            }

            // 初始化用户
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
                        var user = new IdentityUser(userName) { Email = email };
                        var result = await userManager.CreateAsync(user);
                        if (result.Succeeded)
                        {
                            logger.LogInformation($"添加用户成功：{user.UserName}");
                        }
                        else
                        {
                            logger.LogWarning($"添加用户失败：{user.UserName} \t异常如下：\n\t{string.Join("\n\t", result.Errors.Select(error => error.Description))}");
                        }

                        result = await userManager.AddPasswordAsync(user, password);
                        if (result.Succeeded)
                        {
                            logger.LogInformation($"修改密码成功");
                        }
                        else
                        {
                            logger.LogWarning($"修改密码失败，异常如下：\n\t{string.Join("\n\t", result.Errors.Select(error => error.Description))}");
                        }

                        result = await userManager.AddToRolesAsync(user, roles);
                        if (result.Succeeded)
                        {
                            logger.LogInformation($"加入角色 {roles} 成功");
                        }
                        else
                        {
                            logger.LogWarning($"加入角色失败，异常如下：\n\t{string.Join("\n\t", result.Errors.Select(error => error.Description))}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.LogWarning($"初始化用户遇到异常：{ex.Message}");
                }
            }

            // 添加用户声明
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
                        var result = await userManager.AddClaimsAsync(user, claims);
                        if (result.Succeeded)
                        {
                            logger.LogInformation($"添加用户声明成功：{user.UserName}");
                        }
                        else
                        {
                            logger.LogWarning($"添加用户声明失败：{user.UserName} \t异常如下：\n\t{string.Join("\n\t", result.Errors.Select(error => error.Description))}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.LogWarning($"添加用户声明遇到异常：{ex.Message}");
                }
            }

            // 添加角色声明
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
                        foreach (var claim in claims)
                        {
                            var result = await roleManager.AddClaimAsync(role, claim);
                            if (result.Succeeded)
                            {
                                logger.LogInformation($"添加角色声明成功：{role.Name}");
                            }
                            else
                            {
                                logger.LogWarning($"添加角色声明失败：{role.Name} \t异常如下：\n\t{string.Join("\n\t", result.Errors.Select(error => error.Description))}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.LogWarning($"添加角色声明遇到异常：{ex.Message}");
                }
            }
        }
    }
}
