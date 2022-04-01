using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoteIt.Domain.Entities;

namespace NoteIt.Persistence.EF.Configurations.Repositories
{
    public class ContactRepositoryConfiguration : IRepositoryConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(256);
            builder.Property(x => x.EmailAddress)
                .HasMaxLength(128);
            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(15);
            builder.Property(x => x.IsHidden)
                .IsRequired();
            builder.Property(x => x.StorageId)
                .IsRequired();
            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasPrecision(0);
            builder.Property(x => x.LastModified)
                .HasPrecision(0);

            builder.HasOne<Storage>(c => c.Storage)
                .WithMany(s => s.Contacts)
                .HasForeignKey(c => c.StorageId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
