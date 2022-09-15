﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskoMask.Services.Identity.Domain.Entities;

namespace TaskoMask.Services.Identity.Infrastructure.Data.DataProviders
{

    /// <summary>
    /// 
    /// </summary>
    public static class DbSeedData
    {


        /// <summary>
        /// Seed the necessary data that system needs
        /// </summary>
        public static void SeedEssentialData(IServiceProvider serviceProvider)
        {
            using var serviceScope = serviceProvider.CreateScope();
            var configuration = serviceScope.ServiceProvider.GetService<IConfiguration>();
            var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();

            SeedSuperUser(userManager, configuration).Wait();
        }



        /// <summary>
        /// 
        /// </summary>
        private async static Task SeedSuperUser(UserManager<User> userManager, IConfiguration configuration)
        {
            var superUserEmail = configuration["Identity:SuperUser:Email"];
            var superUserName = configuration["Identity:SuperUser:UserName"];

            if (await userManager.FindByEmailAsync(superUserEmail) == null)
            {
                var superUser = new User
                {
                    UserName = superUserName,
                    Email = superUserEmail,
                    IsActive = true,
                };

                var password = configuration["Identity:SuperUser:Password"];
                await userManager.CreateAsync(superUser, password);
            }
        }
    }
}