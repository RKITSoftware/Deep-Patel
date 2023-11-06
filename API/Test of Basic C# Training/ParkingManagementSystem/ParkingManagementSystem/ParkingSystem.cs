using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ParkingManagementSystem
{
    internal class ParkingSystem : IParkingSystem
    {
        #region Public properties

        /// <summary>
        /// Specifies number of floors of building.
        /// </summary>
        public int NumberOfFloors { get; set; }
        /// <summary>
        /// specifies number of slots per floor.
        /// </summary>
        public int NumberOfSlots { get; set; }
        /// <summary>
        /// specifies reserved slots for seniour citizen which are in car.
        /// </summary>
        public HashSet<string> ReservedSlots { get; set; }
        /// <summary>
        /// Stores parking details of car which present in the building.
        /// </summary>
        public List<Car> Cars { get; set; }
        /// <summary>
        /// Stores car details which are checkout and pay charges.
        /// </summary>
        public List<Car> AllCarsData { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize parking object for a building.
        /// </summary>
        /// <param name="numberOfFloors">Floors of building</param>
        /// <param name="numberOfSlots">Slots per floor</param>
        /// <param name="reservedSlots">reserved slot for seniour citizen</param>
        public ParkingSystem(int numberOfFloors, int numberOfSlots, string reservedSlots)
        {
            NumberOfFloors = numberOfFloors;
            NumberOfSlots = numberOfSlots;
            ReservedSlots = new HashSet<string>(reservedSlots.Split(' '));
            Cars = new List<Car>();
            AllCarsData = new List<Car>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Check and park a car at specific spot in parking if slot is available.
        /// </summary>
        /// <param name="registrationNumber">Car's registration number</param>
        /// <param name="checkInTime">Check in time when car is reach at parking spot</param>
        /// <param name="category">Specifies car have seniour citizen or not</param>
        public void CheckIn(string registrationNumber, string checkInTime, string category)
        {
            // Check car is already exist in building or not.
            if (isCarExistsInBuilding(registrationNumber))
            {
                Console.WriteLine("Car is already in building.");
                return;
            }

            // Check spot and if parking space available then park the car.
            for (int floor = 0; floor < NumberOfFloors; floor++)
            {
                for (int slot = 1; slot <= NumberOfSlots; slot++)
                {
                    string parkingSlot = (char)('A' + floor) + "-" + slot;

                    // Checking parking slot
                    if (isSlotAvailable(floor, slot))
                    {
                        // Senior citizen 
                        if (ReservedSlots.Contains(parkingSlot) && category.Equals("R"))
                        {
                            AddCar(floor, slot, parkingSlot, registrationNumber, checkInTime, "R");
                            return;
                        }
                        // Non-senior citizen
                        else if (!ReservedSlots.Contains(parkingSlot) && category.Equals("NR"))
                        {
                            AddCar(floor, slot, parkingSlot, registrationNumber, checkInTime, "NR");
                            return;
                        }
                    }
                }
            }

            Console.WriteLine("Parking is full.");
            return;
        }

        /// <summary>
        /// Car is leave at that time calculate its charges for parking.
        /// </summary>
        /// <param name="registrationNumber">Car's registration number</param>
        /// <param name="checkOutTime">Check out time</param>
        public void CheckOut(string registrationNumber, string checkOutTime)
        {
            // Check car is already exist in building or not.
            if (isCarExistsInBuilding(registrationNumber))
            {
                int charges = 0;
                string[] checkOut = checkOutTime.Split(':');
                TimeSpan endTime = new TimeSpan(int.Parse(checkOut[0]), int.Parse(checkOut[1]), 0);

                // Finding the car
                foreach (var car in Cars)
                {
                    if (car.RegistrationNumber == registrationNumber)
                    {
                        string[] checkIn = car.CheckInTime.Split(':');
                        TimeSpan startTime = new TimeSpan(int.Parse(checkIn[0]), int.Parse(checkIn[1]), 0);

                        // Checks checkOutTime is not lower than checkInTime
                        if (startTime < endTime)
                        {
                            // Calculating time
                            var duration = endTime.Subtract(startTime);
                            int seconds = (duration.Hours * 3600) + (duration.Minutes * 60) + duration.Seconds;

                            // Calculating Charges according to time
                            if (seconds <= 7200)
                                charges = 50;
                            else if (seconds > 7200 && seconds <= 14400)
                                charges = 80;
                            else
                                charges = 100;

                            car.Charges = charges;
                            car.CheckOutTime = checkOutTime;
                            AllCarsData.Add(car);
                            Cars.Remove(car);

                            Console.WriteLine($"Parking charge for {registrationNumber} car is {charges}");
                            return;
                        }
                    }
                }
            }

            Console.WriteLine("Car is not in the building.");
            return;
        }

        /// <summary>
        /// Generate report of all cars which leave
        /// </summary>
        public void GenerateReport()
        {
            // order the data by parking floor, slot, and check in time.
            var data = AllCarsData.OrderBy(car => car.ParkingFloor)
                .ThenBy(car => car.ParkingSlotNumber)
                .ThenBy(car => car.CheckInTime)
                .ToList();

            //// Printing the data
            //Console.WriteLine("Parking Slot  Number Plate     Checkin time  Checkout time  Charges  Category");
            //Console.WriteLine(new string('-', 77));
            //foreach (var car in data)
            //{
            //    string parkingSlot = (char)('A' + car.ParkingFloor) + "-" + car.ParkingSlotNumber;
            //    Console.WriteLine("{0, -12}  {1, -15}  {2, 12}  {3, 13}  Rs. {4, 3}  {5, 8}", parkingSlot,
            //        car.RegistrationNumber, car.CheckInTime, car.CheckOutTime, car.Charges, car.Category);
            //}

            // Printing data on txt file
            string path = $"C:\\Users\\DEEP\\Desktop\\ASP.Net Core\\ParkingManagementSystem\\ParkingManagementSystem\\Report\\{DateTime.Now.ToString("yyyyMMddHHmmss")}.txt";
            Console.WriteLine(path);

            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
                {
                    writer.WriteLine("Parking Slot  Number Plate     Checkin time  Checkout time  Charges  Category");
                    writer.WriteLine(new string('-', 77));

                    foreach (var car in data)
                    {
                        string parkingSlot = (char)('A' + car.ParkingFloor) + "-" + car.ParkingSlotNumber;
                        writer.WriteLine("{0, -12}  {1, -15}  {2, 12}  {3, 13}  Rs. {4, 3}  {5, 8}", parkingSlot,
                            car.RegistrationNumber, car.CheckInTime, car.CheckOutTime, car.Charges, car.Category);
                    }
                }
            }

            Console.WriteLine("Data is successfully written on file.");
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Add car into parking
        /// </summary>
        /// <param name="floor">Specifies floor</param>
        /// <param name="slot">Specifies slot in floor</param>
        /// <param name="parkingSlot">Specifies parking slot</param>
        /// <param name="registrationNumber">Car's registration number</param>
        /// <param name="checkInTime">Car's check in time</param>
        /// <param name="category">Senior citizen in it or not</param>
        private void AddCar(int floor, int slot, string parkingSlot, string registrationNumber, string checkInTime, string category)
        {
            Car car = new Car(floor, slot, registrationNumber, checkInTime, category);
            Cars.Add(car);
            Console.WriteLine($"Parking slot for {registrationNumber} is {parkingSlot}.");
        }

        /// <summary>
        /// Checks is space available in building or not.
        /// </summary>
        /// <param name="floor">Building floor</param>
        /// <param name="slot">Slot in Floor</param>
        /// <returns>True if space is available else false</returns>
        private bool isSlotAvailable(int floor, int slot)
        {
            return !Cars.Any(car => floor == car.ParkingFloor && slot == car.ParkingSlotNumber);
        }

        /// <summary>
        /// Checks is car already present in building or not.
        /// </summary>
        /// <param name="registrationNumber">Car's registartion number</param>
        /// <returns>True if car is present in building else false.</returns>
        private bool isCarExistsInBuilding(string registrationNumber)
        {
            return Cars.Any(car => registrationNumber == car.RegistrationNumber);
        }

        #endregion
    }
}
