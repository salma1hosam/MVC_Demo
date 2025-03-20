
namespace Demo.DataAccess.Data.Configurations
{
	internal class DepartmentConfigurations : IEntityTypeConfiguration<Department>
	{
		public void Configure(EntityTypeBuilder<Department> builder)
		{
			builder.Property(D => D.Id).UseIdentityColumn(10, 10);
			builder.Property(D => D.Name).HasColumnType("varchar(20)");
			builder.Property(D => D.Code).HasColumnType("varchar(20)");

			//Will be used one time when inserting a department will GETDATE() in the CreatedOn (Can reset it)
			builder.Property(D => D.CreatedOn).HasDefaultValueSql("GETDATE()");

			//Will automatically recalculate the date with each modification on this record (Can Not reset it)
			builder.Property(D => D.LastModifiedOn).HasComputedColumnSql("GETDATE()");
		}
	}
}
