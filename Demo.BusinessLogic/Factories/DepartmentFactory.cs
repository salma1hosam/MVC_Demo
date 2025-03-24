using Demo.BusinessLogic.DataTransferObjects;
using Demo.DataAccess.Models.DepartmentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Factories
{
    public static class DepartmentFactory
	{
		//Extension Method Mapping
		public static DepartmentDto ToDepartmentDto(this Department D)
		{
			return new DepartmentDto()
			{
				DeptId = D.Id,
				Code = D.Code,
				Description = D.Description,
				Name = D.Name,
				DateOfCreation = DateOnly.FromDateTime(D.CreatedOn)
			};
		}

		public static DepartmentDetailsDto ToDepartmentDetailsDto(this Department department)
		{
			return new DepartmentDetailsDto()
			{
				Id = department.Id,
				Code = department.Code,
				Description = department.Description,
				Name = department.Name,
				CreatedBy = department.CreatedBy,
				CreatedOn = DateOnly.FromDateTime(department.CreatedOn),
				LastModifiedBy = department.LastModifiedBy,
				LastModifiedOn = DateOnly.FromDateTime(department.LastModifiedOn),
				IsDeleted = department.IsDeleted
			};
		}

		public static Department ToEntity(this CreatedDepartmentDto departmentDto)
		{
			return new Department()
			{
				Name = departmentDto.Name,
				Code = departmentDto.Code,
				Description = departmentDto.Description,
				CreatedOn = departmentDto.CreatedOn.ToDateTime(new TimeOnly())
			};
		}

		public static Department ToEntity(this UpdatedDepartmentDto departmentDto)
		{
			return new Department()
			{
				Id = departmentDto.Id,
				Name = departmentDto.Name,
				Code = departmentDto.Code,
				Description = departmentDto.Description,
				CreatedOn = departmentDto.CreatedOn.ToDateTime(new TimeOnly())
			};
		}
	}
}
