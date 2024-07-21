using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Work_Images.Models
{
    public class data
    {
        public int id { get; set; }

        [Display(Name ="User Name")]
        public string u_name { get; set; }

        [Display(Name ="Birth Date")]
        public string b_date { get; set; }
        public string image_path { get; set; }

        [Display(Name ="Profile Photo")]
        public HttpPostedFileBase imgfile { get; set; }

        [Display(Name ="Email")]
        public string email_add { get; set; }

    }
}