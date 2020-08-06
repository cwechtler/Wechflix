using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Wechflix.Models
{
	public class Min18YearsforMembership: ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var customer = (Customer)validationContext.ObjectInstance;

			if (customer.MembershipTypeId == MembershipType.Unknown ||
				customer.MembershipTypeId == MembershipType.PayAsYouGo) 
			{
				return ValidationResult.Success;
			}

			if (customer.Birthdate == null) {
				return new ValidationResult("Birthdate is Required");
			}

			return (GetAge(customer) >= 18) 
				? ValidationResult.Success 
				: new ValidationResult("Customer is required to be atleast 18 years of age for this Membership Type");
		}

		private int GetAge(Customer customer) {
			var age = DateTime.Today.Year - customer.Birthdate.Value.Year;

			if (DateTime.Today.Month < customer.Birthdate.Value.Month ||
				DateTime.Today.Month == customer.Birthdate.Value.Month &&
				DateTime.Today.Day < customer.Birthdate.Value.Day) {
				age--;
			}

			return age;
		}
	}
}