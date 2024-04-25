using TextFile;

namespace Könyvtár;
class Program
{
    static void Main(string[] args)
    {
        
        Library könyvtár = new Library();

        Infile f_people = new Infile("people.txt");
        Infile f_books = new Infile("books.txt");
        Infile f_borrow = new Infile("borrow.txt");


        //Könyvtár felpopulálása könyvekkel
        while(f_books.ReadBook(out Könyv könyv))
        {
            könyvtár.Feljegyez(könyv);
        }


        //Könyvtár felpopulálása tagokkal
        while(f_people.ReadPeople(out Személy sz))
        {
            sz.Beiratkozik(könyvtár);
        }


        //Könyvtári kölcsönzések
        while(f_borrow.ReadBorrow(out string name, out List<string> booksToBorrow, out List<string> booksToGiveBack))
        {
            bool l = könyvtár.Search(name, out Személy sz); // Ellenőrzés, hogy tag e egyáltalán, máskülönben felesleges elindítani a kölcsönzést

            if(l)
            {
                if(booksToBorrow.Count() != 0)
                {
                    sz.Kölcsönöz(könyvtár, booksToBorrow);
                }
                
                if(booksToGiveBack.Count() != 0)
                {
                    for (int i = 0; i < booksToGiveBack.Count(); i++)
                    {
                        sz.Visszavisz(könyvtár, booksToGiveBack[i]);
                    }
                }   
            }
        }


        könyvtár.Search("Sophia", out Személy személy);

        int tobePaidBySophia = személy.PotDijCalc("2023.07.3");

        Console.WriteLine(tobePaidBySophia);


    }
}

