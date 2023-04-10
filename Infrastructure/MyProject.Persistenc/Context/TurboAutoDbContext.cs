using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyProject.Application.RequestParameters;
using MyProject.Domain.Entities;
using MyProject.Domain.Entities.Identity;
using System.Data;

namespace TurboAuto.Persistence.Contexts
{
    public class TurboAutoDbContext : IdentityDbContext<AppUser>
    {
        public TurboAutoDbContext(DbContextOptions<TurboAutoDbContext> options) : base(options)
        { }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<CarImageFile> CarImageFiles { get; set; }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<BanType> BanTypes { get; set; }
        public DbSet<FilterAnnouncements> FilterAnnouncements { get; set; }

        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<FilterAnnouncements>().HasNoKey();
            builder.Ignore<IdentityRole>();
            builder.Ignore<IdentityUserRole<string>>();
          
            builder.Entity<Announcement>(car =>
            {
                car
                .HasOne(c => c.AppUser)
                .WithMany(au => au.Announcements)
                .HasForeignKey(c => c.AppUserId);

                car.Property(x => x.Price).HasColumnType("decimal(18,2)");
            });
            builder.Entity<Currency>(curr =>
            {
                curr
                .HasMany(c => c.Announcements)
                .WithOne(an => an.Currency)
                .HasForeignKey(c => c.CurrencyId);
            });
            builder.Entity<CarImageFile>(carImage =>
            {
                carImage
                .HasOne(i => i.Announcement)
                .WithMany(c => c.CarImageFiles)
                .HasForeignKey(i => i.AnnouncementId);
            });
            builder.Entity<Make>(make =>
            {
                make
                .HasMany(m => m.Models)
                .WithOne(m => m.Make)
                .HasForeignKey(m => m.MakeId);
                make.Ignore(x => x.CreatedDate);
                make.Ignore(x => x.UpdatedDate);
            });
            builder.Entity<Model>(model =>
            {
                model
                .HasMany(m => m.Announcements)
                .WithOne(m => m.Model)
                .HasForeignKey(m => m.ModelId);
                model.Ignore(x => x.CreatedDate);
                model.Ignore(x => x.UpdatedDate);
            });
            builder.Entity<BanType>().HasData(new List<BanType>
            {
                new BanType{ Ban = "Avtobus" , Id = 1 },
                new BanType{ Ban = "Furqon" , Id = 2 },
                new BanType{ Ban = "Kupe" , Id = 3},
                new BanType{ Ban = "Sedan" , Id = 4},
                new BanType{ Ban = "Suv" , Id =5},
                new BanType{ Ban = "Universal", Id =6 }
            });
            builder.Entity<Currency>().HasData(new List<Currency>
            {
                new Currency(){ CurrencyCode = "Azn", Id = 1 },
                new Currency(){ CurrencyCode = "Usd", Id =2 },
                new Currency(){ CurrencyCode = "Euro", Id =3 }
            });
            builder.Entity<Make>().HasData(new List<Make>
            {
                new Make(){ Id = 1, MakeName = "Audi"},
                new Make(){ Id = 2, MakeName = "Bmw"},
                new Make(){ Id = 3, MakeName = "Mercedes"},
            });
            builder.Entity<Model>().HasData(new List<Model>
            {
                new Model(){ Id = 1, ModelName = "A5", MakeId = 1},
                new Model(){ Id = 2, ModelName = "Q5", MakeId = 1},
                new Model(){ Id = 3, ModelName = "RS3", MakeId = 1},
                new Model(){ Id = 4, ModelName = "RS7", MakeId = 1},
                new Model(){ Id = 5, ModelName = "Q5 Sportback", MakeId = 1},
                new Model(){ Id = 6, ModelName = "730Le", MakeId = 2},
                new Model(){ Id = 7, ModelName = "M5", MakeId = 2},
                new Model(){ Id = 8, ModelName = "X6", MakeId = 2},
                new Model(){ Id = 9, ModelName = "320Gt", MakeId = 2},
                new Model(){ Id = 10, ModelName = "C180", MakeId = 3},
                new Model(){ Id = 11, ModelName = "C240", MakeId = 3},
                new Model(){ Id = 12, ModelName = "CL 63 AMG", MakeId = 3},
                new Model(){ Id = 13, ModelName = "CLA 180", MakeId = 3},
                new Model(){ Id = 14, ModelName = "E 180", MakeId = 3},
            });

        }
    }
}
