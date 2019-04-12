using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace TP1.Data
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var ctx = new TP1DbContext(serviceProvider.GetRequiredService<DbContextOptions<TP1DbContext>>()))
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
                        BirthDate = new DateTime(1990,2,12)
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
