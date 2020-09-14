using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramskiJezici
{
    class OverrideClass
    {
        public String jmbg;
        public String ime;

        public OverrideClass(String a, String b)
        {
            this.jmbg = a;
            this.ime = b;
        }

        public override string ToString()
        {
            return this.ime;
        }
    }
}
