using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoteIt.Domain.Entities;

namespace NoteIt.Persistence.EF.Configurations.Repositories
{
    public class NoteRepositoryConfiguration : IRepositoryConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(256);
            builder.Property(x => x.Content)
                .HasMaxLength(8000);
            builder.Property(x => x.IsImportant)
                .IsRequired();
            builder.Property(x => x.IsHidden)
                .IsRequired();
            builder.Property(x => x.StorageId)
                .IsRequired();
            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasPrecision(0);
            builder.Property(x => x.LastModified)
                .HasPrecision(0);

            builder.HasOne<Storage>(n => n.Storage)
                .WithMany(s => s.Notes)
                .HasForeignKey(n => n.StorageId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
