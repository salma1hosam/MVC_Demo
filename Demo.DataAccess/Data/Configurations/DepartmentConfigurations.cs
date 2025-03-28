
using Demo.DataAccess.Models.DepartmentModel;

namespace Demo.DataAccess.Data.Configurations
{
    internal class DepartmentConfigurations : BaseEntityConfigurations<Department> , IEntityTypeConfiguration<Department>
	{
		public new void Configure(EntityTypeBuilder<Department> builder)
		{
			builder.Property(D => D.Id).UseIdentityColumn(10, 10);
			builder.Property(D => D.Name).HasColumnType("varchar(20)");
			builder.Property(D => D.Code).HasColumnType("varchar(20)");

            base.Configure(builder); //Without it You're hiding the base method and creating a new version(can not inherit the base Configure Method in the BaseEntityConfigurations )
        }
	}
}
