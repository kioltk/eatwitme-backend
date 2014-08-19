using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP.NET_MVC5.Models
{
    public class MeetingViewModel
    {

        [Required]
        [Display(Name = "Longtitude")]
        public string Longtitude { get; set; }
        [Required]
        [Display(Name = "Latitude")]
        public string Latitude { get; set; }


        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Time")]
        public int Time { get; set; }

    }
}