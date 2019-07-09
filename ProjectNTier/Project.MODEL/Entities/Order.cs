using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MODEL.Entities
{
    public class Order : BaseEntity
    {
        [Required(ErrorMessage ="{0} alanı boş geçilemez.")]
        public string ShippedAddress { get; set; }
        [Required(ErrorMessage ="{0} alanı boş geçilemez.")]
        public DateTime? RequiredDate { get; set; }
        public int? AppUserID { get; set; }

        //Relational Properties
        public virtual List<OrderDetail> OrderDetails { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
