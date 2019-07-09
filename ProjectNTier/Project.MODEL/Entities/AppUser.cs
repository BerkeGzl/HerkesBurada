using Project.MODEL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MODEL.Entities
{
    public class AppUser : BaseEntity
    {
        [Required(ErrorMessage ="{0} alanının girilmesi zorunludur."), MaxLength(20,ErrorMessage ="{0} alınan maksimum {1} karakter girilebilir."), MinLength(5, ErrorMessage ="Minimum {1} karakter girebilirsiniz."), Display(Name ="Kullanıcı İsmi")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "{0} alanının girilmesi zorunludur."), MaxLength(10, ErrorMessage = "{0} alınan maksimum {1} karakter girilebilir."), Display(Name = "Şifre")]
        public string Password { get; set; }
        [Required(ErrorMessage ="{0} alanının girilmesi zorunludur."), Compare("Password", ErrorMessage ="Şifreleriniz Uyuşmuyor."), Display(Name ="Şifre Tekrar")]
        public string ConfirmPassword { get; set; }
        public Role UserRole { get; set; }
        public Guid? ActivationCode { get; set; }
        public bool IsActive { get; set; }
        [Required(ErrorMessage ="Email alanını boş geçemezsiniz."), EmailAddress(ErrorMessage ="Lütfen Email formatında bir giriş yapınız.")]
        public string Email { get; set; }
        public AppUser()
        {
            UserRole = Role.Member;
            ActivationCode = Guid.NewGuid();
        }

        //Relational Properties
        public virtual AppUserDetail AppUserDetail { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
