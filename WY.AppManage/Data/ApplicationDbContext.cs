using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WY.AppManage.Models;
using AppManage.Model;

namespace WY.AppManage.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public virtual  DbSet<Project> Project { get; set; }
        public virtual  DbSet<App> App { get; set; }
        public virtual DbSet<Carousel> Carousel { get; set; }
        public DbSet<Suggest> Suggest { get; set; }
    }

    public static class SeedData
    {
        public async static Task Initialize(IServiceProvider service)
        {

            using (var context = (ApplicationDbContext)service.GetService(typeof(ApplicationDbContext)))
            {
                if (context.Project.Any())
                {
                    return;   // 已经初始化过数据
                }
                context.Project.AddRange(
                 new Project
                 {
                     Name = "Fonour集团总部1",
                     CreateTime = DateTime.Now,
                     App = new List<App>()
                     {
                         new App
                         {
                              Name="App1",
                              CreateTime=DateTime.Now
                         },
                         new App
                         {
                              Name="App2",
                              CreateTime=DateTime.Now
                         },
                           new App
                         {
                              Name="App3",
                              CreateTime=DateTime.Now
                         }
                     }

                 },
                 new Project
                 {
                     Name = "Fonour集团总部2",
                     CreateTime = DateTime.Now,
                     App = new List<App>()
                     {
                         new App
                         {
                              Name="App1",
                              CreateTime=DateTime.Now
                         },
                         new App
                         {
                              Name="App2",
                              CreateTime=DateTime.Now
                         },
                           new App
                         {
                              Name="App3",
                              CreateTime=DateTime.Now
                         }
                     }

                 },
                 new Project
                 {
                     Name = "Fonour集团总部3",
                     CreateTime = DateTime.Now,
                     App = new List<App>()
                     {
                         new App
                         {
                              Name="App1",
                              CreateTime=DateTime.Now
                         },
                         new App
                         {
                              Name="App2",
                              CreateTime=DateTime.Now
                         },
                           new App
                         {
                              Name="App3",
                              CreateTime=DateTime.Now
                         }
                     }
                 }
            );
                await context.SaveChangesAsync();
            }
        }
    }
}
