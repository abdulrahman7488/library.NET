using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Bookshop1.Models
{
    public class ContactModel
    {
        [Required(ErrorMessage = "يرجى إدخال اسمك.")]
        [Display(Name = "الاسم")]
        public string Name { get; set; }

        [Required(ErrorMessage = "يرجى إدخال بريدك الإلكتروني.")]
        [EmailAddress(ErrorMessage = "يرجى إدخال بريد إلكتروني صحيح.")]
        [Display(Name = "البريد الإلكتروني")]
        public string Email { get; set; }

        [Required(ErrorMessage = "يرجى إدخال موضوع الرسالة.")]
        [Display(Name = "موضوع الرسالة")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "يرجى كتابة رسالتك.")]
        [Display(Name = "محتوى الرسالة")]
        public string Message { get; set; }
    }
}
