using System;

namespace ShopBridge.Product.Model.Models
{
    public class ItemCategory
    {
        public string Id { get; set; }
        public string Categoryname { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Parentcategoryid { get; set; }
        public bool Isactive { get; set; }
    }
}
