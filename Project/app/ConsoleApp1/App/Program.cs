using App;
class Program
{
    static void Main()
    {
        Console.Title = "Klijent";
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Wybierz opcje z menu: ");
            Console.WriteLine("1. Nowe zgłoszenie");
            Console.WriteLine("2. Sprawdź status zgłoszenia");
            Console.WriteLine("0. wyjdź");
            Console.Write("Select option: ");
            string? choice = Console.ReadLine()?.Trim();

            if (choice == "1") SubmitNewTicket();
            else if (choice == "2") CheckTicketStatus();
            else if (choice == "0") return;
            else
            {
                Console.WriteLine("nie prawidłowa opcja!");
                Console.ReadKey();
            }
        }
    }

    static void SubmitNewTicket()
    {
        Console.Clear();
        Console.Write("Podaj IMEI urządzenia: ");
        string imei = Console.ReadLine()?.Trim() ?? "";

        if (string.IsNullOrEmpty(imei))
        {
            Console.WriteLine("IMEI wymagany");
            Console.ReadKey();
            return;
        }

        if (TicketMgr.IsImeiTaken(imei))
        {
            Console.WriteLine($"IMEI '{imei}' Już istnieje w zgłoszeniah");
            Console.ReadKey();
            return;
        }

        Console.Write("Model urządzenia: ");
        string subject = Console.ReadLine() ?? "";

        Console.Write("Opis usterki: ");
        string description = Console.ReadLine() ?? "";

        var ticket = new Ticket { Imei = imei, Model = subject, Opis = description };
        TicketMgr.AddTicket(ticket);

        Console.WriteLine($"\nZgłoszenie '{imei}' utworzone!");
        Console.WriteLine("naciśnij przycisk aby kontynuować...");
        Console.ReadKey();
    }

    static void CheckTicketStatus()
    {
        Console.Clear();
        Console.Write("Podaj IMEI: ");
        string imei = Console.ReadLine()?.Trim() ?? "";

        var ticket = TicketMgr.FindTicket(imei);
        if (ticket == null)
        {
            Console.WriteLine("Nie znaleziono zgłoszenia!");
        }
        else
        {
            Console.WriteLine($"\nZgłoszenie: ");
            Console.WriteLine($"IMEI: {ticket.Imei}");
            Console.WriteLine($"Model {ticket.Model}");
            Console.WriteLine($"Status: {ticket.Status}");
            Console.WriteLine($"Utworzono: {ticket.Created:yyyy-MM-dd HH:mm}");
            Console.WriteLine($"Opis {ticket.Opis}");
        }
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}