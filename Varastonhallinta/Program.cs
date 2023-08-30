using Microsoft.EntityFrameworkCore.Update;
using System;
using System.ComponentModel.Design;
using System.Reflection.Metadata.Ecma335;

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
                Console.WriteLine();
                Console.WriteLine("1 - Lisää uusi tuote");
                Console.WriteLine("2 - Poista tuote");
                Console.WriteLine("3 - Tulosta eri tuotteiden määrät");
                Console.WriteLine("4 - Tulosta kaikki tuotteet");
                Console.WriteLine("5 - Muokkaa tuotenimeä");
                Console.WriteLine("0 - Lopeta sovellus");
                Console.Write("Valitse mitä haluat tehdä: ");
                input = Console.ReadLine();
                Console.Clear();

                switch (input)
                {
                    case "1":
                        // Tuotteen lisäys

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
                        int tuotteenIdp;

                        Console.WriteLine("Syötä 0 jos haluat poistua");
                        Console.Write("Syötä tuotteen id, jonka haluat poistaa tietokannasta: ");
                        int.TryParse(Console.ReadLine(), out tuotteenIdp);
                        if (tuotteenIdp == 0)
                        {
                            break;
                        }

                        if (RemoveProductFromDB(tuotteenIdp))
                        {
                            Console.WriteLine("Tuotteen poisto onnistui");
                        }
                        else
                        {
                            Console.WriteLine("Tuotteen poisto epäonnistui");
                        }
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
                        int tuotteenIdm;
                        string uusiNimi;

                        Console.WriteLine("Syötä 0 jos haluat poistua");
                        Console.Write("Syötä tuotteen id, jonka nimeä haluat muokata: ");
                        int.TryParse(Console.ReadLine(), out tuotteenIdm);
                        if (tuotteenIdm == 0)
                        {
                            break;
                        }

                        Console.Write("Syötä tuotteen uusi nimi: ");
                        uusiNimi = Console.ReadLine();

                        if (ChangeProductNameInDB(tuotteenIdm, uusiNimi))
                        {
                            Console.WriteLine("Tuotteen muokkaus onnistui");
                        }
                        else
                        {
                            Console.WriteLine("Tuotteen muokkaus epäonnistui");
                        }
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
                int protection = 1;
                while (true)
                {
                    if (varastonhallinta.Tuotteet.Find(protection) is null)
                        break;
                    else
                        protection++;
                }

                Tuote tuotteet = new()
                {
                    Id = protection,
                    Tuotenimi = newTuoteNimi,
                    Tuotteenhinta = newTuotteenHinta,
                    Varastosaldo = newVarastonSaldo
                };
                varastonhallinta.Tuotteet?.Add(tuotteet);
                int affected = varastonhallinta.SaveChanges();
                return affected == 1;
            }

            bool RemoveProductFromDB(int id)
            {
                using Varastonhallinta varastonhallinta = new();
                var tuote = varastonhallinta.Tuotteet.Find(id);

                if (tuote is null)
                {
                    Console.WriteLine($"Tuotetta jonka id on {id} ei löytynyt");
                    return false;
                }

                varastonhallinta.Tuotteet.Remove(tuote);
                int affected = varastonhallinta.SaveChanges();
                return affected == 1;
            }

            void QueryProductCount()
            {
                using Varastonhallinta varastonhallinta = new();
                Console.WriteLine("Varastossa on tämän verran:");
                IQueryable<Tuote>? kaikkiTuotteet = varastonhallinta.Tuotteet;

                // Jos hakutulos on tyhjä
                if (kaikkiTuotteet is null)
                {
                    Console.WriteLine("Yhtään tuotetta ei ole vielä lisätty tietokantaan");
                    return;
                }

                Console.WriteLine("Id, Tuote, Varastosaldo");

                foreach (Tuote tuote in kaikkiTuotteet)
                {
                    Console.WriteLine(tuote.Id + " " + tuote.Tuotenimi + " " + tuote.Varastosaldo + " kpl");
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
                    return;
                }

                Console.WriteLine("Id, Tuote");

                foreach (Tuote tuote in kaikkiTuottteet)
                {
                    Console.WriteLine(tuote.Id + " " + tuote.Tuotenimi);
                }
            }

            bool ChangeProductNameInDB(int id, string uusiNimi)
            {
                using Varastonhallinta varastonhallinta = new();
                Tuote tuote = varastonhallinta.Tuotteet.Find(id);
                if (tuote is null)
                {
                    Console.WriteLine($"Tuotetta jonka id on {id} ei löytynyt");
                    return false;
                }

                tuote.Tuotenimi = uusiNimi;
                varastonhallinta.Tuotteet.Update(tuote);
                int affected = varastonhallinta.SaveChanges();
                return affected == 1;
            }
        }
    }
}