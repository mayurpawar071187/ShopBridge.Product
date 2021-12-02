using System;
using System.Collections.Generic;

#nullable disable

namespace ShopBridge.Product.DataBase.DBEntity
{
    public partial class Itemmaster
    {
        public string Id { get; set; }
        public string Itemname { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Categoryid { get; set; }
        public bool Isactive { get; set; }
        public string Createdby { get; set; }
        public DateTime? Createdon { get; set; }
        public string Modifiedby { get; set; }
        public DateTime? Modifiedon { get; set; }

        public virtual Itemcategory Category { get; set; }
    }
}
