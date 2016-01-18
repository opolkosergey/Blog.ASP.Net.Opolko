using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.ComponentModel.DataAnnotations;

namespace CustomAuth.ViewModels
{
    public class UserBlogModel
    {
       
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Enter title of new blog")]
        public string Title { get; set; }
    }
}