using Chuvashia.NationalProjects.Model;
using Chuvashia.NationalProjects.Model.News;
using Microsoft.EntityFrameworkCore;

namespace Chuvashia.NationalProjects.Context
{
    public sealed class NationalProjectsDbContext : DbContext
    {
        public NationalProjectsDbContext(DbContextOptions options) : base(options) { }

        internal DbSet<Counter> Counters { get; set; }
        internal DbSet<Admin> Admins { get; set; }
        internal DbSet<NewsPost> NewsPosts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Counter>(builder =>
            {
                builder.ToTable("Counters");
                builder.HasKey(o => o.Id);
                builder.Property(o => o.Id)
                    .ValueGeneratedNever();
                builder.Property(o => o.Type)
                    .HasMaxLength(64)
                    .IsRequired();
                builder.Property(o => o.Amount)
                    .HasColumnType("decimal")
                    .IsRequired();
            });

            modelBuilder.Entity<Admin>(builder =>
            {
                builder.ToTable("Admins");
                builder.HasKey(o => o.Id);
                builder.Property(o => o.Id)
                    .ValueGeneratedNever();
                builder.Property(o => o.FirstName)
                    .HasMaxLength(32)
                    .IsRequired();
                builder.Property(o => o.MiddleName)
                    .HasMaxLength(32)
                    .IsRequired();
                builder.Property(o => o.LastName)
                    .HasMaxLength(32)
                    .IsRequired();
                builder.Property(o => o.Phone)
                    .HasMaxLength(32)
                    .IsRequired();
                builder.Property(o => o.Login)
                    .HasMaxLength(128)
                    .IsRequired();
                builder.Property(o => o.Password)
                    .HasMaxLength(128)
                    .IsRequired();
                builder.Property(o => o.Role)
                    .HasConversion<string>()
                    .IsRequired();
            });

            modelBuilder.Entity<NewsPost>(builder =>
            {
                builder.ToTable("NewsPosts");
                builder.HasKey(o => o.Id);
                builder.Property(o => o.Id)
                    .ValueGeneratedNever();
                builder.Property(o => o.Type)
                    .HasConversion<string>()
                    .IsRequired();
                builder.Property(o => o.Title)
                    .HasMaxLength(258)
                    .IsRequired();
                builder.Property(o => o.Text)
                    .IsRequired();
                builder.Property(o => o.CreateDate)
                    .IsRequired();
                builder.Property(o => o.IsArchived)
                    .HasDefaultValue(false)
                    .IsRequired();
            });
        }
    }
}
