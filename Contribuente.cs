using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace progetto_settimanaleS13L5
{
    public class Contribuente
    {
        private static int scelta;
        private static List<Contribuente> listaContribuenti = new List<Contribuente>();

        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string DataNascita { get; set; }
        public string CodiceFiscale { get; set; }
        public string Sesso { get; set; }
        public string ComuneResidenza { get; set; }
        public double RedditoAnnuale { get; set; }
        public double ImpostaDovuta { get; set; }

        private const double Aliquota1 = 0.23;
        private const double Aliquota2 = 0.27;
        private const double Aliquota3 = 0.38;
        private const double Aliquota4 = 0.41;
        private const double Aliquota5 = 0.43;


        public static Contribuente MenuContribuente()
        {
            Contribuente nuovoContribuente = new Contribuente();
            bool dataInseritaCorrettamente = false;
            bool cognomeInseritoCorrettamente = false;

            Console.WriteLine("Inserisci il nome:");
            while (true)
            {
                nuovoContribuente.Nome = Console.ReadLine();

                if (nuovoContribuente.Nome.All(char.IsLetter))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Il nome può contenere solo lettere. Inserisci nuovamente:");
                }
            }

            while (true)
            {
                Console.WriteLine("Inserisci il cognome:");
                nuovoContribuente.Cognome = Console.ReadLine();

                if (nuovoContribuente.Cognome.All(char.IsLetter))
                {
                    cognomeInseritoCorrettamente = true;
                    break;
                }
                else
                {
                    Console.WriteLine("Il cognome può contenere solo lettere. Inserisci nuovamente:");
                }
            }

            if (!dataInseritaCorrettamente)
            {
                while (true)
                {
                    Console.WriteLine("Inserisci la data di nascita (formato: dd/MM/yyyy):");
                    string inputData = Console.ReadLine();

                    try
                    {
                        DateTime dataNascita = DateTime.ParseExact(inputData, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        nuovoContribuente.DataNascita = dataNascita.ToString("dd/MM/yyyy");
                        dataInseritaCorrettamente = true;
                        break;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Formato data non valido. Inserisci la data nel formato dd/MM/yyyy.");
                    }
                }

            }

            Console.WriteLine("Inserisci il codice fiscale:");
            while (true)
            {
                string inputCodice = Console.ReadLine().ToUpper();

                if (inputCodice.Length == 16)
                {
                    nuovoContribuente.CodiceFiscale = inputCodice;
                    break;
                }
                else
                {
                    Console.WriteLine("Il codice fiscale deve contenere esattamente 16 caratteri. Inserisci nuovamente:");
                }
            }


            do
            {
                Console.WriteLine("Inserisci il sesso (M/F):");
                nuovoContribuente.Sesso = Console.ReadLine().ToUpper();

                if (nuovoContribuente.Sesso != "M" && nuovoContribuente.Sesso != "F")
                {
                    Console.WriteLine("Input non valido. Inserisci M o F per il genere.");
                }

            } while (nuovoContribuente.Sesso != "M" && nuovoContribuente.Sesso != "F");

            Console.WriteLine("Inserisci il comune di residenza:");
            nuovoContribuente.ComuneResidenza = Console.ReadLine();

            do
            {
                Console.WriteLine("Inserisci il reddito annuale:");
                string inputReddito = Console.ReadLine();

                double reddito;
                if (double.TryParse(inputReddito, out reddito) && reddito >= 0)
                {
                    nuovoContribuente.RedditoAnnuale = reddito;
                    break;
                }
                else
                {
                    Console.WriteLine("Input non valido. Assicurati di inserire un valore numerico positivo per il reddito.");
                }

            } while (true);

            return nuovoContribuente;
        }




        public static void MenuImposta()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("=========================================");
                Console.WriteLine("                 M E N U                 ");
                Console.WriteLine("=========================================");
                Console.WriteLine("1) Inserimento di una nuova dichiarazione di un contribuente");
                Console.WriteLine("2) Visualizza la lista completa di tutti i contribuenti");
                Console.WriteLine("3) Esci dal programma");
                Console.WriteLine();
                Console.Write("Scegli un opzione (1-3): ");

                if (int.TryParse(Console.ReadLine(), out scelta))
                {
                    switch (scelta)
                    {
                        case 1:
                            Console.WriteLine("=========================================");
                            Console.WriteLine("          Inserimento Contribuente       ");
                            Console.WriteLine("=========================================");
                            Contribuente nuovoContribuente = MenuContribuente();
                            if (nuovoContribuente != null)
                            {
                                CalcolaImposta(nuovoContribuente);
                                listaContribuenti.Add(nuovoContribuente);
                                Console.WriteLine();
                                Console.WriteLine("Contribuente aggiunto alla lista.");
                            }
                            break;
                        case 2:
                            Console.WriteLine();
                            Console.WriteLine("Opzione 2");
                            VisualizzaListaContribuenti();
                            break;
                        case 3:
                            Console.WriteLine();
                            Console.WriteLine("Opzione 3: Uscita dal programma.");
                            Console.WriteLine();
                            return;
                        default:
                            Console.WriteLine();
                            Console.WriteLine("Opzione non valida, riprova");
                            Console.WriteLine();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Opzione non valida, riprova");
                }

                Console.WriteLine();
                Console.WriteLine("Premi INVIO per tornare al menu principale...");
                Console.ReadLine();
            }
        }

        public static void CalcolaImposta(Contribuente contribuente)
        {
            double reddito = contribuente.RedditoAnnuale;

            if (reddito <= 15000)
            {
                contribuente.ImpostaDovuta = reddito * Aliquota1;
            }
            else if (reddito <= 28000)
            {
                contribuente.ImpostaDovuta = 3450 + (reddito - 15000) * Aliquota2;
            }
            else if (reddito <= 55000)
            {
                contribuente.ImpostaDovuta = 6960 + (reddito - 28000) * Aliquota3;
            }
            else if (reddito <= 75000)
            {
                contribuente.ImpostaDovuta = 17220 + (reddito - 55000) * Aliquota4;
            }
            else
            {
                contribuente.ImpostaDovuta = 25420 + (reddito - 75000) * Aliquota5;
            }

            Console.WriteLine();
            Console.WriteLine($"CALCOLO DELL’IMPOSTA DA VERSARE:");
            Console.WriteLine($"Contribuente: {contribuente.Nome} {contribuente.Cognome},");
            Console.WriteLine($"nato il {contribuente.DataNascita}, genere: {contribuente.Sesso},");
            Console.WriteLine($"residente in {contribuente.ComuneResidenza},");
            Console.WriteLine($"codice fiscale: {contribuente.CodiceFiscale}");
            Console.WriteLine($"Reddito dichiarato: EURO {contribuente.RedditoAnnuale}");
            Console.WriteLine($"IMPOSTA DA VERSARE: EURO {contribuente.ImpostaDovuta}");
        }

        public static void VisualizzaListaContribuenti()
        {
            Console.Clear();
            Console.WriteLine("=========================================");
            Console.WriteLine("        Lista   dei    Contribuenti      ");
            Console.WriteLine("=========================================");

            if (listaContribuenti.Count == 0)
            {
                Console.WriteLine("Nessun contribuente presente nella lista.");
            }
            else
            {
                foreach (var contribuente in listaContribuenti)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Contribuente: {contribuente.Nome} {contribuente.Cognome},");
                    Console.WriteLine($"nato il {contribuente.DataNascita} ({contribuente.Sesso}),");
                    Console.WriteLine($"residente in {contribuente.ComuneResidenza},");
                    Console.WriteLine($"codice fiscale: {contribuente.CodiceFiscale}");
                    Console.WriteLine($"Reddito dichiarato: EURO {contribuente.RedditoAnnuale}");
                    Console.WriteLine($"IMPOSTA DA VERSARE: EURO {contribuente.ImpostaDovuta}");
                }
            }

        }

    }

}
