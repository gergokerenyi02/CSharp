using System;
namespace Könyvtár
{
	public class AlreadyMemberException : Exception { }
    public class NotMemberException : Exception { }
    public class TooManyBooksException : Exception { }

    public class Library
	{

		const int borrowTimeInMonths = 1;					 // Hány hónapra lehessen kikölcsönözni egy könyvet.
		
		public List<Személy> tagok = new List<Személy>();  // Könyvtár által nyilvántartott személyek
		public List<Könyv> könyvek = new List<Könyv>();    //  Könyvtárba regisztrált könyvek

		public Library()
		{

		}

		public void Beirat(Személy sz)
		{
			try
			{
				
                if (tagok.Contains(sz))
                {
                    throw new AlreadyMemberException();
                }
                else
                {
                    tagok.Add(sz);
                }
            }
			catch (AlreadyMemberException) { Console.WriteLine("This person is already registered in the library."); }
			
		}

		public void Feljegyez(Könyv k)
		{
			k.id = könyvek.Count() + 1;										// Egyedi ID létrehozása
            k.kolcsonzott = false;
			könyvek.Add(k);
		}

		public Kölcsönzés Kiad(Személy sz, List<string> könyvek)
		{
            List<Könyv> kölcsönzöttKönyvek = new List<Könyv>();				// Kölcsönzés összeállítása
            DateTime currentDate = DateTime.Today;							// Kölcsönzés ideje
            DateTime lejárat = currentDate.AddMonths(borrowTimeInMonths);

            try
			{
				// Tesztesetekhez jó, de fájlból való beolvasás során már korábban eldől a tag ellenőrzés a Search függvénnyel
				// ha nem azzal csinálnám nem ugyan annak érzékelné a 2 tagot, hiába egyező a név
                if (!tagok.Contains(sz))
				{
					throw new NotMemberException();
				}
				

				// Több mint 5 könyv kölcsönzése nem megengedett
                if (könyvek.Count() > 5)
                {
                    throw new TooManyBooksException();
                }
            }
			catch (TooManyBooksException) { Console.WriteLine("You can't borrow more than 5 books at the same time!"); }
            catch (NotMemberException) { Console.WriteLine($"{sz} is not a member of this library"); }



            for (int i = 0; i < könyvek.Count(); i++)
			{
				for (int j = 0; j < this.könyvek.Count(); j++)
				{
                    if (könyvek[i] == this.könyvek[j].title)
                    {
						if (this.könyvek[j].kolcsonzott == false) //Hiába van a könyvtárban feljegyezve a könyv, ellenőrzőm, hogy más nem-e vette már ki
						{
                            kölcsönzöttKönyvek.Add(this.könyvek[j]);

                            this.könyvek[j].kolcsonzott = true;
                        } else
						{
							Console.WriteLine(this.könyvek[j] + " is already borrowed.");
						}
                    }
                }
			}
		
            Kölcsönzés finalBorrowedBooks = new Kölcsönzés(kölcsönzöttKönyvek, lejárat);
        
            return finalBorrowedBooks;

        }


		// Visszaállítom a kölcsönzött booleant false-ra, így más kitudja mostmár kölcsönözni
		public void Visszavesz(Személy sz, Könyv k)
		{
			for (int i = 0; i < könyvek.Count(); i++)
			{
				if (könyvek[i].title == k.title)
				{
					könyvek[i].kolcsonzott = false;

					break;
				}

			}

		}


		// Fájlbeolvasáshoz létrehozott metódus, hogy a megfelelő személyt megkapjam
        public bool Search(string name, out Személy személy)
        {
            személy = null;


			try
			{
			
				bool l = false;

				for (int i = 0; i < tagok.Count() && !l; i++)
				{
					if (tagok[i].név == name)
					{
						l = true;
						személy = tagok[i];
					}
				}

				if (!l)
				{
					throw new NotMemberException();
				}

				return true;

            }
			catch (NotMemberException) { Console.WriteLine($"{name} is not signed to the library."); return false; }

			
			

			



        }

    }
}