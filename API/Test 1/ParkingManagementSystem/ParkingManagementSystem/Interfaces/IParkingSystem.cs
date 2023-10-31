namespace ParkingManagementSystem
{
    /// <summary>
    /// All methods which are necessary for parking system
    /// </summary>
    internal interface IParkingSystem
    {
        void CheckIn(string registrationNumber, string checkInTime, string category);
        void CheckOut(string registrationNumber, string checkOutTime);
        void GenerateReport();
    }
}
