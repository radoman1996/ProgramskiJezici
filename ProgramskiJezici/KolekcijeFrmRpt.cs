using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgramskiJezici
{
    public partial class KolekcijeFrmRpt : Form
    {

        String jmbg;

        public KolekcijeFrmRpt(String jmbg)
        {
            InitializeComponent();
            this.jmbg = jmbg;
        }

        private void KolekcijeFrmRpt_Load(object sender, EventArgs e)
        {
            if (this.jmbg == "")
            {
                SveKolekcijeRpt skr = new SveKolekcijeRpt();
                this.crystalReportViewer1.ReportSource = skr;
            }
            else
            {
                SveKolekcijeKorisnikRpt skkr = new SveKolekcijeKorisnikRpt();
                skkr.SetParameterValue("jmbg", this.jmbg);
                this.crystalReportViewer1.ReportSource = skkr;
            }
        }
    }
}
