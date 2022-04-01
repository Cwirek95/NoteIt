using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoteIt.Domain.Entities;

namespace NoteIt.Persistence.EF.Configurations.Repositories
{
    public class EventRepositoryConfiguration : IRepositoryConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(256);
            builder.Property(x => x.Location)
                .HasMaxLength(256);
            builder.Property(x => x.Description)
                .HasMaxLength(4000);
            builder.Property(x => x.StartDate)
                .IsRequired()
                .HasPrecision(0);
            builder.Property(x => x.EndDate)
                .IsRequired()
                .HasPrecision(0);
            builder.Property(x => x.ReminderDate)
                .HasPrecision(0);
            builder.Property(x => x.IsHidden)
                .IsRequired();
            builder.Property(x => x.StorageId)
                .IsRequired();
            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasPrecision(0);
            builder.Property(x => x.LastModified)
                .HasPrecision(0);

            builder.HasOne<Storage>(e => e.Storage)
                .WithMany(s => s.Events)
                .HasForeignKey(e => e.StorageId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
