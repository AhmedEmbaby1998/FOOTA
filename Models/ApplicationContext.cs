using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebApplication6.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Consumer> Consumers { set; get; }
        public DbSet<FileData> FileDatas { set; get; }
        public DbSet<Line> Lines { set; get; }
        public DbSet<Reading> Readings { set; get; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Line>()
                .HasKey(l => new { l.FileDataId, l.LineNum });
            builder.Entity<Reading>()
                .HasKey(r => new { r.FileDataId, r.ConsumerId });
            builder.Entity<FileData>()
                .HasIndex(f => f.UploadingDate).IsUnique();


        }
    }
}
