using System;
namespace Könyvtár
{
	public abstract class Könyv
	{
		public string title { get; }
        protected string author;
		protected string publisher;
        protected int ISBN;
		protected int pages;

        public bool kolcsonzott { get; set; }


        public DateTime lejáratiDátum { get; set; }

        protected Példányszám példányszám;
        public int id { get; set; } // ha bekerül a könyvtárba, akkor kap ID-t


        protected Könyv(string title, string author, string publisher, int ISBN, int pages, string peldanyszam)
		{
            this.title = title;
            this.author = author;
            this.publisher = publisher;
            this.ISBN = ISBN;
            this.pages = pages;
            this.kolcsonzott = false;
            

            if(peldanyszam == "sok")
            {
                this.példányszám = Sok.Instance();
            }

            if(peldanyszam == "kevés")
            {
               this.példányszám = Ritka.Instance();
            }

            

		}

		public abstract int SurTax(string date);

		public int getDiffDays(DateTime visszavitel)
        {
            return (visszavitel - lejáratiDátum).Days;   
        }

        public override string ToString()
        {
            return $"{title}, written by {author}, (ID: {id})";
        }

    }


	public class Natural : Könyv
	{
        public Natural(string title, string author, string publisher, int ISBN, int pages, string p) : base(title, author, publisher, ISBN, pages, p) { }
		

        public override int SurTax(string visszaDate)
        {
            return getDiffDays(DateTime.Parse(visszaDate)) * (példányszám.Érték(this));
        }
    }

    public class Fiction : Könyv
    {
        public Fiction(string title, string author, string publisher, int ISBN, int pages, string p) : base(title, author, publisher, ISBN, pages, p)
        {

        }

        public override int SurTax(string visszaDate)
        {
            return getDiffDays(DateTime.Parse(visszaDate)) * (példányszám.Érték(this));
        }
    }

    public class Youth : Könyv
    {
        public Youth(string title, string author, string publisher, int ISBN, int pages, string p) : base(title, author, publisher, ISBN, pages, p)
        {

        }

        public override int SurTax(string visszaDate)
        {
            return getDiffDays(DateTime.Parse(visszaDate)) * (példányszám.Érték(this));
        }
    }   
}