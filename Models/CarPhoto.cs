using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class CarPhoto
    {
        [Key]       
        public int PhotoId { get; set; }      
        public string CPhoto { get; set; }
        public int Aid { get; set; }

        [ForeignKey("Aid")]
        public virtual CarAdv CarAdv { get; set; }



    }
}