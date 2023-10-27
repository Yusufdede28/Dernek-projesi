using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dernek_projesi
{
    internal static class Program
    {
        public static string KimlikNo, Ad, Soyad, Şehir, Yaş, KanGrubu, AidatTL, Aktif;
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new KullanıcıGiris());
        }
    }
}
