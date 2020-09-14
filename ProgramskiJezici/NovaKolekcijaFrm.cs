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

namespace ProgramskiJezici
{
    public partial class NovaKolekcijaFrm : Form
    {
        #region PODACI

        PomocnaKlasa pomocna = new PomocnaKlasa();
        RadSaFajlovima radSaFajlovima = new RadSaFajlovima();
        SqlConnection conn;
        String user;
        String tip = "";
        String prvoIme;
        String drugoIme;

        #endregion

        public NovaKolekcijaFrm(SqlConnection konekcija, String username)
        {
            InitializeComponent();
            this.conn = konekcija;
            this.user = username;
        }

        public NovaKolekcijaFrm(SqlConnection konekcija, String username, String tip, String ime1, String ime2)
        {
            InitializeComponent();
            this.conn = konekcija;
            this.user = username;
            this.tip = tip;
            this.prvoIme = ime1;
            this.drugoIme = ime2;
            
        }

        private void NovaKolekcijaFrm_Load(object sender, EventArgs e)
        {
            if (this.tip == "")
            {
                pomocna.popuniTip(comboBoxTipKolekcije, conn);
            }
            else
            {
                comboBoxTipKolekcije.Items.Add(this.tip);
            }
        }

        private void btnKreirajNovuKolekciju_Click(object sender, EventArgs e)
        {
            if (this.tip == "")
            {
                cuvanje();
            }
            else
            {
                String nazivKolekcije = textBoxNovoImeKolekcije.Text;
                int vr = cuvanje();
                if (vr == 0)
                {
                    String[] spisakFajlova1 = radSaFajlovima.spisakFajlova(this.prvoIme, this.user);
                    String[] spisakFajlova2 = radSaFajlovima.spisakFajlova(this.drugoIme, this.user);
                    for(int i = 0; i < spisakFajlova1.Length; i++)
                    {
                        String putanja = "../../sve_kolekcije/" + this.user + "/" + this.prvoIme + "/" + spisakFajlova1[i];
                        radSaFajlovima.kopirajFajl(spisakFajlova1[i], putanja, this.user, nazivKolekcije);
                    }
                    for (int i = 0; i < spisakFajlova2.Length; i++)
                    {
                        String putanja = "../../sve_kolekcije/" + this.user + "/" + this.drugoIme + "/" + spisakFajlova2[i];
                        radSaFajlovima.kopirajFajl(spisakFajlova2[i], putanja, this.user, nazivKolekcije);
                    }
                    String[] spisakUnije = radSaFajlovima.spisakFajlova(nazivKolekcije, this.user);
                    String jmbg = pomocna.GetJmbgKorisnika(this.user, this.conn);
                    int idKolekcije = pomocna.GetIdKolekcije(jmbg, nazivKolekcije, conn);
                    if (idKolekcije != -1)
                    {
                        for(int i = 0; i < spisakUnije.Length; i++)
                        {
                            String putanja = "../../sve_kolekcije/" + this.user + "/" + nazivKolekcije + "/" + spisakUnije[i];
                            float velicina = radSaFajlovima.velicinaFajla(putanja);
                            String imeBezEx = radSaFajlovima.imeFajla(spisakUnije[i]);
                            pomocna.sacuvajDokument(idKolekcije, imeBezEx, this.tip, velicina, this.conn);
                        }
                        int brDoc = pomocna.brojDokumenata(idKolekcije, conn);
                        if (brDoc == -1)
                        {
                            return;
                        }
                        float velKB = pomocna.velicinaKolekcijeKB(idKolekcije, conn);
                        if (velKB == -1)
                        {
                            return;
                        }
                        int konacno = pomocna.updateKolekcijeDokumentaIVelicina(idKolekcije, brDoc, velKB, conn);
                        if (konacno == 0)
                        {
                            int datum = pomocna.updateDatumModifikovanja(idKolekcije, conn);
                            if (datum == 0)
                            {
                                MessageBox.Show("Uspjesno ste kreirali uniju kolekcija");
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
            
        }

        private void btnOtkaziKolekciju_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public int cuvanje()
        {
            String jmbg = pomocna.GetJmbgKorisnika(user, conn);
            int postoji = pomocna.postojiKolekcija(textBoxNovoImeKolekcije.Text, jmbg, conn);
            if (postoji != 0)
            {
                return 1;
            }
            if (pomocna.sacuvajKolekciju(textBoxNovoImeKolekcije, jmbg, comboBoxTipKolekcije, conn))
            {
                radSaFajlovima.KreirajKolekciju(user, textBoxNovoImeKolekcije.Text);
                textBoxNovoImeKolekcije.Text = "";
                comboBoxTipKolekcije.SelectedIndex = -1;
            }
            return 0;
        }
        
    }
}
