using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using rtmcuz.Areas.Identity.Pages.Account;
using rtmcuz.Data.Enums;
using rtmcuz.Data.Models;
using rtmcuz.ViewModels;
using System.Security.Cryptography;

namespace rtmcuz.Data
{
    public partial class RtmcUzContext : IdentityDbContext<IdentityUser>
    {
        public RtmcUzContext(DbContextOptions<RtmcUzContext> options) : base(options)
        {
        }

        public virtual DbSet<Section> Sections { get; set; } = null!;
        public virtual DbSet<Attachment> Attachments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            const string email = "rtmcuz@admin1";

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Section>().Property(u => u.Status).HasDefaultValue(SectionStatus.Active);
            //modelBuilder.Entity<Section>().Property(u => u.Lang).HasDefaultValue(Locales.uz_Latn_UZ);
            modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser[] {
                new IdentityUser{
                    UserName = email,
                    Email = email,
                    NormalizedEmail = email.ToUpper(),
                    NormalizedUserName = email.ToUpper(),
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "rtmcuzAdmin12345+"),
                },
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.HasOne(d => d.Image)
                    .WithOne(p => p.Section)
                    .HasForeignKey<Section>(d => d.ImageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Sections_fk0");
            });
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e =>
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified);

            foreach (var entityEntry in entries)
            {
                entityEntry.Property("UpdatedDate").CurrentValue = DateTimeOffset.UtcNow;

                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Property("CreatedDate").CurrentValue = DateTimeOffset.UtcNow;
                }
            }

            return base.SaveChanges();
        }

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
