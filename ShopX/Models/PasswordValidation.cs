using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.AspNet.Identity;

namespace ShopX.Models
{
    public class PasswordValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            
            Account customer = (Account)validationContext.ObjectInstance;
            String Password = customer.Password;
            if (String.IsNullOrEmpty(Password) || Password.Length < 4 || Password.Length>20)
            {
                return new ValidationResult("The Password must be minimun of 6 characters to maximum of 20 characters long.");
            }
                   

            int counter = 0;
            List<string> patterns = new List<string>
            {
                @"[a-z]",
                @"[A-Z]",
                @"[0-9]",
                @"[!@#$%^&*\(\)_\+\-\={}<>,\.\|""'~`:;\\?\/\[\] ]"
            };              
            foreach (string character in patterns)
            {
                if (Regex.IsMatch(Password, character))
                {
                    counter++;
                }
            }
            if (counter < 4)
            {
                return new ValidationResult("Passwords must have at least one non letter or digit character. Passwords must have at least one digit ('0'-'9'). Passwords must have at least one uppercase ('A'-'Z').");
            }

            return ValidationResult.Success;
        }
    }
}



