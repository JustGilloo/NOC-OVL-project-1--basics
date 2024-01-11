namespace Project1_basisC_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Wat heb ik nodig?

            bool running = true, bevatInhoud = false;
            string naamActiviteit = "", aantalUrenActiviteit = "";
            string activiteitenfile = @"activiteiten.txt";
            string inputKeuze;
            string[] activiteiten;
            string aanmaakdatum = DateTime.Now.ToString("dd/MM/yyyy, HH:mm");

            //Wat wil ik doen?

            while (running)
            {
                Opstart();
                Hoofdmenukeuze(out inputKeuze);
                switch (inputKeuze)
                {
                    case "1":
                        InputActiviteitAanmaken(out naamActiviteit, out aantalUrenActiviteit);
                        ActiviteitenBestandAanmaken(naamActiviteit, aantalUrenActiviteit, aanmaakdatum, activiteitenfile);
                        Console.WriteLine($"De activiteit {naamActiviteit} werd aangemaakt.");
                        Console.WriteLine("Druk op een toets om terug te keren naar het hoofdmenu.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "2":
                        FileInhoudChecken(out bevatInhoud);
                        if (bevatInhoud == false)
                        {
                            Console.WriteLine("Er zijn nog geen activiteiten aangemaakt. Gelieve een activiteit aan te maken.");
                        } else if (bevatInhoud == true)
                        {
                            ActiviteitWijzigen();
                            Console.WriteLine($"Activiteit {naamActiviteit} is succesvol gewijzigd!");
                        }
                        Console.WriteLine("Druk op een toets om terug te keren naar het hoofdmenu.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "3":
                        FileInhoudChecken(out bevatInhoud);
                        if (bevatInhoud == false)
                        {
                            Console.WriteLine("Er zijn nog geen activiteiten aangemaakt. Gelieve een activiteit aan te maken.");
                        }
                        else if (bevatInhoud == true)
                        {
                            ActiviteitVerwijderen(out activiteiten);
                            BestandOverschrijven(activiteiten, activiteitenfile);
                        }
                        Console.WriteLine("Druk op een toets om terug te keren naar het hoofdmenu.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "4":
                        FileInhoudChecken(out bevatInhoud);
                        if (bevatInhoud == false)
                        {
                            Console.WriteLine("Er zijn nog geen activiteiten aangemaakt. Gelieve een activiteit aan te maken.");
                        }
                        else if (bevatInhoud == true)
                        {
                            ActiviteitenWeergeven();
                        }
                        Console.WriteLine("Druk op een toets om terug te keren naar het hoofdmenu.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "5":
                        Console.WriteLine("De applicatie wordt nu gesloten.");
                        running = false;
                        break;
                }
            }
        }

        public static void Opstart() //vraagt bij eerste gebruik van de app naar de naam van de gebruiker en slaat deze op. Daarna wordt de gebruiker bij elke opstart persoonlijk gegroet.
        { 
            string naam;
            if (System.IO.File.Exists("naamGebruiker.txt") == false)
            {
                using (StreamWriter mijnStreamwriter = new StreamWriter("naamGebruiker.txt"))
                {
                    Console.WriteLine("Dag gebruiker! Wat is jouw naam?");
                    naam = Console.ReadLine();
                    mijnStreamwriter.Write(naam);
                    Console.Clear();
                    Console.WriteLine($"Dag {naam}! Wat wil je doen vandaag?");
                }
            }
            else if (System.IO.File.Exists("naamGebruiker.txt"))
            {
                StreamReader leesobject = new StreamReader("naamGebruiker.txt");
                string inhoud = "";
                inhoud = inhoud + leesobject.ReadToEnd();
                leesobject.Close();
                Console.WriteLine($"Dag {inhoud}! Wat wil je doen vandaag?");
            }
            if (System.IO.File.Exists("activiteiten.txt") == false)
            {
                File.Create(@"activiteiten.txt").Close();
            }
        }

        public static void Hoofdmenukeuze(out string input) //checkt de of de input ingegeven bij het hoofdmenu correct is en geeft deze terug aan Main() om de juiste vervolgmethode (menu) op te roepen.
        {  
            bool geldigeInput = false;

            Console.WriteLine("1. Activiteit aanmaken");
            Console.WriteLine("2. Activiteit wijzigen");
            Console.WriteLine("3. Activiteit verwijderen");
            Console.WriteLine("4. Activiteitenoverzicht weergeven");
            Console.WriteLine("5. Applicatie sluiten");
            Console.WriteLine();
            Console.WriteLine("Gelieve uw keuze te maken.");
            input = Console.ReadLine();

            while (geldigeInput == false)
            {
                switch (input)
                {
                    case "1":
                        Console.Clear();
                        geldigeInput = true;
                        break;
                    case "2":
                        Console.Clear();
                        geldigeInput = true;
                        break;
                    case "3":
                        Console.Clear();
                        geldigeInput = true;
                        break;
                    case "4":
                        Console.Clear();
                        geldigeInput = true;
                        break;
                    case "5":
                        Console.Clear();
                        geldigeInput = true;
                        break;
                    default:
                        Console.WriteLine("Geen geldige keuze. Gelieve een keuze te maken uit bovenstaande mogelijkheden.");
                        input = Console.ReadLine();
                        break;
                }
            }
        }

        public static void InputActiviteitAanmaken(out string naamParameter, out string tijdParameter) //wordt opgeroepen als gebruiker 1 kiest in hoofdmenu. Geeft input terug aan Main() om gegevens mee aan te maken..
        { 
            Console.WriteLine("Activiteit aanmaken:");
            Console.WriteLine("Wat is de naam van de activiteit?");
            naamParameter = Console.ReadLine();
            Console.WriteLine("Hoelang heb je al aan deze activiteit gespendeerd? (In uren, geef in kommagetal in)");
            tijdParameter = Console.ReadLine();
            while (String.IsNullOrEmpty(naamParameter) == true || String.IsNullOrEmpty(tijdParameter) == true)
            {
                if (String.IsNullOrEmpty(naamParameter) == true)
                {
                    Console.WriteLine("U gaf geen gegevens in. Gelieve de naam van de activiteit in te geven.");
                    Console.WriteLine("Wat is de naam van de activiteit?");
                    naamParameter = Console.ReadLine();
                }
                if (String.IsNullOrEmpty(tijdParameter) == true)
                {
                    Console.WriteLine("U gaf geen gegevens in. Gelieve de duurtijd in uren in te geven als kommagetal.");
                    Console.WriteLine("Hoelang heb je al aan deze activiteit gespendeerd? (In uren, geef een kommagetal in)");
                    tijdParameter = Console.ReadLine();
                }
            }
        }

        public static void ActiviteitenBestandAanmaken(string naamActiviteit, string duurActiviteit, string datum, string file) //maakt een activiteit aan op de volgende lege index in de ingeladen file.
        {
            string inhoud = "";
            if (System.IO.File.Exists("activiteiten.txt"))
            {
                inhoud = File.ReadAllText("activiteiten.txt");
            }
            inhoud = inhoud + $"{naamActiviteit.PadLeft(10).PadRight(20)} {duurActiviteit.PadLeft(10).PadRight(18)} \t {datum} \n";
            File.WriteAllText(file, inhoud);
        }

        public static void BestandOverschrijven(string[] arrWegTeSchrijven, string file) //overschrijft wijzigingen weg aan de gegevensarray naar een textbestand.
        {
            string inhoud = "";
            foreach (string lijn in arrWegTeSchrijven)
            {
                inhoud += lijn + Environment.NewLine;
            }
 
            File.WriteAllText(file, inhoud);
        }

        public static void ActiviteitWijzigen() //verwijdert de ingegeven index zoals bij ActiviteitVerwijderen maar vraagt meteen om de activiteit weer volledig in te geven. Zo is alles op wijzigingsdatum gesorteerd.
        {
            string[] inhoudInArray = new string[] { }, nieuweInhoudInArray, arrNieuweArray;
            string inputGebruiker = "";
            string naamParameter = "", tijdParameter = "";
            string file = @"activiteiten.txt";
            string wijzigingsdatum = DateTime.Now.ToString("dd/MM/yyyy, HH:mm");
            if (System.IO.File.Exists("activiteiten.txt"))
            {
                int teller = 0;
                bool isGeheelGetal = false;
                bool correcteSelectie = false;
                Console.WriteLine("Activiteiten wijzigen:");
                Console.WriteLine("Welke activiteit wil je wijzigen?");
                Console.WriteLine();
                Console.WriteLine("Naam activiteit".PadLeft(15).PadRight(20) + "Gespendeerde tijd (uren)".PadLeft(15).PadRight(28) + "Wijzigingsdatum".PadLeft(17));
                inhoudInArray = File.ReadAllLines("activiteiten.txt");
                int tellerActiviteiten = inhoudInArray.Length;

                foreach (string lijn in inhoudInArray)
                {
                    teller++;
                    Console.WriteLine($"{teller}. {lijn}");
                }
                Console.WriteLine();
                Console.WriteLine("Geef het getal in van de activiteit die je wilt wijzigen.");
                inputGebruiker = Console.ReadLine();
                isGeheelGetal = IsGeheelGetal(inputGebruiker);

                while (isGeheelGetal == false)
                {
                    Console.WriteLine("U gaf geen getal in. Geef het cijfer in van de activiteit die je wilt wijzigen.");
                    inputGebruiker = Console.ReadLine();
                    isGeheelGetal = IsGeheelGetal(inputGebruiker);
                }

                while (correcteSelectie == false)
                {
                    if (int.Parse(inputGebruiker) <= 0 || int.Parse(inputGebruiker) > tellerActiviteiten)
                    {
                        Console.WriteLine("Geen bestaande activiteit geselecteerd. Gelieve het cijfer in te geven van de activiteit die je wilt wijzigen.");
                        inputGebruiker = Console.ReadLine();
                        isGeheelGetal = IsGeheelGetal(inputGebruiker);
                    }
                    else
                    {
                        correcteSelectie = true;
                        inhoudInArray = inhoudInArray.Where((source, index) => index != int.Parse(inputGebruiker) - 1).ToArray();
                    }
                }
            }
            
            nieuweInhoudInArray = inhoudInArray;
            arrNieuweArray = nieuweInhoudInArray;

            BestandOverschrijven(arrNieuweArray, file);
            
            Console.WriteLine();
            Console.WriteLine("Wat is de naam van de activiteit?");
            naamParameter = Console.ReadLine();
            Console.WriteLine("Hoelang heb je al aan deze activiteit gespendeerd? (In uren, geef in kommagetal in)");
            tijdParameter = Console.ReadLine();
            while (String.IsNullOrEmpty(naamParameter) == true || String.IsNullOrEmpty(tijdParameter) == true)
            {
                if (String.IsNullOrEmpty(naamParameter) == true)
                {
                    Console.WriteLine("U gaf geen gegevens in. Gelieve de naam van de activiteit in te geven.");
                    Console.WriteLine("Wat is de naam van de activiteit?");
                    naamParameter = Console.ReadLine();
                }
                if (String.IsNullOrEmpty(tijdParameter) == true)
                {
                    Console.WriteLine("U gaf geen gegevens in. Gelieve de duurtijd in uren in te geven als kommagetal.");
                    Console.WriteLine("Hoelang heb je al aan deze activiteit gespendeerd? (In uren, geef een kommagetal in)");
                    tijdParameter = Console.ReadLine();
                }
            }
            ActiviteitenBestandAanmaken(naamParameter, tijdParameter, wijzigingsdatum, file);
        }

        public static void ActiviteitVerwijderen(out string[] arrNieuweArray) //schrijft de array van gegevens maar slaat de ingegeven index over bij het herschrijven.
        {
            string[] inhoudInArray = new string[] { }, nieuweInhoudInArray;
            string inputGebruiker = "";
            if (System.IO.File.Exists("activiteiten.txt"))
            {
                int teller = 0;
                bool isGeheelGetal = false;
                bool correcteSelectie = false;
                Console.WriteLine("Activiteiten verwijderen:");
                Console.WriteLine("Welke activiteit wil je verwijderen?");
                Console.WriteLine();
                Console.WriteLine("Naam activiteit".PadLeft(15).PadRight(20) + "Gespendeerde tijd (uren)".PadLeft(15).PadRight(28) + "Wijzigingsdatum".PadLeft(17));
                inhoudInArray = File.ReadAllLines("activiteiten.txt");
                int tellerActiviteiten = inhoudInArray.Length;

                foreach (string lijn in inhoudInArray)
                {
                    teller++;
                    Console.WriteLine($"{teller}. {lijn}");
                }
                Console.WriteLine();
                Console.WriteLine("Geef het getal in van de activiteit die je wilt verwijderen.");
                inputGebruiker = Console.ReadLine();
                isGeheelGetal = IsGeheelGetal(inputGebruiker);

                while (isGeheelGetal == false)
                {
                    Console.WriteLine("U gaf geen getal in. Geef het cijfer in van de activiteit die je wilt verwijderen.");
                    inputGebruiker = Console.ReadLine();
                    isGeheelGetal = IsGeheelGetal(inputGebruiker);
                }

                while (correcteSelectie == false)
                {
                    if (int.Parse(inputGebruiker) <= 0 || int.Parse(inputGebruiker) > tellerActiviteiten)
                    {
                        Console.WriteLine("Geen bestaande activiteit geselecteerd. Gelieve het cijfer in te geven van de activiteit die je wilt verwijderen.");
                        inputGebruiker = Console.ReadLine();
                        isGeheelGetal = IsGeheelGetal(inputGebruiker);
                    }
                    else
                    {
                        correcteSelectie = true;
                        inhoudInArray = inhoudInArray.Where((source, index) => index != int.Parse(inputGebruiker) - 1).ToArray();
                    }
                }
            }
            nieuweInhoudInArray = inhoudInArray;
            arrNieuweArray = nieuweInhoudInArray;
            Console.WriteLine($"Activiteit {inputGebruiker} is succesvol verwijderd!");
        }

        public static void ActiviteitenWeergeven() //laadt bestaande activiteiten in in een mooi overzicht voor de gebruiker.
        {
            if (System.IO.File.Exists("activiteiten.txt"))
            {
                Console.WriteLine("Activiteiten weergeven:");
                Console.WriteLine("Hieronder vind je een overzicht van al je activiteiten en de tijd die je eraan spendeert.");
                Console.WriteLine();
                Console.WriteLine("Naam activiteit".PadLeft(15).PadRight(20) + "Gespendeerde tijd (uren)".PadLeft(15).PadRight(28) + "Wijzigingsdatum".PadLeft(17));
                string inhoud = File.ReadAllText("activiteiten.txt");
                Console.WriteLine(inhoud);
            }
        }

        public static void FileInhoudChecken(out bool input) //checkt of er al activiteiten zijn opgeslagen alvorens ze al dan niet af te drukken waar nodig.
        {
            string inhoud = "";
            bool bevatInhoud = false;
            if (System.IO.File.Exists("activiteiten.txt"))
            {
                inhoud = File.ReadAllText("activiteiten.txt");
                if (String.IsNullOrEmpty(inhoud))
                {
                    bevatInhoud = false;
                }
                else
                    bevatInhoud = true;
            }

            input = bevatInhoud;
        }

        static bool IsGeheelGetal(string input) //functie die checkt of de input van de gebruiker omzetbaar is naar integrale, geeft dit terug als JUIST of FOUT.
        {
            int tijdelijk;
            return int.TryParse(input, out tijdelijk);
        }
    }
}