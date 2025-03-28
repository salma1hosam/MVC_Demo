using Demo.DataAccess.Models.Shared;

namespace Demo.DataAccess.Data.Configurations
{
    public class BaseEntityConfigurations<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            //Will be used one time when inserting a department will GETDATE() in the CreatedOn (Can reset it)
            builder.Property(D => D.CreatedOn).HasDefaultValueSql("GETDATE()");

            //Will automatically recalculate the date with each modification on this record (Can Not reset it)
            builder.Property(D => D.LastModifiedOn).HasComputedColumnSql("GETDATE()");
        }
    }
}
