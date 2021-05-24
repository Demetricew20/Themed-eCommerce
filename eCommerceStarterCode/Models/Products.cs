using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceStarterCode.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ProductPrice { get; set; }
        public int QuantityOnHand { get; set; }

        [Column(TypeName = "decimal(18,1)")]
        public decimal ProductAverageRating { get; set; }
        public byte[] ProductImage { get; set; }
        
        //Add a foreign key to the category file

        [ForeignKey("Category")]

        public int CategoryId { get; set;}

        public Category Category { get; set; }
    }
}
