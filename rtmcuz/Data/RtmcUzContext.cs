using Microsoft.EntityFrameworkCore;
using rtmcuz.Data.Enums;
using rtmcuz.Data.Models;
using rtmcuz.ViewModels;

namespace rtmcuz.Data
{
    public partial class RtmcUzContext : DbContext
    {
        public RtmcUzContext(DbContextOptions<RtmcUzContext> options) : base(options)
        {
        }

        public virtual DbSet<Section> Sections { get; set; } = null!;
        public virtual DbSet<Attachment> Attachments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Section>().Property(u => u.Status).HasDefaultValue(SectionStatus.Active);
            modelBuilder.Entity<Section>().Property(u => u.Lang).HasDefaultValue(Locales.UZ);
            //modelBuilder.Entity<Page>(entity =>
            //{
            //    entity.HasOne(d => d.Parent)
            //        .WithMany(p => p.Children)
            //        .HasForeignKey(d => d.ParentId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("Page_fk0");
            //});
            // ef identity tables renamed
            //modelBuilder.Entity<Page>().ToTable("Pages");

            //new BasketMap().Map(builder);
            //OnModelCreatingPartial(modelBuilder);
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
