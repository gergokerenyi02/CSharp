using System;
namespace Könyvtár
{
	public class Kölcsönzés
	{
		public DateTime lejáratiDátum { get; set; }
		public List<Könyv> könyvek { get; }


        public Kölcsönzés(List<Könyv> könyvek)
		{
			this.könyvek = könyvek;
		}


        public Kölcsönzés(List<Könyv> könyvek, DateTime lejáratiDátum)
		{
			this.lejáratiDátum = lejáratiDátum;
            

            foreach (Könyv k in könyvek)
			{
				k.lejáratiDátum = lejáratiDátum;
			}

            this.könyvek = könyvek;
        }

		public void RemoveBook(Könyv k)
		{
			this.könyvek.Remove(k);
		}

	}
}