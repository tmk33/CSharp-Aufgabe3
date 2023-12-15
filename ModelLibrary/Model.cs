using System.ComponentModel;

namespace ModelLibrary
{
    public class Kunde
    {
        public string ID { get; set; }
        public string Name { get; set; }
        private Verwaltung verwaltung;
        //public List<Bestellung> Bestellungen { get; set; }
        public Kunde(string id, string name, Verwaltung verwaltung)
        {
            ID = id;
            Name = name;
            this.verwaltung = verwaltung;

            // add this new Kunde object into Kunden list in Verwaltung
            verwaltung.addKunde(this);
        }

        public void delete()
        {
            this.ID = null;
            this.Name = null;

            //delete this Kunde Object in Kunden list in Verwaltung
            verwaltung.deleteKunde(this);
        }
    }

    public class Verwaltung
    {
        public List<Kunde> Kunden { get; set; } = new List<Kunde>();
        public List<Artikel> Artikel { get; set; } = new List<Artikel>();

        public void addKunde(Kunde k)
        {
            Kunden.Add(k);
        }

        public void deleteKunde(Kunde k)
        {
            Kunden.Remove(k);
        }

        public void addArtikel(Artikel a)
        {
            Artikel.Add(a);
        }

        public void printAbgelaufeneArtikel()
        {
            foreach (Artikel a in this.Artikel)
            {
                Console.WriteLine($"Id: {a.Artikelnummer}, Bezeichnung: {a.Bezeichnung}");
                Console.WriteLine($"Abgelaufene: {a.getAbgelaufeneMenge()} Menge");
                Console.WriteLine();
            }
        }

        // delete all expired goods until TODAY!
        public void deleteAllAbgelaufeneArtikel()
        {
            foreach (Artikel a in this.Artikel)
            {
                a.deleteAbgelaufeneMengeBisHeute();
            }

            Console.WriteLine($"Alle abgelaufene Artikel wurden geloscht!");
        }

        public void printKundenList()
        {
            Console.WriteLine("------------------Kunden Liste------------------");
            foreach (Kunde k in this.Kunden)
            {
                Console.WriteLine($"Id: {k.ID}, Name: {k.Name}");
            }
            Console.WriteLine();
        }

        public void printArtikelList()
        {
            Console.WriteLine("------------------Artikel Liste------------------");
            foreach (Artikel a in this.Artikel)
            {
                Console.WriteLine($"Id: {a.Artikelnummer}, Bezeichnung: {a.Bezeichnung}, Menge: {a.AktuelleMenge}");
            }
            Console.WriteLine();
        }

    }

    public class Artikel
    {
        public string Artikelnummer { get; set; }
        public string Bezeichnung { get; set; }
        public TimeSpan Ablaufzeit { get; set; }

        public Dictionary<DateTime, int> Verfallsdatum { get; set; } = new Dictionary<DateTime, int>();
        public int MaximalMenge { get; set; }
        public int AktuelleMenge { get; set; } = 0;
        private Verwaltung verwaltung;

        public Artikel(string artikelnummer, string bezeichnung, TimeSpan ablaufzeit, int max, Verwaltung verwaltung)
        {
            Artikelnummer = artikelnummer;
            Bezeichnung = bezeichnung;
            Ablaufzeit = ablaufzeit;
            MaximalMenge = max;
            this.verwaltung = verwaltung;

            // add this new Artikel object into Artikel list in Verwaltung
            verwaltung.addArtikel(this);
        }

        public void add(int menge, DateTime geliefertDatum)
        {
            this.Verfallsdatum.Add(geliefertDatum + this.Ablaufzeit, menge);
            this.AktuelleMenge += menge;
        }

        public int getAbgelaufeneMenge() 
        {
            int result = 0;
            foreach (var v in this.Verfallsdatum)
            {
                result = v.Key <= DateTime.Today ? result + v.Value : result;
            }

            return result;
        }

        public void deleteAbgelaufeneMengeBisHeute()
        {
            foreach (var v in this.Verfallsdatum)
            {
                if (v.Key <= DateTime.Today)
                {
                    this.AktuelleMenge -= v.Value;
                    this.Verfallsdatum.Remove(v.Key);
                }
            }

        }

        public void print() 
        {
            Console.WriteLine("------------------Artikel Info------------------");
            Console.WriteLine($"Id: {this.Artikelnummer}, Bezeichnung: {this.Bezeichnung}");
            Console.WriteLine($"Aktuelle Gesamtmenge: {this.AktuelleMenge}");
            foreach (var v in this.Verfallsdatum)
            {
                Console.WriteLine($"Verfallsdatum {v.Key.ToString("dd/MM/yyyy")}: Menge {v.Value}");
            }
            Console.WriteLine();
        }
    }
}
