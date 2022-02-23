//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopX.Models
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Account
    {         

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Account()
        {
            this.Carts = new HashSet<Cart>();
        }
    
        public System.Guid CustomerID { get; set; }


        [Required (ErrorMessage ="Username is required")]
        [StringLength(15, ErrorMessage = "The Username can't be more than 15 characters", MinimumLength = 3)]
        public string UserName { get; set; }


        [Required (ErrorMessage ="EmailID is required")]
        [DataType(DataType.EmailAddress)]
        [EmailValidation(ErrorMessage = "Account already exist with this email id!")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Mobile Number is required")]
        [RegularExpression(@"\d{10}", ErrorMessage = "Please enter 10 digit Mobile No.")]
        public string MobileNumber { get; set; }


        public string Address { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [PasswordValidation]
        public string Password { get; set; }


        [Compare("Password",ErrorMessage ="Please conform your password")]
        [DataType(DataType.Password)]
        public string ConformPassword { get; set; }

    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart> Carts { get; set; }
    }
}
