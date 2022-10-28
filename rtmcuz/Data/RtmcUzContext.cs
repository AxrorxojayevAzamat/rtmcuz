﻿using Microsoft.EntityFrameworkCore;
using rtmcuz.Models;
using rtmcuz.FormModels;
using rtmcuz.Data.Enums;

namespace rtmcuz.Data
{
    public partial class RtmcUzContext : DbContext
    {
        public RtmcUzContext(DbContextOptions<RtmcUzContext> options) : base(options)
        {
        }

        public virtual DbSet<Section> Sections { get; set; } = null!;

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
        }

        public DbSet<rtmcuz.FormModels.Interactive> Interactive { get; set; }

        public DbSet<rtmcuz.FormModels.News> News { get; set; }
        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


    }
}
