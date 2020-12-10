using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreShop.Model
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
               
        public string Name { get; set; }

        public int? ParentCategoryId { get; set; }

        public virtual Category ParentCategory { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}
