using System;
using _19GRPADS01BNT401_TP3.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace _19GRPADS01BNT401_TP3.Infra.Data
{
    public class TP3DbContext : DbContext
    {

        public TP3DbContext(DbContextOptions<TP3DbContext> options) : base(options) { }

        public DbSet<Friend> Friends { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Friend>().Property(f => f.Id).ValueGeneratedOnAdd().IsRequired();
            modelBuilder.Entity<Friend>().ToTable("TP3_Friend");
        }
    }
}
