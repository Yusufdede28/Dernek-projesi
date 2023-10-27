using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Dernek_projesi
{
    public partial class KullanıcıGiris : Form
    {
        public KullanıcıGiris()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {


               OleDbConnection baglanti = new OleDbConnection("provider=microsoft.jet.oledb.4.0; data source=Vt.mdb");
                baglanti.Open();
                OleDbCommand sorgu = new OleDbCommand("select kullaniciadi,sifre from kullanicigirisi where kullaniciadi=@ad and sifre=@sifre ", baglanti);
                sorgu.Parameters.AddWithValue("@ad", textBox1.Text);
                sorgu.Parameters.AddWithValue("@sifre", textBox2.Text);
                OleDbDataReader dr;
                dr = sorgu.ExecuteReader();

                if (dr.Read())
                {
                    if (guvenlikkodu == Convert.ToUInt32(textBox3.Text))
                    {
                        // Kullanıcı giriş formunu gizle
                        this.Hide();

                        // mevcut_üye_listesi formunu göster

                        Yonetici_Sayfasi frm = new Yonetici_Sayfasi();
                        frm.FormClosed += (s, args) => this.Close();
                        frm.Show();
                    }
                    else
                    {
                        MessageBox.Show(" Güvenlik Kodunu Hatalı Girdiniz");
                    }
                }
                else
                {
                    baglanti.Close();
                    MessageBox.Show("Yanlış kullanıcı adı veya şifre");
                }
            }
            catch
            {
                MessageBox.Show("Lütfen kullanıcı adı ve şifrenizle giriş yapınız");
            }
            finally
            {
                textBox1.Text = "";
                textBox2.Clear();
                textBox3.Clear();
            }
        }
        int guvenlikkodu;
        private void KullanıcıGiris_Load(object sender, EventArgs e)
        {
            Random r = new Random();
            guvenlikkodu = r.Next(1000, 9999);
            label5.Text = guvenlikkodu.ToString();
        }
    }
}
