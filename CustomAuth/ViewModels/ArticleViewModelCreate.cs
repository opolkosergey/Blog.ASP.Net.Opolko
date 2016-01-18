using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomAuth.ViewModels
{
    public class ArticleViewModelCreate
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Fill title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Fill content")]
        public string Content { get; set; }
        [Required(ErrorMessage = "Select blog")]
        public string Blog { get; set; }
        public string Tags { get; set; }
    }
}