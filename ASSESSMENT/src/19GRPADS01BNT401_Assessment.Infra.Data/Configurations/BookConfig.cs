using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using _19GRPADS01BNT401_Assessment.Domain.Entities;

namespace _19GRPADS01BNT401_Assessment.Infra.Data.Configurations
{
    public class BookConfig: IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(p => p.Isbn)
                .HasMaxLength(13)
                .IsRequired();           

        }
    }
}