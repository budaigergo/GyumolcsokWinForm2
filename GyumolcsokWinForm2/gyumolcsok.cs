using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GyumolcsokWinForm2
{
    internal class gyumolcsok
    {
        int id;
        string nev;
        int egysegar;
        int mennyiseg;

        public int Id { get => id; set => id = value; }
        public string Nev { get => nev; set => nev = value; }
        public int Egysegar { get => egysegar; set => egysegar = value; }
        public int Mennyiseg { get => mennyiseg; set => mennyiseg = value; }

        public gyumolcsok(int id, string nev, int egysegar, int mennyiseg)
        {
            this.id = id;
            this.nev = nev;
            this.egysegar = egysegar;
            this.mennyiseg = mennyiseg;
        }

        public override string ToString()
        {
            return nev;
        }
    }
}
