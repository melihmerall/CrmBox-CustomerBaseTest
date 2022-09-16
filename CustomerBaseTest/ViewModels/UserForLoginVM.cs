﻿using System.ComponentModel.DataAnnotations;

namespace CustomerBaseTest.ViewModels
{
    public class UserForLoginVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Lütfen Kullanıcı adını giriniz")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Lütfen Şifrenizi giriniz")]

        public string Password { get; set; }
    }
}
