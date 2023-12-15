using ModelLibrary;

class Program
{
    static void Main()
    {
        Verwaltung verwaltung = new Verwaltung();

        Kunde kunde1 = new Kunde("001", "Peter", verwaltung);
        Kunde kunde2 = new Kunde("002", "John", verwaltung);

        verwaltung.printKundenList();

        Artikel Milch = new Artikel("001", "Milch", new TimeSpan(3,0,0,0), 50, verwaltung);
        Milch.add(20, new DateTime(2023,12,01));
        Milch.add(10, new DateTime(2023,12,15));

        Artikel Eier = new Artikel("002", "Eier", new TimeSpan(7, 0, 0, 0), 50, verwaltung);
        Eier.add(20, new DateTime(2023,12,10));
        Eier.add(30, new DateTime(2023,12,15));

        verwaltung.printArtikelList();

        Milch.print();
        Eier.print();

        verwaltung.printAbgelaufeneArtikel();

        verwaltung.deleteAllAbgelaufeneArtikel();

        verwaltung.printArtikelList();
    }
}
