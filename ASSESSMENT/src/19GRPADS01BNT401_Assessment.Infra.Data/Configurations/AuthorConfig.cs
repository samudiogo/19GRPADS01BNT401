using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using _19GRPADS01BNT401_Assessment.Domain.Entities;

namespace _19GRPADS01BNT401_Assessment.Infra.Data.Configurations
{
    public class AuthorConfig:IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.LastName).IsRequired();

            builder.Property(p => p.BirthDate).IsRequired();
            builder.Property(p => p.Email).IsRequired();
        }
    }
}