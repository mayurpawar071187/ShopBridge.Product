using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ShopBridge.Product.Model.Models
{
    public class Itembase
    {
        [Required(ErrorMessage = "Please Add item name")]
        public string Itemname { get; set; }

        [Required(ErrorMessage = "Please Add item code")]
        public string Code { get; set; }
        public string Description { get; set; }
    }
    public class Item: Itembase
    {
        public string Id { get; set; }
        public string ItemImagePath { get; set; }

        [Required(ErrorMessage = "Please Add item Category")]
        public string Categoryid { get; set; }
        public bool Isactive { get; set; }
    }

    public class ItemSearch: Itembase
    {
        public int page { get; set; }
        public int size { get; set; }
    }
}
