namespace Varastonhallinta
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string? input;
            bool quit = false;

            while (quit == false)
            {
                Console.WriteLine("1 - Lisää uusi tuote");
                Console.WriteLine("2 - Poista tuote");
                Console.WriteLine("3 - Tulosta eri tuotteiden määrät");
                Console.WriteLine("4 - Tulosta kaikki tuotteet");
                Console.WriteLine("5 - Muokkaa tuotenimeä");
                Console.WriteLine("0 - Lopeta sovellus");
                Console.Write("Valitse mitä haluat tehdä: ");
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        // Tuotteen lisäys
                        Console.Clear();

                        // Uuden tuotteen muuntajat
                        string tuotenimi;
                        int tuotehinta;
                        int varastonsaldo;

                        Console.Write("Syötä uuden tuotteen nimi: ");
                        tuotenimi = Console.ReadLine();
                        Console.Write("Syötä uuden tuotteen hinta: ");
                        int.TryParse(Console.ReadLine(), out tuotehinta);
                        Console.Write("Syötä uuden tuotteen varastosaldo: ");
                        int.TryParse(Console.ReadLine(), out varastonsaldo);

                        if (AddProductToDB(tuotenimi, tuotehinta, varastonsaldo))
                        {
                            Console.WriteLine("Tuotteen lisäys onnistui");
                        }
                        else 
                        { 
                            Console.WriteLine("Tuotteen lisäys epäonnistui");
                        }
                        break;
                    case "2":
                        // Tuotteen poisto
                        RemoveProductFromDB();
                        break;
                    case "3":
                        // Tuotteiden määrän tulostus
                        QueryProductCount();
                        break;
                    case "4":
                        // Kaikkien tuotteiden tulostus
                        QueryAllProducts();
                        break;
                    case "5":
                        // Tuotteen nimen muokkaus
                        ChangeProductNameInDB();
                        break;
                    case "0":
                        // Sovelluksen lopetus
                        quit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }

            bool AddProductToDB(string newTuoteNimi, int newTuotteenHinta, int newVarastonSaldo)
            {
                using Varastonhallinta varastonhallinta = new(); // using varmistaa ettätietokantayhteys hävitetää käytön jälkeen
                IQueryable<Tuote> kaikkiTuotteet = varastonhallinta.Tuotteet;


                Tuote tuotteet = new()
                {
                    Id = kaikkiTuotteet.Count()+1,
                    Tuotenimi = newTuoteNimi,
                    Tuotteenhinta = newTuotteenHinta,
                    Varastosaldo = newVarastonSaldo
                };
                varastonhallinta.Tuotteet?.Add(tuotteet);
                int affected = varastonhallinta.SaveChanges();
                return affected == 1;
            }

            void RemoveProductFromDB()
            {

            }

            void QueryProductCount()
            {
                using Varastonhallinta varastonhallinta = new();
                Console.WriteLine("Tuotteet ja niiden määrät:");
                IQueryable<Tuote>? kaikkiTuotteet = varastonhallinta.Tuotteet;

                // Jos hakutulos on tyhjä
                if (kaikkiTuotteet is null)
                {
                    Console.WriteLine("Yhtään tuotetta ei ole vielä lisätty tietokantaan");
                    return;
                }

                foreach (Tuote tuote in kaikkiTuotteet)
                {
                    Console.WriteLine("Tuotetta: " + tuote.Tuotenimi + " on " + tuote.Varastosaldo + " varastossa");
                }
            }
             
            void QueryAllProducts() 
            {
                using Varastonhallinta varastonhallinta = new();
                Console.WriteLine("Varastossa on: ");
                IQueryable<Tuote>? kaikkiTuottteet = varastonhallinta.Tuotteet;

                // Jos hakutulos on tyhjä
                if (kaikkiTuottteet is null)
                {
                    Console.WriteLine("Yhtään tuotetta ei ole vielä lisätty tietokantaan");
                }

                foreach (Tuote tuote in kaikkiTuottteet)
                {
                    Console.WriteLine(tuote.Tuotenimi);
                }
            }

            void ChangeProductNameInDB()
            {

            }
        }
    }
}