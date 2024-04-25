using System;
namespace Könyvtár
{
	public class NoBooksToBorrowException : Exception { }

	public class Személy
	{
		public string név { get; }

		public List<Kölcsönzés> kölcsönzések = new List<Kölcsönzés>();

		public Személy(string név)
		{
			this.név = név;
		}

        public void Beiratkozik(Library k)
        {
			k.Beirat(this);
        }

		public void Kölcsönöz(Library k, List<string> könyvek)
		{
			Kölcsönzés kivettKönyvek = k.Kiad(this, könyvek);

			try
			{

				if(!(kivettKönyvek.könyvek.Count() > 0))
				{
					throw new NoBooksToBorrowException(); // nem tudott semmit kikölcsönözni
				}
    
	            kölcsönzések.Add(kivettKönyvek);
            }
			catch (NoBooksToBorrowException) { Console.WriteLine($"{név}, please look around for other books."); }
        }


		public void Visszavisz(Library k, string title)
		{
			for (int i = 0; i < kölcsönzések.Count(); i++)
			{
				for (int j = 0; j < kölcsönzések[i].könyvek.Count; j++)
				{

					if (kölcsönzések[i].könyvek[j].title == title)
					{
							
                        k.Visszavesz(this, kölcsönzések[i].könyvek[j]);

						kölcsönzések[i].RemoveBook(kölcsönzések[i].könyvek[j]);
						
                    }
				}
			}
		}


		public int PotDijCalc(string date)
		{
			int potdij = 0;

			foreach(Kölcsönzés kölcs in kölcsönzések)
			{
				for (int i = 0; i < kölcs.könyvek.Count(); i++)
				{
					potdij += kölcs.könyvek[i].SurTax(date);
				}
			}

			return potdij;
		}


		



		// Print (osztáyldiagramban nem jelölt)
        public override string ToString()
        {
			return $"{név}";
        }
    }
}