using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Uyg03Test.Models;
using Uyg03Test.ViewModel;

namespace Uyg03Test.Controllers
{
    public class ServiceController : ApiController
    {
        DB02Entities3 db = new DB02Entities3();
        SonucModel sonuc = new SonucModel();

        #region Kategori
        
        [HttpGet]
        [Route("api/kategoriliste")]

        public List<KategoriModel>  KategoriListe()
        {
            List<KategoriModel> liste = db.Kategori.Select(x => new KategoriModel()
            {

                KatAdi = x.KatAdi,
                KatId = x.KatId,
                KatUrunSay = x.Urun.Count()

            }).ToList(); 

            return liste;
        }

        [HttpGet]
        [Route("api/kategoribyid/{katId}")]

        public KategoriModel KategoriById(int KatId)
        {
            KategoriModel kayit = db.Kategori.Where(s => s.KatId == KatId).Select(x => 
             new KategoriModel()
 
            {
                KatAdi = x.KatAdi,
                KatId = x.KatId,
                KatUrunSay = x.Urun.Count()

            }).FirstOrDefault();
            return kayit;
        }
        [HttpPost]
        [Route("api/kategoriekle")]
        public SonucModel KategoriEkle(KategoriModel model)
        {
            if (db.Kategori.Count(s=>s.KatId==model.KatId) > 0)
            {
                sonuc.islem = false;
                sonuc.Mesaj = "Girilen Kategori Adı Kayıtlıdır!";
                return sonuc;
            }

            Kategori yeni = new Kategori();
            yeni.KatAdi = model.KatAdi;
            db.Kategori.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.Mesaj = "Kategori Başarıyla Eklenmiştir!";

            return sonuc;
        }
        [HttpPut]
        [Route("api/kategoriduzenle")]
        public SonucModel KategoriDuzenle(KategoriModel model)
        {
            Kategori kayit = db.Kategori.Where(s => s.KatId == model.KatId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.Mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }

            sonuc.islem = true;
            sonuc.Mesaj = "Kategori Düzenlendi";
            return sonuc;
        }
        [HttpDelete]
        [Route("api/kategorisil/{katId}")]
        public SonucModel KategoriSil(int katId)
        {
            Kategori kayit = db.Kategori.Where(s => s.KatId == katId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.Mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }

            if (db.Urun.Count(s=> s.UrunKatId == katId) > 0)
            {
                sonuc.islem = false;
                sonuc.Mesaj = "Ürün kaydı olan kategori silinemez.";
                return sonuc;
            }

            db.Kategori.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.Mesaj = "Kategori silindi";

            return sonuc; 
        }

        #endregion

        #region Urun
        [HttpGet]
        [Route("api/urunliste")]
        public List<UrunModel> UrunListe()
        {
            List<UrunModel> liste = db.Urun.Select(x => new UrunModel()
            {
                UrunId = x.UrunId,
                UrunAdi = x.UrunAdi,
                UrunKatId = x.UrunKatId,
                urunKatAdi = x.Kategori.KatAdi,
                UrunFiyat = x.UrunFiyat,
                StokAdet = x.UrunStokAdet,

            }).ToList();

            return liste;
        }
        [HttpGet]
        [Route("api/urunlistebyid/{katId}")]
        public List<UrunModel> urunListeByKatId(int katId)
        {
            List<UrunModel> liste = db.Urun.Where(s => s.UrunKatId == katId).Select(x => new UrunModel()
            {
                UrunId = x.UrunId,
                UrunAdi = x.UrunAdi,
                UrunKatId = x.UrunKatId,
                urunKatAdi = x.Kategori.KatAdi,
                UrunFiyat = x.UrunFiyat,
                StokAdet = x.UrunStokAdet,

            }).ToList();

            return liste;
        }
        [HttpGet]
        [Route("api/urunbyid/{urunId}")]
        public UrunModel UrunbyId(int urunId)
        {
            UrunModel kayit = db.Urun.Where(s => s.UrunId == urunId).Select(x=> new UrunModel()
            {
                UrunId = x.UrunId,
                UrunAdi = x.UrunAdi,
                urunKatAdi = x.Kategori.KatAdi,
                UrunKatId = x.UrunKatId,
                UrunFiyat = x.UrunFiyat,
                StokAdet = x.UrunStokAdet
            }).FirstOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/urunekle")]
        public SonucModel UrunEkle(UrunModel Model)
        {
            if (db.Urun.Count(s=>s.UrunAdi==Model.UrunAdi && s.UrunKatId == Model.UrunKatId)
                > 0)
            {
                sonuc.islem = false;
                sonuc.Mesaj = "Girilen ürün ilgili kategoride kayıtlıdır.";
                return sonuc;

            }

            Urun yeni = new Urun();
            yeni.UrunAdi = Model.UrunAdi;
            yeni.UrunFiyat = Model.UrunFiyat;
            yeni.UrunKatId = Model.UrunKatId;
            yeni.UrunStokAdet = Model.StokAdet;

            db.Urun.Add(yeni);
            db.SaveChanges();

            sonuc.islem= true;
            sonuc.Mesaj = "Urun Başarıyla Eklendi!";

            return sonuc;
        }
        [HttpPut]
        [Route("api/urunduzenle")]
        public SonucModel UrunDuzenle(UrunModel Model)
        {
            Urun kayit = db.Urun.Where(s => s.UrunId == Model.UrunId).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.Mesaj = "Kayıt bulunamadı";
                return sonuc;
            }
            kayit.UrunAdi = Model.UrunAdi;
            kayit.UrunFiyat = Model.UrunFiyat;
            kayit.UrunKatId = Model.UrunKatId;
            kayit.UrunStokAdet = Model.StokAdet;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.Mesaj = "Ürün düzenlendi!";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/urunsil/{urunId}")]
        public SonucModel urunsil(int urunId)
        {
            Urun kayit = db.Urun.Where(s => s.UrunId == urunId).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.Mesaj = "Kayıt bulunamadı!";
                return sonuc;
            }
            db.Urun.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.Mesaj = "Ürün Silindi";
            return sonuc;
        }
        #endregion
    }
}
