using System.Data.Entity.ModelConfiguration;

namespace Aaron.Core
{
    public abstract class BaseEntityTypeConfiguration<T, TKey> : EntityTypeConfiguration<T> where T : BaseEntity<TKey>
                                                                                            where TKey : struct
    {
        public BaseEntityTypeConfiguration(string tableName)
        {
            this.ToTable(tableName);
            
            this.HasKey(x => x.Id);
            this.Property(x => x.CreationDate).IsOptional();
            this.Property(x => x.ModifiedDate).IsOptional();
        }
    }
}
