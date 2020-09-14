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
    public partial class DijeljenjeKolekcijeFrm : Form
    {

        #region PODACI

        String ime1;
        String ime2;
        float velicinaKolekcije;

        String user;
        String nazivKolekcije;
        int idKolekcije;
        String tip;
        SqlConnection konekcija;

        PomocnaKlasa pomocna = new PomocnaKlasa();
        RadSaFajlovima radSaFajlovima = new RadSaFajlovima();
        RadSaDijeljenjem radSaDijeljenjem = new RadSaDijeljenjem();

        enum Paneli
        {
            panel1,
            panel2,
            panel3
        }
        enum Izbor
        {
            Greska,
            ParNepar,
            Velicina
        }

        private Paneli paneli;
        private Izbor izbor;

        #endregion

        public DijeljenjeKolekcijeFrm(String user, String nazivKolekcije, int idKolekcije, String tip, SqlConnection conn)
        {
            InitializeComponent();

            this.user = user;
            this.nazivKolekcije = nazivKolekcije;
            this.idKolekcije = idKolekcije;
            this.tip = tip;
            this.konekcija = conn;

            paneli = Paneli.panel1;
            izbor = Izbor.Greska;
        }

        private void DijeljenjeKolekcijeFrm_Load(object sender, EventArgs e)
        {
            DKpanel1.Visible = true;
            panelVelicina.Visible = false;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (paneli == Paneli.panel1)
            {
                if (textBoxPrvoIme.Text == "" || textBoxDrugoIme.Text == "")
                {
                    MessageBox.Show("Polja moraju biti popunjena");
                    return;
                }
                String jmbg = pomocna.GetJmbgKorisnika(this.user, this.konekcija);
                String prvoIme = textBoxPrvoIme.Text;
                String drugoIme = textBoxDrugoIme.Text;
                int provjera = 0;
                provjera = pomocna.postojiKolekcija(prvoIme, jmbg, this.konekcija);
                if (provjera == 1)
                {
                    MessageBox.Show("Postoji kolekcija sa imenom " + prvoIme);
                    return;
                }
                provjera = pomocna.postojiKolekcija(drugoIme, jmbg, this.konekcija);
                if (provjera == 1)
                {
                    MessageBox.Show("Postoji kolekcija sa imenom " + drugoIme);
                    return;
                }
                SetIme1(prvoIme);
                SetIme2(drugoIme);

                paneli = Paneli.panel2;
                DKpanel2.Visible = true;
                DKpanel1.Visible = false;
                return;
            }
            if (paneli == Paneli.panel2)
            {
                if (izbor == Izbor.Greska)
                {
                    MessageBox.Show("Potrebno je izabrati kriterijum");
                    return;
                }
                if (izbor == Izbor.ParNepar)
                {
                    radSaDijeljenjem.prikaziDokumenta(this.idKolekcije, dataGridViewSpisak, this.konekcija);
                    DKpanel3.Visible = true;
                    radSaDijeljenjem.ParNepar(dataGridViewSpisak);
                }
                if (izbor == Izbor.Velicina)
                {
                    if (textBoxVelicina.Text == "")
                    {
                        MessageBox.Show("Polje mora biti popunjeno");
                        return;
                    }
                    float podatak = float.Parse(textBoxVelicina.Text);
                    if (podatak < 0)
                    {
                        MessageBox.Show("Vrijednost mora biti pozitivna");
                        return;
                    }
                    bool provjera1 = radSaDijeljenjem.postojeDokumentaManje(this.idKolekcije, podatak, this.konekcija);
                    bool provjera2 = radSaDijeljenjem.postojeDokumentaVise(this.idKolekcije, podatak, this.konekcija);
                    if (provjera1)
                    {
                        if (provjera2)
                        {
                            float vel = float.Parse(textBoxVelicina.Text);
                            SetVelicina(vel);
                            radSaDijeljenjem.prikaziDokumenta(this.idKolekcije, dataGridViewSpisak, this.konekcija);
                            DKpanel3.Visible = true;
                            radSaDijeljenjem.Velicina(dataGridViewSpisak, podatak);
                        }
                        else
                        {
                            MessageBox.Show("Ne postoje dokumenta sa vecom vrijednosti. Nije moguce kreirati kolekciju");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ne postoje dokumenta sa manjom vrijednosti. Nije moguce kreirati kolekciju");
                        return;
                    }
                }
                paneli = Paneli.panel3;
                DKpanel2.Visible = false;
                panelVelicina.Visible = false;
                return;
            }
            if (paneli == Paneli.panel3)
            {
                DialogResult dr = MessageBox.Show("Da li ste sigurni da zelite da podijelite kolekciju?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                String jmbg = pomocna.GetJmbgKorisnika(this.user, this.konekcija);
                if (dr == DialogResult.Yes)
                {
                    if (izbor == Izbor.ParNepar)
                    {
                        String prvoIme = GetIme1();
                        String drugoIme = GetIme2();
                        if (pomocna.sacuvajKolekciju(prvoIme, jmbg, this.tip, this.konekcija))
                        {
                            if (pomocna.sacuvajKolekciju(drugoIme, jmbg, this.tip, this.konekcija))
                            {
                                radSaFajlovima.KreirajKolekciju(this.user, prvoIme);
                                radSaFajlovima.KreirajKolekciju(this.user, drugoIme);
                                for(int i = 0; i < dataGridViewSpisak.RowCount; i++)
                                {
                                    String nazivFajla = dataGridViewSpisak.Rows[i].Cells["Naziv"].Value.ToString() + "." + this.tip;
                                    String putanja = "../../sve_kolekcije/" + this.user + "/" + this.nazivKolekcije + "/" + nazivFajla;
                                    if (i % 2 == 0)
                                    {
                                        radSaFajlovima.kopirajFajl(nazivFajla, putanja, this.user, prvoIme);
                                    }
                                    else
                                    {
                                        radSaFajlovima.kopirajFajl(nazivFajla, putanja, this.user, drugoIme);
                                    }
                                }
                                int id1 = pomocna.GetIdKolekcije(jmbg, prvoIme, this.konekcija);
                                int id2 = pomocna.GetIdKolekcije(jmbg, drugoIme, this.konekcija);
                                String[] spisakFajlova = radSaFajlovima.spisakFajlova(prvoIme, this.user);
                                for(int i = 0; i < spisakFajlova.Length; i++)
                                {
                                    String putanja = "../../sve_kolekcije/" + this.user + "/" + prvoIme + "/" + spisakFajlova[i];
                                    float velicina = radSaFajlovima.velicinaFajla(putanja);
                                    String imeBezEx = radSaFajlovima.imeFajla(spisakFajlova[i]);
                                    pomocna.sacuvajDokument(id1, imeBezEx, this.tip, velicina, this.konekcija);
                                }
                                spisakFajlova = radSaFajlovima.spisakFajlova(drugoIme, this.user);
                                for (int i = 0; i < spisakFajlova.Length; i++)
                                {
                                    String putanja = "../../sve_kolekcije/" + this.user + "/" + drugoIme + "/" + spisakFajlova[i];
                                    float velicina = radSaFajlovima.velicinaFajla(putanja);
                                    String imeBezEx = radSaFajlovima.imeFajla(spisakFajlova[i]);
                                    pomocna.sacuvajDokument(id2, imeBezEx, this.tip, velicina, this.konekcija);
                                }
                                int brDok = pomocna.brojDokumenata(id1, this.konekcija);
                                float KB = pomocna.velicinaKolekcijeKB(id1, this.konekcija);
                                if (brDok == -1)
                                {
                                    return;
                                }
                                if (KB == -1)
                                {
                                    return;
                                }
                                pomocna.updateKolekcijeDokumentaIVelicina(id1, brDok, KB, this.konekcija);

                                brDok = pomocna.brojDokumenata(id2, this.konekcija);
                                KB = pomocna.velicinaKolekcijeKB(id2, this.konekcija);
                                if (brDok == -1)
                                {
                                    return;
                                }
                                if (KB == -1)
                                {
                                    return;
                                }
                                pomocna.updateKolekcijeDokumentaIVelicina(id2, brDok, KB, this.konekcija);

                                MessageBox.Show("Uspjesno kreiranje dijaljenja kolekcije");
                                return;
                            }
                        }
                    }
                    if (izbor == Izbor.Velicina)
                    {
                        String prvoIme = GetIme1();
                        String drugoIme = GetIme2();
                        float velKB = GetVelicina();
                        if (pomocna.sacuvajKolekciju(prvoIme, jmbg, this.tip, this.konekcija))
                        {
                            if (pomocna.sacuvajKolekciju(drugoIme, jmbg, this.tip, this.konekcija))
                            {
                                /*
                                 *  prekopirati dokumenta iz trenutnog foldera
                                 *  prvo u prvu kolekciju
                                 *  zatim u drugu kolekciju
                                 *  ili naizmjenicno
                                 *  pratiti uslove..
                                 *  nakon kopiranja u bazu sacuvati dokumenta da se zna kojoj kolekciji pripadaju
                                 *  odraditi potrebne update-e
                                 */
                                //kreiranje foldera sa nazivima kolekcija
                                radSaFajlovima.KreirajKolekciju(this.user, prvoIme);
                                radSaFajlovima.KreirajKolekciju(this.user, drugoIme);
                                //kopiranje fajlova u nove kolekcije..manji u prvu veci u drugu
                                for (int i = 0; i < dataGridViewSpisak.RowCount; i++)
                                {
                                    String nazivFajla = dataGridViewSpisak.Rows[i].Cells["Naziv"].Value.ToString() + "." + this.tip;//naziv sa ext
                                    String putanja = "../../sve_kolekcije/" + this.user + "/" + this.nazivKolekcije + "/" + nazivFajla;
                                    float podatak = float.Parse(dataGridViewSpisak.Rows[i].Cells["Velicina"].Value.ToString());
                                    if (podatak < velKB)
                                    {
                                        //ako je velicina manja kopiramo u prvu novu kolekciju
                                        radSaFajlovima.kopirajFajl(nazivFajla, putanja, this.user, prvoIme);
                                    }
                                    else
                                    {
                                        //ako je velicina veca kopiramo u drugu
                                        if (podatak > velKB)
                                        {
                                            radSaFajlovima.kopirajFajl(nazivFajla, putanja, this.user, drugoIme);
                                        }
                                    }
                                }
                                //cuvanje dokumenata u bazu i upisivanje kojoj kolekciji pripada
                                int id1 = pomocna.GetIdKolekcije(jmbg, prvoIme, this.konekcija);
                                int id2 = pomocna.GetIdKolekcije(jmbg, drugoIme, this.konekcija);
                                String[] spisakFajlova = radSaFajlovima.spisakFajlova(prvoIme, this.user);//imena sa ext
                                for (int i = 0; i < spisakFajlova.Length; i++)
                                {
                                    String putanja = "../../sve_kolekcije/" + this.user + "/" + prvoIme + "/" + spisakFajlova[i];
                                    float velicina = radSaFajlovima.velicinaFajla(putanja);
                                    String imeBezEx = radSaFajlovima.imeFajla(spisakFajlova[i]);
                                    pomocna.sacuvajDokument(id1, imeBezEx, this.tip, velicina, this.konekcija);
                                }
                                spisakFajlova = radSaFajlovima.spisakFajlova(drugoIme, this.user);
                                for (int i = 0; i < spisakFajlova.Length; i++)
                                {
                                    String putanja = "../../sve_kolekcije/" + this.user + "/" + drugoIme + "/" + spisakFajlova[i];
                                    float velicina = radSaFajlovima.velicinaFajla(putanja);
                                    String imeBezEx = radSaFajlovima.imeFajla(spisakFajlova[i]);
                                    pomocna.sacuvajDokument(id2, imeBezEx, this.tip, velicina, this.konekcija);
                                }
                                //update podataka u kolekciji
                                int brDok = pomocna.brojDokumenata(id1, this.konekcija);
                                float KB = pomocna.velicinaKolekcijeKB(id1, this.konekcija);
                                if (brDok == -1)
                                {
                                    return;
                                }
                                if (KB == -1)
                                {
                                    return;
                                }
                                pomocna.updateKolekcijeDokumentaIVelicina(id1, brDok, KB, this.konekcija);

                                brDok = pomocna.brojDokumenata(id2, this.konekcija);
                                KB = pomocna.velicinaKolekcijeKB(id2, this.konekcija);
                                if (brDok == -1)
                                {
                                    return;
                                }
                                if (KB == -1)
                                {
                                    return;
                                }
                                pomocna.updateKolekcijeDokumentaIVelicina(id2, brDok, KB, this.konekcija);

                                MessageBox.Show("Uspjesno kreiranje dijaljenja kolekcije");
                                return;
                            }
                        }
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

        private void radiobtnParNepar_CheckedChanged(object sender, EventArgs e)
        {
            izbor = Izbor.ParNepar;
            panelVelicina.Visible = false;
        }

        private void radiobtnVelicina_CheckedChanged(object sender, EventArgs e)
        {
            izbor = Izbor.Velicina;
            panelVelicina.Visible = true;
        }

        #region PRISTUPNE METODE

        public String GetIme1()
        {
            return this.ime1;
        }

        public void SetIme1(String s)
        {
            this.ime1 = s;
        }

        public String GetIme2()
        {
            return this.ime2;
        }

        public void SetIme2(String s)
        {
            this.ime2 = s;
        }

        public float GetVelicina()
        {
            return this.velicinaKolekcije;
        }

        public void SetVelicina(float f)
        {
            this.velicinaKolekcije = f;
        }

        #endregion
    }
}
