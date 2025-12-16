using HotelBooking.Domain.Contracts;
using HotelBooking.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Infrastructure.Identity.DataSeed
{
    public class IdentityDataInitializer : IDataInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<IdentityDataInitializer> _logger;

        public IdentityDataInitializer(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<IdentityDataInitializer> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }
        public async Task InitializeAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));

                if (!_userManager.Users.Any())
                {
                    var userAdmin = new ApplicationUser
                    {
                        UserName = "YehiaNourEldin",
                        Email = "YehiaNourEldin@gmail.com",
                        PhoneNumber = "01142394329"
                    };

                    await _userManager.CreateAsync(userAdmin, "P@ssw0rd");

                    await _userManager.AddToRoleAsync(userAdmin, "Admin");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while seeding identity database : Message = {ex.Message} ");
            }
        }
    }
}
