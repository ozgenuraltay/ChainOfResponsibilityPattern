using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChainOfResponsibilityPattern.DAL.Entities
{
    public class Product
    {
        public int ProductID { get; set; }

        public string Name { get; set; }

        [Column(TypeName ="decimal(18,2)")]
        public decimal Price { get; set; }

        public int Stock { get; set; }
    }
}
