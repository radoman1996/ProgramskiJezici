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
using System.Threading;

namespace ProgramskiJezici
{
    public partial class ProgressBar : Form
    {
        #region PODACI

        int idKolekcije;
        String tip;
        float[] velicine;
        String[] imena;
        String[] putanje;
        String username;
        String ime;
        SqlConnection konekcija;

        RadSaFajlovima radSaFajlovima = new RadSaFajlovima();
        PomocnaKlasa pomocna = new PomocnaKlasa();

        #endregion

        public ProgressBar(int idKolekcije, String tip, String username, String ime, float[] velicine, String[] imena, String[] putanje, SqlConnection konekcija)
        {
            InitializeComponent();
            this.idKolekcije = idKolekcije;
            this.tip = tip;
            this.username = username;
            this.ime = ime;
            this.velicine = new float[velicine.Length];
            for(int i=0; i < velicine.Length; i++)
            {
                this.velicine[i] = velicine[i];
            }
            this.imena = new String[imena.Length];
            for(int i = 0; i < imena.Length; i++)
            {
                this.imena[i] = imena[i];
            }
            this.putanje = new String[putanje.Length];
            for(int i = 0; i < putanje.Length; i++)
            {
                this.putanje[i] = putanje[i];
            }
            this.konekcija = konekcija;

            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.RunWorkerAsync();
        }

        private void ProgressBar_Load(object sender, EventArgs e)
        {

        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = 0;
            foreach (String fajl in imena)
            {
                Thread.Sleep(100);
                int p = ((i + 1) * 100) / (imena.Length);
                worker.ReportProgress(p, i);
                Console.WriteLine("POSTO JE " + p);
                Console.WriteLine("STAVKA JE " + i);
                Thread.Sleep(100);
                int provjera = pomocna.postojiDokument(idKolekcije, fajl, konekcija);
                if (provjera == 2)
                {
                    return;
                }
                if (provjera == 0)
                {
                    if (pomocna.sacuvajDokument(idKolekcije, fajl, tip, velicine[i], konekcija) == 0)
                    {
                        radSaFajlovima.kopirajFajl(fajl + "." + tip, putanje[i], username, ime);
                    }
                }
                else
                {
                    if (provjera == 1)
                    {
                        if (pomocna.sacuvajDokument(idKolekcije, fajl, tip, velicine[i], konekcija) == 0)
                        {
                            radSaFajlovima.izbrisiFajl(fajl + "." + tip, username, ime);
                            radSaFajlovima.kopirajFajl(fajl + "." + tip, putanje[i], username, ime);
                        }
                    }
                }
                i += 1;
            }
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

    }
}
