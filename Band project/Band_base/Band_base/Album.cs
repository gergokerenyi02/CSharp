using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Band_base
{
    public class Album
    {
        public string cim;
        public int megjelenes;
        public int pontszam;
        public int Hossz { get; private set; }

        private List<szam> szamok = new List<szam>();


        public struct szam
        {
            public string cim;
            public int hossz;



            public szam(string cim, int hossz)
            {
                this.cim = cim;
                this.hossz = hossz;
            }

        }

        public Album(string cim, int megjelenes, int pontszam)
        {
            this.cim = cim;
            this.megjelenes= megjelenes;
            this.pontszam= pontszam;
            Hossz = 0;

        }

        public void Add(szam szam)
        {
            szamok.Add(szam);
            Hossz += szam.hossz;
        }

        public szam MinSzam()
        {
            szam minSzam = szamok[0];

            foreach (szam n in szamok)
            {
                if(n.hossz < minSzam.hossz)
                {
                    minSzam = n;
                }
            }

            return minSzam;
            
            
            

              
        }


    }
}
