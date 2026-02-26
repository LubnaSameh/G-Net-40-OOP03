using System;

namespace OOP_03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Part 01: Theoretical Questions

            #region Q1: Relationship Types
            /*
             * a) Composition
             * b) Association
             * c) Inheritance
             * d) Aggregation
             * e) Dependency
             */
            #endregion

            #region Q2: Access Modifiers and Sealed
            /*
             * * a) 
             * - Yes, a child class in a different assembly can access a protected field.
             * - No, you cannot access it through an object instance from the outside.
             * * b) 
             * - Protected Internal: Means the member is accessible in the same project or in any child class even in other projects.
             * - Private Protected: Means the member is accessible only by child classes that are in the same project.
             * * c) 
             * - Sealed Class: It means no other class can inherit from this class.
             * - Sealed Method: It means a child class cannot override this specific method.
             * * d) 
             * - Yes, you can create an object from a sealed class using new.
             * - Why: Because 'sealed' only prevents other classes from inheriting from it
                 it does not stop us from using the class to make objects.
             */
            #endregion

            #endregion

            #region Part 02: Practical (Extending the Movie Ticket Booking System)

            // a. Create a Cinema and open it
            Cinema myCinema = new Cinema("Galaxy Cinema");
            myCinema.OpenCinema();

            // b. Create one of each ticket type and add them to the Cinema
            StandardTicket st = new StandardTicket("Inception", 120m, "A-5");
            VIPTicket vip = new VIPTicket("Avengers", 200m, true);
            IMAXTicket imax = new IMAXTicket("Dune", 180m, false);

            myCinema.AddTicket(st);
            myCinema.AddTicket(vip);
            myCinema.AddTicket(imax);

            // c. Print all tickets
            myCinema.PrintAllTickets();

            // Print Statistics (From expected output)
            Console.WriteLine("\n========== Statistics ==========");
            Console.WriteLine($"Total Tickets Created: {Ticket.GetTotalTickets()}");
            Console.WriteLine($"Booking Ref 1: {BookingHelper.GenerateBookingReference()}");
            Console.WriteLine($"Booking Ref 2: {BookingHelper.GenerateBookingReference()}");
            Console.WriteLine($"Group Discount (5 x 100 EGP): {BookingHelper.CalcGroupDiscount(5, 100m)} EGP (10% off)");

            // d. Close the Cinema
            myCinema.CloseCinema();

            #endregion
        }
    }

    #region Part 02 Classes

    // 1. Base Class Ticket
    public class Ticket
    {
        private decimal _price;
        private static int _ticketCounter = 0;

        public string MovieName { get; set; }
        public int TicketId { get; private set; } // Read-only, auto-incremented

        public decimal Price
        {
            get { return _price; }
            set { if (value > 0) _price = value; } // Validation: > 0
        }

        public decimal PriceAfterTax
        {
            get { return Price * 1.14m; } // 14% tax
        }

        // Constructor
        public Ticket(string movieName, decimal price)
        {
            MovieName = movieName;
            Price = price;

            _ticketCounter++;
            TicketId = _ticketCounter;
        }

        // Static Method
        public static int GetTotalTickets()
        {
            return _ticketCounter;
        }

        // Override ToString
        public override string ToString()
        {
            return $"Ticket #{TicketId} | {MovieName} | Price: {Price:F0} EGP | After Tax: {PriceAfterTax:F2} EGP";
        }
    }

    // 2.a. StandardTicket Child Class
    public class StandardTicket : Ticket
    {
        public string SeatNumber { get; set; }

        public StandardTicket(string movieName, decimal price, string seatNumber) : base(movieName, price)
        {
            SeatNumber = seatNumber;
        }

        public override string ToString()
        {
            return $"{base.ToString()} | Seat: {SeatNumber}";
        }
    }

    // 2.b. VIPTicket Child Class
    public class VIPTicket : Ticket
    {
        public bool LoungeAccess { get; set; }
        public decimal ServiceFee { get; set; } = 50m;

        public VIPTicket(string movieName, decimal price, bool loungeAccess) : base(movieName, price)
        {
            LoungeAccess = loungeAccess;
        }

        public override string ToString()
        {
            string access = LoungeAccess ? "Yes" : "No";
            return $"{base.ToString()} | Lounge: {access} | Service Fee: {ServiceFee:F0} EGP";
        }
    }

    // 2.c. IMAXTicket Child Class
    public class IMAXTicket : Ticket
    {
        public bool Is3D { get; set; }

        public IMAXTicket(string movieName, decimal price, bool is3D) : base(movieName, price)
        {
            Is3D = is3D;
            if (Is3D)
            {
                Price += 30m; // Increase price by 30 if 3D
            }
        }

        public override string ToString()
        {
            string threeD = Is3D ? "Yes" : "No";
            return $"{base.ToString()} | IMAX 3D: {threeD}";
        }
    }

    // Projector Class (Used inside Cinema for Composition)
    public class Projector
    {
        public void Start() { Console.WriteLine("Projector started."); }
        public void Stop() { Console.WriteLine("Projector stopped."); }
    }

    // 3. Cinema Class
    public class Cinema
    {
        public string CinemaName { get; set; }
        private Projector _projector; // Composition (Created inside)
        private Ticket[] _tickets;
        private int _ticketCount;

        public Cinema(string name)
        {
            CinemaName = name;
            _projector = new Projector(); // Instantiated inside
            _tickets = new Ticket[20];    // Holds up to 20 tickets
            _ticketCount = 0;
        }

        public void AddTicket(Ticket t)
        {
            if (_ticketCount < 20)
            {
                _tickets[_ticketCount] = t;
                _ticketCount++;
            }
        }

        public void PrintAllTickets()
        {
            Console.WriteLine("\n========== All Tickets ==========");
            for (int i = 0; i < _ticketCount; i++)
            {
                Console.WriteLine(_tickets[i]);
            }
        }

        public void OpenCinema()
        {
            Console.WriteLine("========== Cinema Opened ==========");
            _projector.Start();
        }

        public void CloseCinema()
        {
            Console.WriteLine("\n========== Cinema Closed ==========");
            _projector.Stop();
        }
    }

    // BookingHelper (From Assignment 2 - Required for Expected Output)
    public static class BookingHelper
    {
        private static int _refCounter = 0;

        public static string GenerateBookingReference()
        {
            _refCounter++;
            return $"BK-{_refCounter}";
        }

        public static decimal CalcGroupDiscount(int count, decimal pricePerTicket)
        {
            decimal total = count * pricePerTicket;
            if (count >= 5)
                return total * 0.90m; // 10% discount
            return total;
        }
    }

    #endregion
}