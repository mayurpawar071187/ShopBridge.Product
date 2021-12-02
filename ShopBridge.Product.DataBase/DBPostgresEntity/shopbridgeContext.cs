using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ShopBridge.Product.DataBase.DBEntity
{
    public partial class shopbridgeContext : DbContext
    {
        public shopbridgeContext()
        {
        }

        public shopbridgeContext(DbContextOptions<shopbridgeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Itemcategory> Itemcategories { get; set; }
        public virtual DbSet<Itemmaster> Itemmasters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_IN.UTF-8");

            modelBuilder.Entity<Itemcategory>(entity =>
            {
                entity.ToTable("itemcategory", "inventory");

                entity.HasIndex(e => e.Categoryname, "itemcategory_categoryname_idx");

                entity.HasIndex(e => e.Code, "itemcategory_code_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("character varying")
                    .HasColumnName("id");

                entity.Property(e => e.Categoryname)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("categoryname");

                entity.Property(e => e.Code)
                    .HasColumnType("character varying")
                    .HasColumnName("code");

                entity.Property(e => e.Createdby)
                    .HasColumnType("character varying")
                    .HasColumnName("createdby");

                entity.Property(e => e.Createdon)
                    .HasPrecision(0)
                    .HasColumnName("createdon");

                entity.Property(e => e.Description)
                    .HasColumnType("character varying")
                    .HasColumnName("description");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.Modifiedby)
                    .HasColumnType("character varying")
                    .HasColumnName("modifiedby");

                entity.Property(e => e.Modifiedon)
                    .HasPrecision(0)
                    .HasColumnName("modifiedon");

                entity.Property(e => e.Parentcategoryid)
                    .HasColumnType("character varying")
                    .HasColumnName("parentcategoryid");
            });

            modelBuilder.Entity<Itemmaster>(entity =>
            {
                entity.ToTable("itemmaster", "inventory");

                entity.HasIndex(e => e.Code, "itemmaster_code_idx");

                entity.HasIndex(e => e.Itemname, "itemmaster_itemname_idx");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasColumnName("id");

                entity.Property(e => e.Categoryid)
                    .HasColumnType("character varying")
                    .HasColumnName("categoryid");

                entity.Property(e => e.Code)
                    .HasColumnType("character varying")
                    .HasColumnName("code");

                entity.Property(e => e.Createdby)
                    .HasColumnType("character varying")
                    .HasColumnName("createdby");

                entity.Property(e => e.Createdon)
                    .HasPrecision(0)
                    .HasColumnName("createdon");

                entity.Property(e => e.Description)
                    .HasColumnType("character varying")
                    .HasColumnName("description");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.Itemname)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("itemname");

                entity.Property(e => e.Modifiedby)
                    .HasColumnType("character varying")
                    .HasColumnName("modifiedby");

                entity.Property(e => e.Modifiedon)
                    .HasPrecision(0)
                    .HasColumnName("modifiedon");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Itemmasters)
                    .HasForeignKey(d => d.Categoryid)
                    .HasConstraintName("itemmaster_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseNpgsql("Host=asaassa;Database=shopbridge;Username=asassa;Password=asasas");
//            }
//        }   
