using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace TP1.Data
{
    public class TP1DbContext:DbContext
    {
        public TP1DbContext(DbContextOptions<TP1DbContext> options):base(options)
        {

        }

        public DbSet<Friend> Friends { get; set; }


    }
}
