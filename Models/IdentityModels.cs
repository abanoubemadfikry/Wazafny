﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Job_Offers_Website.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApplication2.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public virtual ICollection<ApplyForJob> ApplyJobs { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
        public string UserType { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
      
    }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Job_Offers_Website.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<Job_Offers_Website.Models.Job> Jobs { get; set; }

        public System.Data.Entity.DbSet<Job_Offers_Website.Models.RoleViewModel> RoleViewModels { get; set; }

        public System.Data.Entity.DbSet<Job_Offers_Website.Models.ApplyForJob> ApplyForJobs { get; set; }

        //public System.Data.Entity.DbSet<WebApplication2.Models.EditProfileViewModel> EditProfileViewModels { get; set; }

        //public System.Data.Entity.DbSet<WebApplication2.Models.ApplicationUser> ApplicationUsers { get; set; }

        //public System.Data.Entity.DbSet<WebApplication2.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}