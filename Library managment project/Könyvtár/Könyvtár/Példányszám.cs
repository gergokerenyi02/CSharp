using System;
namespace Könyvtár
{
	public interface Példányszám
	{
		public int Érték(Natural k);
        public int Érték(Fiction k);
        public int Érték(Youth k);
    }

    public class Ritka : Példányszám
    {
        private static Ritka instance;
        private Ritka() { }
        public static Ritka Instance()
        {
            instance ??= new Ritka();
            return instance;
        }

        public int Érték(Natural k) {return 100;}
        public int Érték(Fiction k) {return 50;}
        public int Érték(Youth k) {return 30;}
    }

    public class Sok : Példányszám
    {
        private static Sok instance;
        private Sok() { }
        public static Sok Instance()
        {
            instance ??= new Sok();
            return instance;
        }

        public int Érték(Natural k) {return 20;}

        public int Érték(Fiction k) {return 10;}

        public int Érték(Youth k) {return 20;}
    }
}