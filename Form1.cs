using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtelSistemi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            giris grs = new giris();
            anaEkran main = new anaEkran();
            if (txtkullanici.Text == string.Empty || txtsifre.Text == string.Empty)
            {
                MessageBox.Show("Lütfen kullanıcı adını ve şifreni gir.", "HATA | Pansiyon Otomasyonu", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                grs.girisYap(txtkullanici.Text, txtsifre.Text, DateTime.Now);
                string bilgiTut = txtkullanici.Text + " " + txtsifre.Text.ToString();
                if (grs.girisDurumu == bilgiTut)
                {
                    main.Show();
                    Hide();
                }                
            }
        }
    }
}
