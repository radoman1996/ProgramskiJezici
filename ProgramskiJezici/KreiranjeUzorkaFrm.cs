using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgramskiJezici
{
    public partial class KreiranjeUzorkaFrm : Form
    {

        #region PODACI

        String imeKolekcije;
        float velicinaOd;
        float velicinaDo;

        int idKolekcije;
        String user;
        String tip;
        String nazivKolekcije;
        SqlConnection konekcija;

        RadSaUzorcima radSaUzorcima = new RadSaUzorcima();
        PomocnaKlasa pomocna = new PomocnaKlasa();
        RadSaFajlovima radSaFajlovima = new RadSaFajlovima();

        enum Izbor
        {
            Greska,
            Slobodna,
            Parna,
            Neparna,
            Velicina
        }

        enum Paneli
        {
            panel1,
            panel2,
            panel3
        }

        private Izbor izbor;
        private Paneli paneli;

        #endregion

        public KreiranjeUzorkaFrm(int idKolekcije, String username, String tip, String naziv, SqlConnection conn)
        {
            InitializeComponent();

            this.idKolekcije = idKolekcije;
            this.user = username;
            this.tip = tip;
            this.nazivKolekcije = naziv;
            this.konekcija = conn;

            izbor = Izbor.Greska;
            paneli = Paneli.panel1;
        }

        private void KreiranjeUzorkaFrm_Load(object sender, EventArgs e)
        {
            KUpanel1.Visible = true;
            dataGridViewSelekcija.Enabled = true;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (paneli == Paneli.panel1)
            {
                if (textBoxImeKolekcije.Text == "")
                {
                    MessageBox.Show("Potrebno je da unesete ime kolekcije");
                    return;
                }
                String jmbg = pomocna.GetJmbgKorisnika(this.user, this.konekcija);
                int postojiKolekcija = pomocna.postojiKolekcija(textBoxImeKolekcije.Text, jmbg, this.konekcija);
                if (postojiKolekcija != 0)
                {
                    MessageBox.Show("Unesite drugo ime");
                    return;
                }
                SetIme(textBoxImeKolekcije.Text);
                paneli = Paneli.panel2;
                KUpanel2.Visible = true;
                KUpanel1.Visible = false;
                return;
            }
            if (paneli == Paneli.panel2)
            {
                if (izbor == Izbor.Greska)
                {
                    MessageBox.Show("Potrebno je za izaberete kriterijum za kreiranje uzorka");
                    return;
                }
                if (izbor == Izbor.Slobodna)
                {
                    radSaUzorcima.prikaziDokumenta(this.idKolekcije, -1, -1, dataGridViewSelekcija, this.konekcija);
                    KUpanel3.Visible = true;
                    radSaUzorcima.slobodnaSelekcija(dataGridViewSelekcija);
                }
                if (izbor == Izbor.Parna)
                {
                    radSaUzorcima.prikaziDokumenta(this.idKolekcije, -1, -1, dataGridViewSelekcija, this.konekcija);
                    KUpanel3.Visible = true;
                    radSaUzorcima.parnaSelekcija(dataGridViewSelekcija);
                }
                if (izbor == Izbor.Neparna)
                {
                    radSaUzorcima.prikaziDokumenta(this.idKolekcije, -1, -1, dataGridViewSelekcija, this.konekcija);
                    KUpanel3.Visible = true;
                    radSaUzorcima.neparnaSelekcija(dataGridViewSelekcija);
                }
                if (izbor == Izbor.Velicina)
                {
                    if (textBoxODkb.Text == "" || textBoxDOkb.Text == "")
                    {
                        MessageBox.Show("Polja moraju biti popunjena");
                        return;
                    }
                    float odKB = float.Parse(textBoxODkb.Text);
                    float doKB = float.Parse(textBoxDOkb.Text);
                    if (odKB > doKB)
                    {
                        MessageBox.Show("Velicina OD mora biti manja od velicine DO");
                        return;
                    }
                    if (odKB < 0 || doKB < 0)
                    {
                        MessageBox.Show("Vrijednosti moraju biti pozitivne");
                        return;
                    }
                    bool provjera = radSaUzorcima.postojeDokumenta(this.idKolekcije, odKB, doKB, this.konekcija);
                    if (provjera == false)
                    {
                        MessageBox.Show("Ne postoje dokumenta za prikazivanje");
                        return;
                    }
                    radSaUzorcima.prikaziDokumenta(this.idKolekcije, odKB, doKB, dataGridViewSelekcija, this.konekcija);
                    KUpanel3.Visible = true;
                    radSaUzorcima.velicinaSelekcija(dataGridViewSelekcija);
                }
                paneli = Paneli.panel3;
                KUpanel2.Visible = false;
                panelPoljaVelicine.Visible = false;
                return;
            }
            if (paneli == Paneli.panel3)
            {
                if (dataGridViewSelekcija.SelectedRows.Count < 1)
                {
                    MessageBox.Show("Potrebno je da izaberete dokumenta");
                    return;
                }
                DialogResult dr = MessageBox.Show("Da li ste sigurni da zelite da kreirate uzorak kolekcije?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    String naziv = GetIme();
                    String jmbg = pomocna.GetJmbgKorisnika(this.user, this.konekcija);
                    if(pomocna.sacuvajKolekciju(naziv, jmbg, this.tip, this.konekcija))
                    {
                        radSaFajlovima.KreirajKolekciju(this.user, naziv);
                        int broj = dataGridViewSelekcija.SelectedRows.Count;
                        for(int i = 0; i < broj; i++)
                        {
                            String nazivFajla = dataGridViewSelekcija.SelectedRows[i].Cells["Naziv"].Value.ToString();
                            String putanja = "../../sve_kolekcije/" + this.user + "/" + this.nazivKolekcije + "/" + nazivFajla + "." + this.tip;
                            radSaFajlovima.kopirajFajl(nazivFajla + "." + this.tip, putanja, this.user, naziv);
                        }
                        String[] spisak = radSaFajlovima.spisakFajlova(naziv, this.user);
                        int id = pomocna.GetIdKolekcije(jmbg, naziv, this.konekcija);
                        for(int i = 0; i < spisak.Length; i++)
                        {
                            String putanja = "../../sve_kolekcije/" + this.user + "/" + naziv + "/" + spisak[i];
                            float velicina = radSaFajlovima.velicinaFajla(putanja);
                            String imeBezEx = radSaFajlovima.imeFajla(spisak[i]);
                            pomocna.sacuvajDokument(id, imeBezEx, this.tip, velicina, this.konekcija);
                        }
                        int brDok = pomocna.brojDokumenata(id, this.konekcija);
                        float KB = pomocna.velicinaKolekcijeKB(id, this.konekcija);
                        if (brDok == -1)
                        {
                            return;
                        }
                        if (KB == -1)
                        {
                            return;
                        }
                        pomocna.updateKolekcijeDokumentaIVelicina(id, brDok, KB, this.konekcija);
                        MessageBox.Show("Uspjesno kreiranje uzorka kolekcije");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Greska pri cuvanju kolekcije uzorka");
                        return;
                    }

                }
                if (dr == DialogResult.No)
                {
                    this.Close();
                    return;
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radiobtnSlobodnaSelekcija_CheckedChanged(object sender, EventArgs e)
        {
            panelPoljaVelicine.Visible = false;
            izbor = Izbor.Slobodna;
        }

        private void radiobtnParnaSelekcija_CheckedChanged(object sender, EventArgs e)
        {
            panelPoljaVelicine.Visible = false;
            izbor = Izbor.Parna;
        }

        private void radiobtnNeparnaSelekcija_CheckedChanged(object sender, EventArgs e)
        {
            panelPoljaVelicine.Visible = false;
            izbor = Izbor.Neparna;
        }

        private void radiobtnVelicina_CheckedChanged(object sender, EventArgs e)
        {
            panelPoljaVelicine.Visible = true;
            izbor = Izbor.Velicina;
        }


        #region PRISTUPNE METODE

        public String GetIme()
        {
            return this.imeKolekcije;
        }

        public void SetIme(String s)
        {
            this.imeKolekcije = s;
        }


        public float GetOD()
        {
            return this.velicinaOd;
        }

        public void SetOD(float val)
        {
            this.velicinaOd = val;
        }


        public float GetDO()
        {
            return this.velicinaDo;
        }

        public void SetDO(float val)
        {
            this.velicinaDo = val;
        }

        #endregion

        private void dataGridViewSelekcija_Click(object sender, EventArgs e)
        {
            if (izbor == Izbor.Parna)
            {
                radSaUzorcima.parnaSelekcija(dataGridViewSelekcija);
            }
            if (izbor == Izbor.Neparna)
            {
                radSaUzorcima.neparnaSelekcija(dataGridViewSelekcija);
            }
        }
    }
}
