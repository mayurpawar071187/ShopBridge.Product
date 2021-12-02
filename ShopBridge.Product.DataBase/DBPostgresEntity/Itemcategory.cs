using System;
using System.Collections.Generic;

#nullable disable

namespace ShopBridge.Product.DataBase.DBEntity
{
    public partial class Itemcategory
    {
        public Itemcategory()
        {
            Itemmasters = new HashSet<Itemmaster>();
        }

        public string Id { get; set; }
        public string Categoryname { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Parentcategoryid { get; set; }
        public bool Isactive { get; set; }
        public string Createdby { get; set; }
        public DateTime? Createdon { get; set; }
        public string Modifiedby { get; set; }
        public DateTime? Modifiedon { get; set; }

        public virtual ICollection<Itemmaster> Itemmasters { get; set; }
    }
}
