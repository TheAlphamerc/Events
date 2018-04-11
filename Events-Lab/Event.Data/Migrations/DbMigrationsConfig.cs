namespace Events.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Runtime.Remoting.Contexts;

    public sealed class DbMigrationsConfig : DbMigrationsConfiguration<Data.ApplicationDbContext>
    {
        public DbMigrationsConfig()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Data.ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                var adminEmail = "admin@admin.com";
                var adminUserName = adminEmail;
                var adminFullName = "System Administrator";
                var adminPassword = adminEmail;
                string adminRole = "Administrator";

                CreateAdminUSer(context,adminEmail,adminFullName,adminPassword,adminRole,adminUserName);
                CreateSeverelEvents(context);
            }
        }

        private void CreateSeverelEvents(ApplicationDbContext context)
        {
            context.Events.Add(new Event()
            {
                // throw new NotImplementedException();
                Title = "Party @ SoftUni",
                StartDateTIme = DateTime.Now.Date.AddDays(5).AddHours(21).AddMinutes(30),
                Author = context.Users.First(),
                Description = "Loreus Ipsum"
            });

            context.Events.Add(new Event()
            {
                // throw new NotImplementedException();
                Title = "Passed Event <Anonymous>",
                StartDateTIme = DateTime.Now.Date.AddDays(-2).AddHours(10).AddMinutes(30),
                Duration = TimeSpan.FromHours(1.5),
                Comments = new HashSet<Comment>
                {
                    new Comment(){ Text = "<Anonymous> comment"},
                    new Comment(){ Text = "User Comment", Author = context.Users.First()}
                },
                 Description = "Loreus Ipsum"

            });

        }

        private void CreateAdminUSer(ApplicationDbContext context, string adminEmail, string adminFullName, string adminPassword, string adminRole, string adminUserName)
        {
            //  throw new NotImplementedException();
            var adminuser = new ApplicationUser
            {
                UserName = adminUserName,
                Fullname = adminFullName,
                Email = adminEmail
            };
            var userStore   = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 1,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false
            };

            var userCresteResult = userManager.Create(adminuser, adminPassword);
            if (!userCresteResult.Succeeded)
            {
                throw new Exception(string.Join(";", userCresteResult.Errors));
            }

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var roleCreateResult = roleManager.Create(new IdentityRole(adminRole));
            if (!roleCreateResult.Succeeded)
            {
              throw new Exception(string.Join(";", roleCreateResult.Errors));
            }

            var addAdminRoleResult = userManager.AddToRole(adminuser.Id, adminRole);
            if (!addAdminRoleResult.Succeeded)
            {
                throw new Exception(string.Join(";", addAdminRoleResult.Errors));
            }

         
        }

        
    }
}
