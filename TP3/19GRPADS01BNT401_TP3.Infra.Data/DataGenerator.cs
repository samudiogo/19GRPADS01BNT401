using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using _19GRPADS01BNT401_TP3.Domain.Entities;

namespace _19GRPADS01BNT401_TP3.Infra.Data
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var ctx = new TP3DbContext(serviceProvider.GetRequiredService<DbContextOptions<TP3DbContext>>()))
            {
                if (ctx.Friends.Any())
                    return;

                ctx.Friends.AddRange(
                    new Friend
                    {
                        Id = Guid.NewGuid(),
                        Name = "Samuel",
                        LastName = "Diogo",
                        Email = "samudiogo@gmai.com",
                        BirthDate = new DateTime(1990, 2, 12)
                    },
                    new Friend
                    {
                        Id = Guid.NewGuid(),
                        Name = "Dione",
                        LastName = "Lucia",
                        Email = "dione@gmail.com",
                        BirthDate = new DateTime(1965, 12, 18)
                    },
                    new Friend
                    {
                        Id = Guid.NewGuid(),
                        Name = "Gustavo",
                        LastName = "Fernandes",
                        Email = "gustavo@gmail.com",
                        BirthDate = new DateTime(1988, 2, 19)
                    }
                );
                ctx.SaveChanges();
            }
        }
    }
}