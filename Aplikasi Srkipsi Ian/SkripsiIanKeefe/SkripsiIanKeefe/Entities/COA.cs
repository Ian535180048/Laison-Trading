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
    
    public partial class COA
    {
        public COA()
        {
            this.JURNALs = new HashSet<JURNAL>();
        }
    
        public long COA_ID { get; set; }
        public string COA_NO { get; set; }
    
        public virtual ICollection<JURNAL> JURNALs { get; set; }
    }
}
