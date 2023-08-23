namespace Tietokantaharjoitus4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input;
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
                        // Tuotteen lisäys funktio tähän
                        break;
                    case "2":
                        // Tuotteen poisto funktio tähän
                        break;
                    case "3":
                        // Tuotteiden määrän tulostus funktio tähän
                        break;
                    case "4":
                        // Kaikkien tuotteiden tulostus tähän
                        break;
                    case "5":
                        // Tuotenimen muokkaus tähän
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
        }
    }
}