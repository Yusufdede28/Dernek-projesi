using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Dernek_projesi
{
    public partial class Mevcut_üyeyi_düzenle : Form
    {
       
        public Mevcut_üyeyi_düzenle()
        {
            InitializeComponent();
        }

        private void Mevcut_üyeyi_düzenle_Load(object sender, EventArgs e)
        {
            textBox1.Text = Program.KimlikNo;
            textBox2.Text = Program.Ad;
            textBox3.Text = Program.Soyad;
            textBox4.Text = Program.Şehir;
            textBox5.Text = Program.Yaş;
            textBox6.Text = Program.KanGrubu;
            textBox7.Text = Program.AidatTL;
            checkBox1.Text= Program.Aktif;
        }

        private void Kaydet_btn_Click(object sender, EventArgs e)
        {
            using (OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Vt.mdb"))
            {
                baglanti.Open();
                OleDbCommand komut = new OleDbCommand("update Uyebilgileri set [Kimlik No]=@KimlikNo,Ad=@Ad,Soyad=@Soyad,Sehir=@Sehir,Yas=@Yas,[Kan Grubu]=@KanGrubu,[Aidat TL]=@AidatTL where [Kimlik No]=@KimlikNo", baglanti);
                komut.Parameters.AddWithValue("@KimlikNo", textBox1.Text);
                komut.Parameters.AddWithValue("@Ad", textBox2.Text);
                komut.Parameters.AddWithValue("@Soyad", textBox3.Text);
                komut.Parameters.AddWithValue("@Sehir", textBox4.Text);
                komut.Parameters.AddWithValue("@Yas", textBox5.Text);
                komut.Parameters.AddWithValue("@KanGrubu", textBox6.Text);
                komut.Parameters.AddWithValue("@AidatTL", textBox7.Text);
               
                komut.ExecuteNonQuery();
            }
            MessageBox.Show("Düzenleme başarılı bir şekilde gerçekleşti");

            Yonetici_Sayfasi frm = new Yonetici_Sayfasi();
            frm.Show();
            this.Close();
        }

        private void GeriDon_btn_Click(object sender, EventArgs e)
        {

            Yonetici_Sayfasi frm = new Yonetici_Sayfasi();
            frm.Show();
            this.Hide();
        }
    }
}
