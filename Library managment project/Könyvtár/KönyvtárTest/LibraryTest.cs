using System.Runtime.InteropServices;
using Könyvtár;
namespace LibraryTest;

[TestClass]
public class LibraryTest
{
    

    [TestMethod]
    public void NewSzemély()
    {
        Személy személy = new Személy("Sophia");

        Assert.AreEqual(személy.név, "Sophia");
    }

    [TestMethod]
    public void LibraryWithPeople()
    {
        Library library = new Library();
        Személy személy = new Személy("Sophia");
        személy.Beiratkozik(library);

        Assert.AreEqual(library.tagok.Count(), 1);
    }

    [TestMethod]
    public void newBook()
    {
        Natural book = new Natural("The Hidden World", "Jane Smith", "XYZ Publishing", 12345, 300, "sok");

        
        Assert.IsTrue(book.title == "The Hidden World" && !book.kolcsonzott);

    }

    [TestMethod]
    public void LibraryWithBooks()
    {
        Library library = new Library();
        Natural book1 = new Natural("book1", "Jane Smith", "XYZ Publishing", 12345, 300, "sok");
        Youth book2 = new Youth("book2", "Jane Smith", "XYZ Publishing", 12345, 300, "sok");

        library.Feljegyez(book1);
        library.Feljegyez(book2);

        Assert.AreEqual(library.könyvek.Count(), 2);

    }


    [TestMethod]
    public void BorrowingABook()
    {
        Library library = new Library();
        Natural book1 = new Natural("book1", "Jane Smith", "XYZ Publishing", 12345, 300, "sok");
        library.Feljegyez(book1);

        

        List<string> booksToGet = new List<string>
        {
            "book1"
        };

        Személy sz = new Személy("Arien");
        sz.Beiratkozik(library);

        sz.Kölcsönöz(library, booksToGet);

        Assert.AreEqual(library.könyvek[0].kolcsonzott, true);
        Assert.AreEqual(sz.kölcsönzések.Count(), 1);


    }

    [TestMethod]
    public void BringingBackABook()
    {
        Library library = new Library();
        Natural book1 = new Natural("book1", "Jane Smith", "XYZ Publishing", 12345, 300, "sok");
        library.Feljegyez(book1);



        List<string> booksToGet = new List<string>
        {
            "book1"
        };

        Személy sz = new Személy("Arien");
        sz.Beiratkozik(library);

        sz.Kölcsönöz(library, booksToGet);

        Assert.AreEqual(library.könyvek[0].kolcsonzott, true);
        Assert.AreEqual(sz.kölcsönzések.Count(), 1);

        sz.Visszavisz(library, "book1");

        Assert.AreEqual(library.könyvek[0].kolcsonzott, false);
        Assert.AreEqual(sz.kölcsönzések[0].könyvek.Count(), 0);


    }

}
