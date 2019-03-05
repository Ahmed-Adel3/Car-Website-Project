using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{

    public enum status { accepted, waiting, rejected, sold }
    public class CarAdv
    {
        [Key]
        public int Aid { get; set; }
        public string Color { get; set; }
        [Required(ErrorMessage = "this Field is Required")]
        public Nullable<int> Year { get; set; }
        public Nullable<int> Cc { get; set; }
        public Nullable<decimal> Km { get; set; }
        public Nullable<bool> AirDevice { get; set; }
        public Nullable<bool> SunRoof { get; set; }
        [Required(ErrorMessage = "Please Select Transimition Type")]
        public string Transimition { get; set; }
        [Required(ErrorMessage = "please Enter the Price")]
        public Nullable<decimal> Price { get; set; }
        public Nullable<System.DateTime> AdDate { get; set; }
        [Required(ErrorMessage = "Please Select the Governorate")]
        public Nullable<int> Gid { get; set; }
        [Required(ErrorMessage = "Please Select the City")]
        public Nullable<int> CityId { get; set; }
        public string Description { get; set; }
        public Nullable<bool> AirBag { get; set; }
        public Nullable<bool> Power { get; set; }
        public Nullable<bool> CenterLock { get; set; }
        public Nullable<bool> Alarm { get; set; }
        public Nullable<bool> Radio { get; set; }
        public Nullable<bool> ABS { get; set; }
        public Nullable<bool> ElectricWindow { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public Nullable<int> Mid { get; set; }
        public Nullable<int> ModelId { get; set; }
        public string ApplicationUser_Id { get; set; }
        public status state { get; set; }

        public virtual City City { get; set; }
        public virtual Governrate Governrate { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public virtual CarModel CarModel { get; set; }
        
        public virtual ICollection<CarPhoto> CarPhoto { get; set; }

        [ForeignKey("ApplicationUser_Id")]
        public virtual ApplicationUser ApplicationUser { get; set; }


    }
}