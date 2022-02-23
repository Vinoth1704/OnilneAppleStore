using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopX.Models
{
    public class EmailValidation : ValidationAttribute
    {
        private ShopEntities db = new ShopEntities();
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Account customer = (Account)validationContext.ObjectInstance;
            String CurrentEmailId = customer.Email;

            foreach (Account user in db.Accounts)
            {
                if (user.Email == CurrentEmailId)
                {
                    return new ValidationResult("Account already exist with this email id!");
                }
            }           
            return ValidationResult.Success;
        }
    }
}
