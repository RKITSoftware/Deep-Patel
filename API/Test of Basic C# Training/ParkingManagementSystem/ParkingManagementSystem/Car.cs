namespace ParkingManagementSystem
{
    /// <summary>
    /// Car information which is necessary for parking in building.
    /// </summary>
    internal class Car
    {
        #region Public Properties

        /// <summary>
        /// Car's registration number
        /// </summary>
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// Senior citizen in car or not
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Car's checkin time at building
        /// </summary>
        public string CheckInTime { get; set; }

        /// <summary>
        /// Car's checkout time at building
        /// </summary>
        public string CheckOutTime { get; set; }

        /// <summary>
        /// Car's parking floor in building.
        /// </summary>
        public int ParkingFloor { get; set; }

        /// <summary>
        /// Car's parking slot in building
        /// </summary>
        public int ParkingSlotNumber { get; set; }

        /// <summary>
        /// Car's parking charges
        /// </summary>
        public int Charges { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize Car object
        /// </summary>
        /// <param name="floor">Parked floor</param>
        /// <param name="slot">Parked slot in floor</param>
        /// <param name="registrationNumber">Car's registration number</param>
        /// <param name="checkInTime">Check in time when car is parked in parking.</param>
        /// <param name="category">Senior citizen in it or not.</param>
        public Car(int floor, int slot, string registrationNumber, string checkInTime, string category)
        {
            this.ParkingFloor = floor;
            this.ParkingSlotNumber = slot;
            this.RegistrationNumber = registrationNumber;
            this.CheckInTime = checkInTime;
            this.Category = category;
        }

        #endregion
    }
}
