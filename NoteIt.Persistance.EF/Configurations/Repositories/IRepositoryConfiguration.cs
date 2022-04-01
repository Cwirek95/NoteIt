using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NoteIt.Persistence.EF.Configurations.Repositories
{
    public interface IRepositoryConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class
    {
        new void Configure(EntityTypeBuilder<TEntity> builder);
    }
}
