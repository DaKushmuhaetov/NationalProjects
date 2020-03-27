using Chuvashia.NationalProjects.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chuvashia.NationalProjects.Context
{
    public sealed class NationalProjectsDbContext : DbContext
    {
        public NationalProjectsDbContext(DbContextOptions options) : base(options) { }

        internal DbSet<Counter> Counters { get; set; }
        internal DbSet<Admin> Admins { get; set; }

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
        }
    }
}
