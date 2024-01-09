using System;

namespace ParkingManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Taking user inputs for parking
            Console.Write("Enter number of Floors :- ");
            int numberOfFloors = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter number of Slots :- ");
            int numberOfSlots = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter reseved slots :- ");
            string reservedSlots = Console.ReadLine();

            // Initializing a parkingSystem object
            ParkingSystem parkingSystem = new ParkingSystem(numberOfFloors, numberOfSlots, reservedSlots);

            // Running app
            while (true)
            {
                // Getting car details.
                string[] carDetails = Console.ReadLine().Split(' ');

                switch (carDetails[0])
                {
                    case "GENERATE":
                        parkingSystem.GenerateReport();
                        break;
                    case "CHECKOUT":
                        parkingSystem.CheckOut(carDetails[1], carDetails[2]);
                        break;
                    case "CHECKIN":
                        parkingSystem.CheckIn(carDetails[1], carDetails[2], carDetails[3]);
                        break;
                    default:
                        Console.WriteLine("Entered incorrect data format so system terminated.");
                        System.Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
