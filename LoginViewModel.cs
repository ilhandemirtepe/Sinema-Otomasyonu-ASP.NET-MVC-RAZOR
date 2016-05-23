using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcSinemaOdev.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "kullanıcı adı boş geçilemez..")]
        [Display(Name = "Kullanıcı Adı")]
        [MaxLength(50)]
        public string UserNameViewModel { get; set; }
        [Required(ErrorMessage = "şifre boş geçilemez.")]
        [Display(Name = "Şifre")]
        [MaxLength(50)]
        public string PasswordViewModel { get; set; } 
    }
}