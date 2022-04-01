using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoteIt.Domain.Entities;

namespace NoteIt.Persistence.EF.Configurations.Repositories
{
    public class StorageRepositoryConfiguration : IRepositoryConfiguration<Storage>
    {
        public void Configure(EntityTypeBuilder<Storage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(64);
            builder.Property(x => x.CreatedAt).IsRequired().HasPrecision(0);
            builder.Property(x => x.LastModified).HasPrecision(0);
        }
    }
}
