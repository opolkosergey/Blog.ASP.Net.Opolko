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
        [Required(ErrorMessage = "Укажите заголовок статьи")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Заполните содержимое")]
        public string Content { get; set; }
        [Required(ErrorMessage = "Укажите блог")]
        public string Blog { get; set; }
    }
}