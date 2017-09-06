using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Branch
    {
        [Key]
        public int Bid { get; set; }
        public string Name { get; set; }
        public Nullable<int> Gid { get; set; }
        public Nullable<int> CityId { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string WorkingHours{ get; set; }

        public string Vacation { get; set; }
        public Nullable<int> Mid { get; set; }

        public virtual City City { get; set; }
        public virtual Governrate Governrate { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
    }
}