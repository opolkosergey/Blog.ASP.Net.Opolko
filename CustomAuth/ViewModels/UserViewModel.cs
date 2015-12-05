using System;
using System.ComponentModel.DataAnnotations;

namespace CustomAuth.ViewModels
{

    public enum Role
    {
        Administrator = 1,
        Moderator,
        User,
        Guest
    }
    public class UserViewModel
    {
        public int Id { get; set; }

        [Display(Name = "User's e-mail")]
        public string Email { get; set; }

        [Display(Name = "Date of user's registration")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "User's role in the system")]
        public Role Role { get; set; }
    }
}