﻿using System.ComponentModel.DataAnnotations;

namespace Eticaret.WebUI.Models
{
    public class UserEditViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Adı")]
        public string Name { get; set; }
        [Display(Name = "Soyadı")]
        public string SurName { get; set; }

        public string Email { get; set; }
        [Display(Name = "Telefonu")]
        public string? Phone { get; set; }
        [Display(Name = "Şifre")]
        public string Password { get; set; }
    }
}
