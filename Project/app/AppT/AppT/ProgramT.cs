namespace AppT;
using System;

class ProgramT
{
    static void Main()
    {
        Console.Title = "TSP";
        while (true)
        {
            //menu technika
            Console.Clear();
            Console.WriteLine("Portal Technika");
            Console.WriteLine("1. Lista wszystkich napraw");
            Console.WriteLine("2. Rozpocznij naprawę");
            Console.WriteLine("0. Wyjdź");
            Console.Write("Wybierz opcje: ");
            string? choice = Console.ReadLine()?.Trim();

            if (choice == "1") ListTickets();
            else if (choice == "2") EditTicket();
            else if (choice == "0") return;
            else
            {
                Console.WriteLine("Nieprawidłowy wybór. Naciśnij dowolny przycisk...");
                Console.ReadKey();
            }
        }
    }

    static void ListTickets()
    {
        TicketMgr.ListAllActiveTickets();
        Console.WriteLine("\nNaciśnij dowolny przycisk...");
        Console.ReadKey();
    }

    static void EditTicket()
    {
        Console.Write("Podaj IMEI: ");
        string imei = Console.ReadLine()?.Trim() ?? "";
        
        var ticket = TicketMgr.GetActiveTicketByImei(imei);
        if (ticket == null)
        {
            Console.WriteLine("Nie znaleziono albo został już rozwiązany");
            Console.ReadKey();
            return;
        }
        
        Console.WriteLine($"\nIMEI: {ticket.Imei}");
        Console.WriteLine($"Model: {ticket.Model}");
        Console.WriteLine($"Status: {ticket.Status}");
        Console.WriteLine($"Opis: {ticket.Opis}");
        Console.WriteLine($"Komentarz ({ticket.Comments.Count}):");
        foreach (var comment in ticket.Comments)
            Console.WriteLine($"  - {comment}");


        while (true)
        {
            //edycja naprawy 
            Console.WriteLine("\nEDCJA NAPRAWY");
            Console.WriteLine("1. Dodaj komentarz");
            Console.WriteLine("2. Rozpocznij naprawę");
            Console.WriteLine("3. Wypełnij użyte części i zakończ naprawę");
            Console.WriteLine("0. Powrót do menu");
            Console.Write("Wybierz opcje: ");
            string? Choice = Console.ReadLine()?.Trim();

            if (Choice == "1")
            {
                Console.Write("Komentarz: ");
                string comment = Console.ReadLine() ?? "";
                if (!string.IsNullOrEmpty(comment))
                {
                    TicketMgr.AddComment(imei, comment);
                    Console.WriteLine("Dodano komentarz");
                }
            }
            else if (Choice == "2")
            {
                ticket.Status = "W naprawie";
                TicketMgr.SaveActiveTickets(TicketMgr.LoadActiveTickets());
                Console.WriteLine("Zmieniono na W naprawie");
                Console.ReadKey();
                return;
            }
            else if (Choice == "3")
            {
                Console.Write("Urzyte częsci: ");
                string process = Console.ReadLine() ?? "";
                if (!string.IsNullOrEmpty(process))
                {
                    TicketMgr.SetResolutionProcess(imei, process);
                    Console.WriteLine("Zakończono naprawę!");
                }
                Console.ReadKey();
                return;
            }
            else if (Choice == "0") return;
            
            Console.WriteLine("\nNaciśnij dowolny przycisk....");
            Console.ReadKey();
        }
    }
}
