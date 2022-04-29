using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Uyg03Test.ViewModel
{
    public class UrunEkleme
    {
        public string UrunAdi { get; set; }
        public string UrunKatId { get; set; }
        public decimal UrunFiyat { get; set; }
        public int StokAdet { get; set; }
    }
}