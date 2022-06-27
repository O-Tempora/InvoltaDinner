using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class SignUpModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email {get;set;}

        [Required]
        [Display(Name = "UserName")]
        public string UserName {get;set;}

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password {get;set;}

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Repeat Password")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string RepeatPassword {get;set;}
    }
}