﻿using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Models.Shared.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace Demo.BusinessLogic.DataTransferObjects.Employee
{
    public class CreatedEmployeeDto
    {
        [Required(ErrorMessage = "Name Can't Be Null")] //Use it with the non-nullable field when you need to display an Error Message
        [MaxLength(50, ErrorMessage = "Max length should be 50 character")]
        [MinLength(5, ErrorMessage = "Min length should be 5 characters")]
        public string Name { get; set; } = null!;

        [Range(22, 35)]
        public int? Age { get; set; }

        [RegularExpression("^[1-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}$",
           ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string? Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActived { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Display(Name = "Phone Number")]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Hiring Date")]
        public DateOnly HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }

        public int? DepartmentId { get; set; }

        public IFormFile? Image { get; set; }
    }
}
