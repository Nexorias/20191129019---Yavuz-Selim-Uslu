 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Uyg03Test.ViewModel
{
    public class UrunModel
    {
        public int UrunId { get; set; }
        public string UrunAdi { get; set; }
        public int UrunKatId { get; set; }
        public string urunKatAdi { get; set; }
        public decimal UrunFiyat { get; set; }
        public int StokAdet { get; set; }
    }
}