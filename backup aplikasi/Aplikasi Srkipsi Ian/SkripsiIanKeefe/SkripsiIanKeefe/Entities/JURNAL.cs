//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class JURNAL
    {
        public long JURNAL_ID { get; set; }
        public Nullable<long> TRANS_PENJUALAN_ID { get; set; }
        public Nullable<long> TRANS_HUTANG_ID { get; set; }
        public Nullable<long> TRANS_BIAYA_ID { get; set; }
        public Nullable<long> TRANS_BYR_UTANG_ID { get; set; }
        public Nullable<long> TRANS_STOCK_BARANG_ID { get; set; }
        public Nullable<long> COA_ID { get; set; }
        public Nullable<decimal> IN_AMT { get; set; }
        public Nullable<decimal> OUT_AMT { get; set; }
        public string DESCR { get; set; }
        public Nullable<System.DateTime> TANGGAL { get; set; }
    
        public virtual COA COA { get; set; }
        public virtual TRANS_BIAYA TRANS_BIAYA { get; set; }
        public virtual TRANS_PEMBAYARAN_HUTANG TRANS_PEMBAYARAN_HUTANG { get; set; }
        public virtual TRANS_HUTANG TRANS_HUTANG { get; set; }
        public virtual TRANS_PENJUALAN TRANS_PENJUALAN { get; set; }
        public virtual TRANS_STOCK_BARANG TRANS_STOCK_BARANG { get; set; }
    }
}
