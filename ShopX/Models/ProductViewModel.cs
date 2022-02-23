using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;

namespace ShopX.Models
{
    public class ProductViewModel
    {
        public System.Guid ProductID { get; set; }


        [Required(ErrorMessage = "Category is required")]
        public string CategoryName { get; set; }


        [Required(ErrorMessage = "Product name is required")]
        public string ProductName { get; set; }


        [Required(ErrorMessage = "Price is required")]
        [DataType(DataType.Currency)]
        public int Price { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public HttpPostedFileBase ImagePath { get; set; }
        [Required(ErrorMessage = "Image is required")]
        public HttpPostedFileBase ImagePath_1 { get; set; }
        [Required(ErrorMessage = "Image is required")]
        public HttpPostedFileBase ImagePath_2 { get; set; }
        public string Specification { get; set; }
    }
}
