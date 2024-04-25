using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFile;

namespace RPG_3
{
    class Jatek
    {

        List<Karakter> parti = new List<Karakter>();
        List<Ellenfel> ellenfelek = new List<Ellenfel>();

        public void Beolvas(string partiFileName, string ellenfelekFileName)
        {
            parti.Clear();
            ellenfelek.Clear();
            
            TextFileReader reader = new(partiFileName);
            char[] separators = { ' ', '\t' };
            string[] tokens;
            while (reader.ReadLine(out string line))
            {
                tokens = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                Karakter ujKarakter = null;
                switch(tokens[1])
                {
                    case "harcos":
                        ujKarakter = new Harcos(tokens[0], int.Parse(tokens[2]), int.Parse(tokens[3]), int.Parse(tokens[4]));
                        break;
                    case "kósza":
                        ujKarakter = new Kosza(tokens[0], int.Parse(tokens[2]), int.Parse(tokens[3]), int.Parse(tokens[4]));
                        break;
                    case "varázsló":
                        ujKarakter = new Varazslo(tokens[0], int.Parse(tokens[2]), int.Parse(tokens[3]), int.Parse(tokens[4]));
                        break;
                    default: break;
                }

                parti.Add(ujKarakter);
            }

            reader = new(ellenfelekFileName);
            while (reader.ReadLine(out string line))
            {
                tokens = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                // biztos ork
                ellenfelek.Add(new Ork(int.Parse(tokens[1]), int.Parse(tokens[2]), int.Parse(tokens[3])));
            }

        }

        public (string, List<string>) Lejatszik()
        {
            while(!(parti.Count() == 0 || ellenfelek.Count() == 0))
            {
                kor();
            }

            if(parti.Count() != 0)
            {
                // karakterek visszadása

                List<string> players = new List<string>();

                foreach(Karakter k in parti)
                {

                    players.Add(k.getName());
                }

                return ("nyert", players);
                

            } else
            {
                List<string> emptyList = new List<string>();

                return ("vesztett", emptyList);
            }

        }


        public void kor()
        {
            foreach(Karakter karakter in parti)
            {
                karakter.tamad(parti, ellenfelek);

            }

            HPSzuresE();

            if(ellenfelek.Count() == 0)
            {
                return;
            } else
            {
                ellenfelek[0].tamad(parti);
            }

            HPSzuresP();

        }

        public void HPSzuresE()
        {
            List<Ellenfel> ujEllenfelek = new List<Ellenfel>();

            foreach(Ellenfel ellenfel in ellenfelek)
            {
                if(ellenfel.eletbenVan())
                {
                    ujEllenfelek.Add(ellenfel);
                }
            }

            ellenfelek = ujEllenfelek;
        }
        public void HPSzuresP()
        {
            List<Karakter> ujParti = new List<Karakter>();

            foreach (Karakter karakter in parti)
            {
                if (karakter.eletbenVan())
                {
                    ujParti.Add(karakter);
                }
            }

            parti = ujParti;

        }
    } 
}
