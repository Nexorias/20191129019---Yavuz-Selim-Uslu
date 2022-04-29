using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Uyg03Test.ViewModel
{
    public class UrunDuzenlemeModeli
    {
        public string UrunId { get; set; }
        public string UrunAdi { get; set; }
        public string UrunKatId { get; set; }
        public decimal UrunFiyat { get; set; }
        public int UrunStokAdet { get; set; }
    }
}