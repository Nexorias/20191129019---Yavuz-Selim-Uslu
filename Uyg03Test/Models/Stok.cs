//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Uyg03Test.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Stok
    {
        public int StokAdet { get; set; }
        public int StokUrunId { get; set; }
    
        public virtual Urun Urun { get; set; }
    }
}