using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;

namespace OtelSistemi
{
    public partial class frmMusteriKayit: Form
    {
        public frmMusteriKayit()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                    {
                        m.Result = (IntPtr)0x2;
                    }
                    return;
            }
            base.WndProc(ref m);
        }
        ArrayList odalar = new ArrayList();
        void koltukYazdir()
        {
            string oda = "";
            for (int i = 0; i < odalar.Count; i++)
            {
                oda += odalar[i].ToString() + ",";
            }
            if (odalar.Count >= 1)
            {
                oda = oda.Remove(oda.Length - 1, 1);
            }
            txtOda.Text = oda;
        }

        private void odaTikla(object sender, EventArgs e)
        {
            if (((Button)sender).BackColor == Color.Aquamarine)
            {
                ((Button)sender).BackColor = Color.YellowGreen;
                if (!odalar.Contains(((Button)sender).Text))
                {
                    odalar.Add(((Button)sender).Text);
                }
                koltukYazdir();
            }
            else if (((Button)sender).BackColor == Color.YellowGreen)
            {
                ((Button)sender).BackColor = Color.Aquamarine;
                if (odalar.Contains(((Button)sender).Text))
                {
                    odalar.Remove(((Button)sender).Text);
                }
                koltukYazdir();
            }
        }
        public DateTime girisTarihi { get; set; }
        public DateTime cikisTarihi { get; set; }
         private void button1_Click(object sender, EventArgs e)
        {
            girisTarihi = Convert.ToDateTime(dateTimePicker1.Value);
            cikisTarihi = Convert.ToDateTime(dateTimePicker2.Value);
            musteriKayit kayit = new musteriKayit();
            for (int i = 0; i < odalar.Count; i++)
            {
                string oda = odalar[i].ToString();
                kayit.kayitAl(txtAdi.Text, txtSoyadi.Text, cmbCinsiyet.Text, txtTelefon.Text, txtMail.Text, txtTc.Text, oda, txtUcret.Text, girisTarihi, cikisTarihi);
            }
            tmrKontrol.Start();
        }

       
        private void tmrKontrol_Tick(object sender, EventArgs e)
        {
            DataBase db = new DataBase();
            if (db.baglanti.State == ConnectionState.Open)
            {
                db.baglanti.Close();
            }
            try
            {
                db.baglanti.Open();
                SqlCommand donustur = new SqlCommand("SELECT * FROM odalar WHERE durumu=@durum", db.baglanti);
                donustur.Parameters.AddWithValue("@durum", "DOLU");
                SqlDataReader donustur_Oku = donustur.ExecuteReader();
                while (donustur_Oku.Read())
                {
                    string butonAdi = donustur_Oku["butonAdi"].ToString();
                    string durumu = donustur_Oku["durumu"].ToString();
                    if (durumu.Equals("DOLU"))
                    {
                        Controls.Find(butonAdi, true)[0].BackColor = Color.IndianRed;
                    }
                    if (durumu.Equals("BOŞ"))
                    {
                        Controls.Find(butonAdi, true)[0].BackColor = Color.Aquamarine;
                    }
                }
                donustur.Dispose();
                donustur_Oku.Close();
                db.baglanti.Close();
                tmrKontrol.Stop();
            }
            catch (Exception hata) { System.Windows.Forms.MessageBox.Show("" + hata); }
            finally
            {
                db.baglanti.Close();
            }
        }
        private void frmMusteriKayit_Load(object sender, EventArgs e)
        {
            tmrKontrol.Start();
        }

       
    }
}

