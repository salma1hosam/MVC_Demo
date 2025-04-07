using Demo.BusinessLogic.DataTransferObjects.Department;
using Demo.BusinessLogic.Factories;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Repositories.Interfaces;


namespace Demo.BusinessLogic.Services.Classes
{
    public class DepartmentService : IDepartmentService
    {
		private readonly IUnitOfWork _unitOfWork;

		public DepartmentService(IUnitOfWork unitOfWork) //1.Injection
        {
			_unitOfWork = unitOfWork;
		}

        //GET All Departments
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();

            //Manual Mapping
            var depatmentsToReturn = departments.Select(D => new DepartmentDto
            {
                DeptId = D.Id,
                Code = D.Code,
                Description = D.Description,
                Name = D.Name,
                DateOfCreation = DateOnly.FromDateTime(D.CreatedOn)
            });

            return depatmentsToReturn;
        }

        //GET Department By Id
        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);

            #region Manual Mapping
            //if (department is null) return null;
            //else
            //{
            //	var departmentToReturn = new DepartmentDetailsDto()
            //	{
            //		Id = department.Id,
            //		Code = department.Code,
            //		Description = department.Description,
            //		Name = department.Name,
            //		CreatedBy = department.CreatedBy,
            //		CreatedOn = DateOnly.FromDateTime(department.CreatedOn),
            //		LastModifiedBy = department.LastModifiedBy,
            //		LastModifiedOn = DateOnly.FromDateTime(department.LastModifiedOn),
            //		IsDeleted = department.IsDeleted
            //	};
            //	return departmentToReturn;
            //} 
            #endregion

            ////Constructor Mapping
            //return department is null ? null : new DepartmentDetailsDto(department);

            //Extension Method Mapping
            return department is null ? null : department.ToDepartmentDetailsDto();
        }

        //Create New Department
        public int AddDepartment(CreatedDepartmentDto departmentDto)
        {
            var department = departmentDto.ToEntity();
            _unitOfWork.DepartmentRepository.Add(department);
            return _unitOfWork.SaveChanges();
        }

        //Update Department
        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
            _unitOfWork.DepartmentRepository.Update(departmentDto.ToEntity());
            return _unitOfWork.SaveChanges();
        }

        //Delete Department
        public bool DeleteDepartment(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            if (department is null) return false;
            else
            {
                _unitOfWork.DepartmentRepository.Remove(department);
                int result = _unitOfWork.SaveChanges();
                return result > 0 ? true : false;
            }
        }
    }
}
