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
    public partial class Yeni_Uye_Ekleme : Form
    {
        public Yeni_Uye_Ekleme()
        {
            InitializeComponent();
        }

        private void Yeni_Uye_Ekleme_Load(object sender, EventArgs e)
        {

        }

        private void Kaydet_btn_Click(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Vt.mdb");
            baglanti.Open();

            OleDbCommand sorgu = new OleDbCommand("select [Kimlik No] from Uyebilgileri where [Kimlik No]=@KimlikNo", baglanti);
            sorgu.Parameters.AddWithValue("@KimlikNo", textBox1.Text);

            OleDbDataReader dr;
            dr = sorgu.ExecuteReader();

            if (dr.Read())
            {
                MessageBox.Show("Bu Kimlik Numarası Zaten Mevcut");
            }
            else
            {
                OleDbCommand komut = new OleDbCommand("INSERT INTO Uyebilgileri ([Kimlik No], Ad, Soyad, Sehir, Yas, [Kan Grubu], [Aidat TL]) VALUES (@KimlikNo, @Ad, @Soyad, @Sehir, @Yas, @KanGrubu, @AidatTL)", baglanti);
                komut.Parameters.AddWithValue("@KimlikNo", textBox1.Text);
                komut.Parameters.AddWithValue("@Ad", textBox2.Text);
                komut.Parameters.AddWithValue("@Soyad", textBox3.Text);
                komut.Parameters.AddWithValue("@Sehir", textBox4.Text);
                komut.Parameters.AddWithValue("@Yas", textBox5.Text);
                komut.Parameters.AddWithValue("@KanGrubu", textBox6.Text);
                komut.Parameters.AddWithValue("@AidatTL", textBox7.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Yeni üye ekleme başarılı");

                Yonetici_Sayfasi frm = new Yonetici_Sayfasi();
                frm.Show();
                this.Hide();
            }






        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Girilen karakterin rakam olup olmadığını kontrol edin
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Rakam değilse karakteri reddedin
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // TextBox içeriğinin uzunluğunu kontrol edin
            if (textBox1.Text.Length > 11)
            {
                textBox1.Text = textBox1.Text.Substring(0, 11); // Maksimum 11 karaktere sınırlayın
            }
        }

        private void GeriDon_btn_Click(object sender, EventArgs e)
        {
            Yonetici_Sayfasi frm = new Yonetici_Sayfasi();
            frm.Show();
            this.Hide();
        }
    }
}
