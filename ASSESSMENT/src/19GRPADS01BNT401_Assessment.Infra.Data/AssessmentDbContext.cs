using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using _19GRPADS01BNT401_Assessment.Domain.Entities;
using _19GRPADS01BNT401_Assessment.Infra.Data.Configurations;

namespace _19GRPADS01BNT401_Assessment.Infra.Data
{
    public class AssessmentDbContext : DbContext
    {
        public AssessmentDbContext(DbContextOptions<AssessmentDbContext> options) : base(options) { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        public DbSet<BookAuthor> BookAuthors { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>()
                .HasKey(ba => new { ba.BookId, ba.AuthorId });

            modelBuilder.Entity<BookAuthor>()
                .HasOne(p => p.Book)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(p => p.BookId);

            modelBuilder.Entity<BookAuthor>()
                .HasOne(p => p.Author)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(p => p.AuthorId);


            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthorConfig).Assembly);


            //seeding
            Seeding(modelBuilder);
        }

        private void Seeding(ModelBuilder modelBuilder)
        {
            var hemingway = new Author
            {
                Id = Guid.Parse("1cc08f85-6e83-464a-ac04-3d942dfbc1d5"),
                Name = "Ernest",
                LastName = "Hemingway",
                BirthDate = new DateTime(1899, 7, 21),
                Email = "hemingway@foundations.org"
            };
            modelBuilder.Entity<Author>().HasData(hemingway);

            var oldManAndTheSea = new Book
            {
                Id = Guid.Parse("4499e7c1-edd0-4c3a-90be-c57c66999d99"),
                Title = "Old man and the sea",
                Year = 1952,
                Isbn = "9788484377092",
            };
            modelBuilder.Entity<Book>().HasData(oldManAndTheSea);

            modelBuilder.Entity<BookAuthor>().HasData(new BookAuthor { AuthorId = hemingway.Id, BookId = oldManAndTheSea.Id });

        }
    }
}