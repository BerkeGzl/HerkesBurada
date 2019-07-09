using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MODEL.Entities
{
    public class Product : BaseEntity
    {
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string ProductName { get; set; }
        [Range(5, 500, ErrorMessage = "Stok ancak 5 ile 500 aralığında olabilir.")]
        public string Description { get; set; }
        public short? UnitsInStock { get; set; }
        public string ImagePath { get; set; }
        [Range(5,15000, ErrorMessage ="Fiyat ancak 5 ile 15000 aralığında olabilir.")]
        public decimal? UnitPrice { get; set; }
        public int? CategoryID { get; set; }

        //Relational Properties
        public virtual Category Category { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}
