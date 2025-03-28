using Demo.BusinessLogic.DataTransferObjects.Department;
using Demo.BusinessLogic.Factories;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Repositories.Interfaces;


namespace Demo.BusinessLogic.Services.Classes
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository) //1.Injection
        {
            _departmentRepository = departmentRepository;
        }

        //GET All Departments
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _departmentRepository.GetAll();

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
            var department = _departmentRepository.GetById(id);

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
            return _departmentRepository.Add(department);
        }

        //Update Department
        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
            return _departmentRepository.Update(departmentDto.ToEntity());
        }

        //Delete Department
        public bool DeleteDepartment(int id)
        {
            var department = _departmentRepository.GetById(id);
            if (department is null) return false;
            else
            {
                int result = _departmentRepository.Remove(department);
                return result > 0 ? true : false;
            }
        }
    }
}
