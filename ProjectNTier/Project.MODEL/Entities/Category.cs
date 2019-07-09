using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MODEL.Entities
{
    public class Category : BaseEntity
    {
        public Category()
        {
            Products = new List<Product>();
        }
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string CategoryName { get; set; }
        public string Description { get; set; }

        //Relational Properties
        public virtual List<Product> Products { get; set; }
    }
}
