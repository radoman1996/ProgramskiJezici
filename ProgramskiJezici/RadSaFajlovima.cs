using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace ProgramskiJezici
{
    class RadSaFajlovima
    {
        public void KreirajKorisnickiFolder(String username)
        {
            DirectoryInfo di = new DirectoryInfo("../../sve_kolekcije/" + username);
            try
            {
                di.Create();
            }
            catch(Exception err)
            {
                MessageBox.Show("Greska prilikom kreiranja foldera " + err);
            }
        }

        public void KreirajKolekciju(String username, String naziv)
        {
            DirectoryInfo di = new DirectoryInfo("../../sve_kolekcije/" + username + "/" + naziv);
            try
            {
                di.Create();
            }
            catch(Exception err)
            {
                MessageBox.Show("Greska prilikom kreiranja kolekcije " + err);
            }
        }

        public String[] imeFajla(String[] imeSaExt)
        {
            String[] podaci = new String[imeSaExt.Length];
            for(int i = 0; i < imeSaExt.Length; i++)
            {
                String ime = imeSaExt[i];
                int pozicija = ime.LastIndexOf('.');
                String konacno = ime.Substring(0, pozicija); ;
                podaci[i] = konacno;
            }
            return podaci;
        }

        public String imeFajla(String imeSaExt)
        {
            String podatak = "";
            int pozicija = imeSaExt.LastIndexOf('.');
            podatak = imeSaExt.Substring(0, pozicija);
            return podatak;
        }

        public float[] velicinaFajla(String[] putanje)
        {
            float[] podatak = new float[putanje.Length];
            for(int i = 0; i < putanje.Length; i++)
            {
                FileInfo fi = new FileInfo(putanje[i]);
                double d = fi.Length / 1024.0;
                float t = (float)Math.Ceiling(d);
                podatak[i] = t;
            }
            return podatak;
        }

        public float velicinaFajla(String putanja)
        {
            float podatak = 0;
            FileInfo fi = new FileInfo(putanja);
            double d = fi.Length / 1024.0;
            float t = (float)Math.Ceiling(d);
            podatak = t;
            return podatak;
        }

        public void kopirajFajl(String imeFajla, String putanja, String username, String imeKolekcije)
        {
            FileInfo fi = new FileInfo(putanja);
            if (File.Exists("../../sve_kolekcije/" + username + "/" + imeKolekcije + "/" + imeFajla))
            {
                fi.CopyTo("../../sve_kolekcije/" + username + "/" + imeKolekcije + "/" + "(1)" + imeFajla);
            }
            else
            {
                fi.CopyTo("../../sve_kolekcije/" + username + "/" + imeKolekcije + "/" + imeFajla);
            }
        }

        public void izbrisiFajl(String imeFajla, String username, String imeKolekcije)
        {
            Console.WriteLine(imeFajla);
            FileInfo fi = new FileInfo("../../sve_kolekcije/" + username + "/" + imeKolekcije + "/" + imeFajla);
            fi.Delete();
        }

        public void izbrisiJednuKolekciju(String username, String naziv)
        {
            DirectoryInfo di = new DirectoryInfo("../../sve_kolekcije/" + username + "/" + naziv);
            String[] nizFajlova = Directory.GetFiles("../../sve_kolekcije/" + username + "/" + naziv);
            String[] nazivi;
            for(int i = 0; i < nizFajlova.Length; i++)
            {
                nazivi = nizFajlova[i].Split('\\');
                izbrisiFajl(nazivi[nazivi.Length - 1], username, naziv);
            }
            di.Delete();
        }

        public String[] spisakFajlova(String nazivKolekcije, String username)
        {
            String[] nizPath = Directory.GetFiles("../../sve_kolekcije/" + username + "/" + nazivKolekcije);
            String[] spisak = new String[nizPath.Length];
            String[] nazivi;
            for(int i = 0; i < nizPath.Length; i++)
            {
                nazivi = nizPath[i].Split('\\');
                spisak[i] = nazivi[nazivi.Length - 1];
            }
            return spisak;
        }

    }
}
