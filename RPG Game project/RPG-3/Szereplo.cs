using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// zöld kör public
// teljes rombusz private
// üres rombusz protected

namespace RPG_3
{
    public abstract class Szereplo
    {
        protected int HP;
        protected int vedelem;
        protected int sebzes;


        public Szereplo(int HP, int vedelem, int sebzes)
        {
            this.HP = HP;
            this.vedelem = vedelem;
            this.sebzes = sebzes;
        }

        public bool eletbenVan()
        {
            return HP >= 0;
        }

        public void sebzodik(Szereplo tamado)
        {
            int tenylegesSebzes = tamado.sebzes - vedelem;

            if(tenylegesSebzes > 0)
            {
                HP = HP - tenylegesSebzes;
            }
        }


    }


    public abstract class Ellenfel : Szereplo
    {
        public Ellenfel(int HP, int vedelem, int sebzes) : base(HP, vedelem, sebzes) { }

        public abstract void tamad(List<Karakter> parti);
    }

    public abstract class Karakter : Szereplo
    {
        protected string nev;

        public Karakter(string nev, int HP, int vedelem, int sebzes) : base(HP, vedelem, sebzes)
        {
            this.nev = nev;   
        }


        public abstract void tamad(List<Karakter> parti, List<Ellenfel> ellenfelek);

        public string getName()
        {
            return nev;
        }

    }


    public class Harcos : Karakter
    {
        public Harcos(string nev, int HP, int vedelem, int sebzes) : base(nev, HP, vedelem, sebzes) { }

        public override void tamad(List<Karakter> parti, List<Ellenfel> ellenfelek)
        {
            int sajatIndex = 0;

            for (int i = 0; i < parti.Count(); i++)
            {
                if (parti[i] == this)
                {
                    sajatIndex = i;
                }
            }

            if (sajatIndex != 0)
            {
                return;
            }
            else
            {
                ellenfelek[0].sebzodik(this);
            }

        }
    }

    public class Kosza : Karakter
    {
        public Kosza(string nev, int HP, int vedelem, int sebzes) : base(nev, HP, vedelem, sebzes) { }

        public override void tamad(List<Karakter> parti, List<Ellenfel> ellenfelek)
        {
            ellenfelek[0].sebzodik(this);
        }
    }

    public class Varazslo : Karakter
    {
        public Varazslo(string nev, int HP, int vedelem, int sebzes) : base(nev, HP, vedelem, sebzes) { }


        public override void tamad(List<Karakter> parti, List<Ellenfel> ellenfelek)
        {
            ellenfelek[0].sebzodik(this);

            if (ellenfelek.Count() > 1)
            {
                ellenfelek[1].sebzodik(this);
            }
        }
    }

    public class Ork : Ellenfel
    {

        public Ork(int HP, int vedelem, int sebzes) : base(HP, vedelem, sebzes) { }
        
        public override void tamad(List<Karakter> parti)
        {
            parti[0].sebzodik(this);
        }
    }
}
