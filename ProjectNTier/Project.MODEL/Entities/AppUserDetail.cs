using Project.MODEL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MODEL.Entities
{
    public class AppUserDetail : BaseEntity
    {
        [Required(ErrorMessage = "{0} alanının girilmesi zorunludur."), MaxLength(20, ErrorMessage = "{0} alınan maksimum {1} karakter girilebilir."), Display(Name = "İsim")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "{0} alanının girilmesi zorunludur."), MaxLength(20, ErrorMessage = "{0} alınan maksimum {1} karakter girilebilir."), Display(Name = "Soyisim")]
        public string LastName { get; set; }
        public string Address { get; set; }
        public Gender? Gender { get; set; }
        public string UserIP { get; set; }
        //Relational Properties
        public virtual AppUser AppUser { get; set; }
    }
}
