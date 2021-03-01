using System;

namespace VehicleVannaUI_Chilelli
{
    public enum VehicleTypes
    {
        Car,
        SportsCar,
        Truck,
        Motorcycle,
        MotorHome
    }
    public class VehicleEnum
    {
        public VehicleEnum(string make, string model, string year, string vT, int lP, string bE, string bFN, string bLN)
        {
            Make = make;
            Model = model;
            Year = year;
            listPrice = lP;
            buyerEmail = bE;
            buyerFirstName = bFN;
            buyerLastName = bLN;
            vehicleType = (VehicleTypes)Enum.Parse(typeof(VehicleTypes), vT);
        }
        public VehicleTypes vehicleType { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public int listPrice { get; set; }
        public string buyerEmail { get; set; }
        public string buyerFirstName { get; set; }
        public string buyerLastName { get; set; }
    }
}
