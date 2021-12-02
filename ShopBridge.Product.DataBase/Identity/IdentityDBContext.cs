using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopBridge.Product.DataBase.DBEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBridge.Product.DataBase.Identity
{
    public class ShopBridgeIdentityDBContext : IdentityDbContext<ShopBridgeUser>
    {
        public ShopBridgeIdentityDBContext(DbContextOptions<ShopBridgeIdentityDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
