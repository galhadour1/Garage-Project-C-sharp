using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class VehicleCreator
    {
        public enum eVehicleType
        {
            Motorcycle = 1,
            Car,
            Truck
        }

        // If vehicle is electric he sets is engine to ElectricEngine
        public static Vehicle CreateNewVehicle(eVehicleType? i_VehicleType)
        {
            Vehicle o_Vehicle = null;
            switch (i_VehicleType)
            {
                case eVehicleType.Motorcycle:
                    {
                        o_Vehicle = new Motorcycle();
                        break;
                    }

                case eVehicleType.Car:
                    {
                        o_Vehicle = new Car();
                        break;
                    }

                case eVehicleType.Truck:
                    {
                        o_Vehicle = new Truck();
                        break;
                    }
            }

            return o_Vehicle;
        }
    }
}
