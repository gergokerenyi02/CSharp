using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFile;

namespace Band_base
{
    public class Infile
    {
        private TextFileReader reader = new TextFileReader("input.txt");




        public bool Read(out Album album)
        {
            album = null;

            bool l = reader.ReadLine(out string line);
            
            if (l)
            {
                char[] seperators = { ' ' };
                string[] tokens = line.Split(seperators, StringSplitOptions.RemoveEmptyEntries);

                album = new Album(tokens[0], int.Parse(tokens[1]), int.Parse(tokens[2]));

                for (int i = 3; i < tokens.Length; i+=2)
                {
                    album.Add(new Album.szam(tokens[i], int.Parse(tokens[i + 1])));
                }

            }

            return l;
           


        }


    }
}
