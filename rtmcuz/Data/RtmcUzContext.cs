using Microsoft.EntityFrameworkCore;
using rtmcuz.Models;

namespace rtmcuz.Data
{
    public partial class RtmcUzContext : DbContext
    {
        public RtmcUzContext(DbContextOptions<RtmcUzContext> options) : base(options)
        {
        }

        public virtual DbSet<Page> Pages { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ef identity tables renamed
            //modelBuilder.Entity<Page>().ToTable("Pages");

            //new BasketMap().Map(builder);
            //OnModelCreatingPartial(modelBuilder);
        }
        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


    }
}
