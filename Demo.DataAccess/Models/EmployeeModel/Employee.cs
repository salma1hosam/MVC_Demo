﻿using Demo.DataAccess.Models.Shared;
using Demo.DataAccess.Models.Shared.Enums;


namespace Demo.DataAccess.Models.EmployeeModel
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActived { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
    }
}
