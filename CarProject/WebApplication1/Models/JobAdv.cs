using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class JobAdv
    {
        [Key]
        public int JAid { get; set; }
        [Required(ErrorMessage = "Please Enter Workshop Name")]
        public string WorkShopName { get; set; }
        [Required(ErrorMessage = "Please Enter Workshop Phone Number")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "invalied Phone Number (MinimumL 4 numbers & Maxmuime is 15 numbers)")]
        public string WorkShopPhone { get; set; }
        [Required(ErrorMessage = "Please Select city ")]
        public Nullable<int> CityId { get; set; }
        [Required(ErrorMessage = "Please Select Governrate ")]
        public Nullable<int> Gid { get; set; }

        public string Description { get; set; }

        public Nullable<System.DateTime> AdDate { get; set; }
        public string ApplicationUser_Id { get; set; }
        [Required(ErrorMessage = "Please select Your Job")]
        public Nullable<int> Jid { get; set; }

        public status state { get; set; }

        public virtual TechJob TechJob { get; set; }

        public virtual City City{ get; set; }
        public virtual Governrate Governrate { get; set; }

        [ForeignKey("ApplicationUser_Id")]
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}