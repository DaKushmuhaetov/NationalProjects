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

        public DbSet<Counter> Counters { get; set; }

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
        }
    }
}
