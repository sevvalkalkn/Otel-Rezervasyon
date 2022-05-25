using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.Remoting.Messaging;

namespace OtelSistemi
{
    class csMusteriEkrani
    {
        DataBase db = new DataBase();
        public string guncelledurum { get; set; }
        public string silDurum { get; set; }

        public DataTable tablola()
        {
            if (db.baglanti.State == ConnectionState.Open)
            {
                db.baglanti.Close();
            }
            try
            {
                db.baglanti.Open();
                SqlCommand veriAl = new SqlCommand("select * from musteriler", db.baglanti);
                SqlDataAdapter adaptor = new SqlDataAdapter(veriAl);
                DataTable tablo = new DataTable();
                adaptor.Fill(tablo);
                return tablo;
            }
            catch { return null; }
            finally
            {
                db.baglanti.Close();
            }
        }
        public void musteriGuncelle(int id, string adi, string soyadi, string cinsiyet, string telefonNo, string mail, string tcNo, string odaNo, string ücret, DateTime giris, DateTime cikis)
        {
            if (db.baglanti.State == ConnectionState.Open)
            {
                db.baglanti.Close();
            }
            try
            {
                db.baglanti.Open();
                SqlCommand guncelle = new SqlCommand("UPDATE musteriler SET adi=@adi, soyadi=@soyadi, cinsiyet=@cinsiyet, telefon=@telefon, mail=@mail, tcNo=@tc, odaNo=@oda, ücret=@ucret, girisTarihi=@tarih1,cikisTarihi=@tarih2 WHERE id=@id", db.baglanti);
                guncelle.Parameters.AddWithValue("@adi", adi);
                guncelle.Parameters.AddWithValue("@soyadi", soyadi);
                guncelle.Parameters.AddWithValue("@cinsiyet", cinsiyet);
                guncelle.Parameters.AddWithValue("@telefon", telefonNo);
                guncelle.Parameters.AddWithValue("@mail", mail);
                guncelle.Parameters.AddWithValue("@tc", tcNo);
                guncelle.Parameters.AddWithValue("@oda", odaNo);
                guncelle.Parameters.AddWithValue("@ucret", ücret);
                guncelle.Parameters.AddWithValue("@tarih1", giris);
                guncelle.Parameters.AddWithValue("@tarih2", cikis);
                guncelle.Parameters.AddWithValue("@id", id);
                guncelle.ExecuteNonQuery();
                guncelledurum = adi + "" + soyadi + "İsimli kişinin verileri güncellenmiştir.";

            }
            catch (Exception ex){ }
            finally
            {
                db.baglanti.Close();
            }
        }
        public void musteriSil(int id)
        {
            if (db.baglanti.State == ConnectionState.Open)
            {
                db.baglanti.Close();
            }
            try
            {
                db.baglanti.Open();
                SqlCommand sil = new SqlCommand("DELETE musteriler WHERE id=@id", db.baglanti);
                sil.Parameters.AddWithValue("@id", id);
                sil.ExecuteNonQuery();
                silDurum = "Silme işlemi başarılı";
            }
            catch { }
            finally
            {
                db.baglanti.Close();
            }
        }
        public DataTable musteriAra(string adi)
        {
            if (db.baglanti.State == ConnectionState.Open)
            {
                db.baglanti.Close();
            }
            try
            {
                db.baglanti.Open();
                SqlCommand arama = new SqlCommand("SELECT * FROM musteriler WHERE adi LIKE '%'+@adi+'%' ", db.baglanti);
                arama.Parameters.AddWithValue("@adi", adi);
                SqlDataAdapter adaptor = new SqlDataAdapter(arama);
                DataTable tablo = new DataTable();
                adaptor.Fill(tablo);
                return tablo;
            }


            catch { return null; }
            finally
            {
                db.baglanti.Close();
            }
        }


    }
}
