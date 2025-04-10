﻿using System.ComponentModel.DataAnnotations;

namespace Demo.Presentation.ViewModels.AccountViewModels
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage = "First Name Can Not Be Null")]
		[MaxLength(50)]
		public string FirstName { get; set; }

		[Required]
		[MaxLength(50)]
		public string LastName { get; set; }

		[Required]
		[MaxLength(50)]
		public string UserName { get; set; } // Unique 

		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[DataType(DataType.Password)]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Compare(nameof(Password))]
		public string ConfirmPassword { get; set; }

		public bool IsAgree { get; set; }
	}
}
