using System;
using System.Collections;
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
    public partial class frmOdalar : Form
    {
        public frmOdalar()
        {
            InitializeComponent();
        }
        ArrayList odalar = new ArrayList();
        private void frmOdalar_Load(object sender, EventArgs e)
        {
            string odaAdi = "";
            string yeniDeger = "";
            // bool oldumu=false;
            for (int i = 1; i < this.Controls.Count + 1; i++)
            {
                odaAdi = Convert.ToString(this.Controls.Find("oda" + i.ToString(), true).FirstOrDefault() as Button);
                yeniDeger = odaAdi.Split(':').Last();
                odalar.Add(yeniDeger);
                //oldumu = true;
            }
            //if (oldumu == true)
            //{
            odalarinDurumu();
            //}
        }
        void odalarinDurumu()
        {
            csOdalar oda = new csOdalar();
            try
            {
                foreach (string odaninAdi in odalar) // ODA 1 Dolu
                {
                    oda.odaDegerleri(odaninAdi, "DOLU"); //
                    if (oda.durum_oku == "DOLU")
                    {
                        this.Controls.Find(oda.butonAdi, true)[0].BackColor = Color.IndianRed;
                        this.Controls.Find(oda.butonAdi, true)[0].Text = odaninAdi + " \n" + oda.alanKisi;                  
                    }
                        if (oda.durum_oku == "BOŞ")
                    {
                        this.Controls.Find(oda.butonAdi, true)[0].BackColor = Color.Aquamarine;
                    }
                }
            }
            catch (Exception hata) { System.Windows.Forms.MessageBox.Show("" + hata); }
        }

        private void frmOdalar_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
