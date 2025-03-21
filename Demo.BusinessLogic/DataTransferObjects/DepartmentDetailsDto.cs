using Demo.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.DataTransferObjects
{
	public class DepartmentDetailsDto
	{
		#region Constructor - Base Mapping
		//public DepartmentDetailsDto(Department department)
		//{
		//	Id = department.Id;
		//	Code = department.Code;
		//	Description = department.Description;
		//	Name = department.Name;
		//	CreatedBy = department.CreatedBy;
		//	CreatedOn = DateOnly.FromDateTime(department.CreatedOn);
		//	LastModifiedBy = department.LastModifiedBy;
		//	LastModifiedOn = DateOnly.FromDateTime(department.LastModifiedOn);
		//	IsDeleted = department.IsDeleted;
		//} 
		#endregion
		
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Code { get; set; } = string.Empty;
		public string? Description { get; set; }
		public int CreatedBy { get; set; } 
		public DateOnly CreatedOn { get; set; }
		public int LastModifiedBy { get; set; }
		public DateOnly LastModifiedOn { get; set; }
		public bool IsDeleted { get; set; } 
	}
}
