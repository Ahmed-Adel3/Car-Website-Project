using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class AccessoriesAdv
    {
        [Key]
        public int AccId { get; set; }
        [Required(ErrorMessage = "Please Select Accessory Type")]
        public int AccTypeId { get; set; }
        [Required(ErrorMessage = "Please Select Status")]
        public string Status { get; set; }
        public string Aphoto { get; set; }
        [Required(ErrorMessage = "Price is Required")]
        public Nullable<decimal> Price { get; set; }
        public Nullable<System.DateTime> AdDate { get; set; }
        public Nullable<int> Mid { get; set; }
        public string ApplicationUser_Id { get; set; }

        public string TradeMark { get; set; }
        public string  Description { get; set; }
        [Required(ErrorMessage = "Please Select Governorate")]
        public Nullable<int> Gid { get; set; }
        [Required(ErrorMessage = "Please Select City")]
        public Nullable<int> CityId { get; set; }
        public status state { get; set; }


        public virtual Accessory Accessory  { get; set; }
       
        public virtual Manufacturer Manufacturer { get; set; }
        public virtual City City { get; set; }
        public virtual Governrate Governrate { get; set; }

        [ForeignKey("ApplicationUser_Id")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}