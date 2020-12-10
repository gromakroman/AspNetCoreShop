using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreShop.Model
{
    public class BasketViewModel
    {
        [Key]
        public int BasketId { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Цена")]
        public float Price { get; set; }

        [Display(Name = "Количество")]
        public int Count { get; set; }

        [Display(Name = "Сумма")]
        public float Amount { get; set; }

        public string Image { get; set; }

    }
}
