﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SkripsiIanKeefe.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LAISONEntities : DbContext
    {
        public LAISONEntities()
            : base("name=LAISONEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<CART> CARTs { get; set; }
        public DbSet<CHECKOUT> CHECKOUTs { get; set; }
        public DbSet<CLIENT> CLIENTs { get; set; }
        public DbSet<COA> COAs { get; set; }
        public DbSet<JURNAL> JURNALs { get; set; }
        public DbSet<PEMBAYARAN> PEMBAYARANs { get; set; }
        public DbSet<PLACE> PLACEs { get; set; }
        public DbSet<PRODUCT> PRODUCTs { get; set; }
        public DbSet<REF_USER> REF_USER { get; set; }
        public DbSet<SUPPLIER> SUPPLIERs { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<TRANS_BIAYA> TRANS_BIAYA { get; set; }
        public DbSet<TRANS_HUTANG> TRANS_HUTANG { get; set; }
        public DbSet<TRANS_PEMBAYARAN_HUTANG> TRANS_PEMBAYARAN_HUTANG { get; set; }
        public DbSet<TRANS_PENJUALAN> TRANS_PENJUALAN { get; set; }
        public DbSet<TRANS_STOCK_BARANG> TRANS_STOCK_BARANG { get; set; }
    }
}
