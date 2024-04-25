
namespace RPG_3
{
    class Program
    {
        public static void Main()
        {
            Jatek jatek = new();
            jatek.Beolvas("karakterek.txt", "ellensegek.txt");
            string eredmeny;
            List<string> tulelok;
            (eredmeny, tulelok) = jatek.Lejatszik();
            Console.WriteLine(eredmeny);
            if (tulelok.Count > 0)
            {
                Console.Write(tulelok[0]);
                for (int i = 1; i < tulelok.Count; i++)
                {
                    Console.Write($", {tulelok[i]}");
                }
                Console.WriteLine();
            }
        }
    }
}