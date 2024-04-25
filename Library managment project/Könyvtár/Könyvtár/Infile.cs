using System;
using TextFile;

namespace Könyvtár
{
	public class Infile
	{
        private TextFileReader reader;

        public Infile(string filename)
        {
            reader = new TextFileReader(filename);
        }

        public bool ReadBook(out Könyv könyv)
        {
            könyv = null;
            bool l = reader.ReadLine(out string line);
            if (l)
            {
                
                char[] separators = { '\t' };
                string[] tokens = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                string type = tokens[0];
                string title = tokens[1];
                string author = tokens[2];
                string publisher = tokens[3];
                int ISBN = int.Parse(tokens[4]);
                int pages = int.Parse(tokens[5]);

                
                string p = tokens[6];

                switch (type)
                {
                    case "Youth":
                        könyv = new Youth(title, author, publisher, ISBN, pages, p);
                        break;
                    case "Fiction":
                        könyv = new Fiction(title, author, publisher, ISBN, pages, p);
                        break;
                    case "Natural":
                        könyv = new Natural(title, author, publisher, ISBN, pages, p);
                        break;
                }


            }

            
            return l;
        }

        public bool ReadPeople(out Személy személy)
        {
            személy = null;
            bool l = reader.ReadLine(out string line);
            if (l)
            {

                char[] separators = { '\t' };
                string[] tokens = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                string name = tokens[0];

                személy = new Személy(name);

            }


            return l;
        }

        public bool ReadBorrow(out string name, out List<string> booksToBorrow, out List<string> booksToGiveBack)
        {
            name = "";
            booksToBorrow = new List<string>();
            booksToGiveBack = new List<string>();

            bool l = reader.ReadLine(out string line);
            if (l)
            {
                char[] separators = { ',' };
                string[] tokens = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                name = tokens[0];

                bool terminate = false;

                string method = tokens[1];

                if(method == "K")
                { 
                    for (int i = 2; i < tokens.Length && !terminate; i++)
                    {
                        booksToBorrow.Add(tokens[i]);
                    
                        if (tokens[i] == "V")
                        {
                            terminate = true;

                            for (int j = i+1; j < tokens.Length; j++)
                            {
                                booksToGiveBack.Add(tokens[i]);
                            }
                        }
                    
                    }
                }

                if(method == "V")
                {
                    for (int i = 2; i < tokens.Length && !terminate; i++)
                    {
                        booksToGiveBack.Add(tokens[i]);
                        

                        if (tokens[i] == "K")
                        {
                            terminate = true;

                            for (int j = i + 1; j < tokens.Length; j++)
                            {
                                booksToBorrow.Add(tokens[i]);
                            }
                        }

                    }

                }
            }

            return l;
        }
    }
}
