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
    
    public partial class TRANS_PEMBAYARAN_HUTANG
    {
        public TRANS_PEMBAYARAN_HUTANG()
        {
            this.JURNALs = new HashSet<JURNAL>();
        }
    
        public long TRANS_BYR_UTANG_ID { get; set; }
        public Nullable<long> TRANS_HUTANG_ID { get; set; }
        public string TRANS_NO { get; set; }
        public Nullable<decimal> TOTAL { get; set; }
        public System.DateTime TANGGAL { get; set; }
    
        public virtual ICollection<JURNAL> JURNALs { get; set; }
        public virtual TRANS_HUTANG TRANS_HUTANG { get; set; }
    }
}
