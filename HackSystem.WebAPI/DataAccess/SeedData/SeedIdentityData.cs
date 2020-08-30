using System;
using System.Linq;
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
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var logger = services.GetRequiredService<ILogger<SeedIdentityData>>();

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

            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

            // 初始化用户
            foreach ((string name, string email, string password, string[] roles) in new[]
             {
                ("CMD", "commander@hack.com", "12345@Qq", new[]{ CommonSense.Roles.CommanderRole }),
                ("Leon", "leon@hack.com", "12345@Qq", new[]{ CommonSense.Roles.CommanderRole, CommonSense.Roles.HackerRole }),
                ("Mathilda", "mathilda@hack.com", "12345@Qq", new[]{ CommonSense.Roles.HackerRole })
             })
            {
                try
                {
                    if (await userManager.FindByEmailAsync(email) == null)
                    {
                        var user = new IdentityUser(name) { Email = email };
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

                        foreach (var role in roles)
                        {
                            result = await userManager.AddToRoleAsync(user, role);
                            if (result.Succeeded)
                            {
                                logger.LogInformation($"加入角色 {roles} 成功");
                            }
                            else
                            {
                                logger.LogWarning($"加入角色 {roles} 失败，异常如下：\n\t{string.Join("\n\t", result.Errors.Select(error => error.Description))}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.LogWarning($"初始化用户遇到异常：{ex.Message}");
                }
            }
        }
    }
}
