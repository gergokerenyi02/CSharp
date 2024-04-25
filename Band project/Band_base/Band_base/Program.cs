using System;
using System.ComponentModel.DataAnnotations;
using TextFile;

namespace Band_base
{
    class Program
    {
        static void Main()
        {
            try
            {
                Infile f = new Infile();
                Album lastAlbum = new Album("", 0, 0);

                while (f.Read(out Album album))
                {
                    if(album.megjelenes <= 2015)
                    {


                        if(album.MinSzam().hossz >= 40 && album.MinSzam().hossz <= 60)
                        {
                            Console.WriteLine(album.MinSzam().cim);
                        }

                        
                    }

                    if (album.megjelenes > 2015)
                    {

                        lastAlbum = album;
                        break;
                    }
                }


                int db = 0;
                Album MaxAlbum = new Album("", 0, 0);


                if (lastAlbum.pontszam >= 7)
                {
                    db++;
                }

                if (lastAlbum.Hossz > MaxAlbum.Hossz)
                {
                    MaxAlbum = lastAlbum;

                }


                while (f.Read(out Album album))
                {
                    if(album.Hossz > MaxAlbum.Hossz)
                    {
                        MaxAlbum = album;
                    }

                    if(album.pontszam >= 7)
                    {
                        db++;
                    }

                }

                Console.WriteLine(MaxAlbum.cim);
                Console.WriteLine(db);


            }
            catch (FileNotFoundException) { Console.WriteLine("File doesn't exist."); }

            
        }
    }
}
