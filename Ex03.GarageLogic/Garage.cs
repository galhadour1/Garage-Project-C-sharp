using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly List<VehicleInGarage> r_VehiclesInGarage;

        public Garage()
        {
            r_VehiclesInGarage = new List<VehicleInGarage>();
        }

        public List<VehicleInGarage> VehiclesInGarage
        {
            get
            {
                return r_VehiclesInGarage;
            }
        }

        public void AddVehicleToTheGarage(VehicleInGarage i_VehicleToAdd)
        {
            VehicleInGarage foundVehicle = null;

            foundVehicle = SearchVehicleInTheGarage(i_VehicleToAdd.LicenseNumber);
            if (foundVehicle == null)
            {
                r_VehiclesInGarage.Add(i_VehicleToAdd);
            }
            else
            {
                foundVehicle.VehicleStatus = eVehicleStatus.InRepair;
                throw new ArgumentException(@"The requested vehicle is already exits in the Data Base, replacing the vehicle in repair");
            }
        }

        public VehicleInGarage SearchVehicleInTheGarage(string i_LicenseNumber)
        {
            VehicleInGarage o_FoundVehicle = null;

            foreach (VehicleInGarage currentVehicle in r_VehiclesInGarage)
            {
                if (currentVehicle.LicenseNumber == i_LicenseNumber) 
                {
                    o_FoundVehicle = currentVehicle;
                }
            }

            return o_FoundVehicle;
        }

        public void BlowWheelsToMaxByLicense(string i_LicenseNumber)
        {
            VehicleInGarage vehicle = null;

            vehicle = SearchVehicleInTheGarage(i_LicenseNumber);
            if (vehicle != null)
            {
                vehicle.BlowWheelsToMax();
            }
        }

        public void ChangeVehicleStatus(string i_LicenseNumber, eVehicleStatus i_NewVehicleStatus)
        {
            VehicleInGarage vehicle = null;

            vehicle = SearchVehicleInTheGarage(i_LicenseNumber);
            if (vehicle != null)
            {
                vehicle.VehicleStatus = i_NewVehicleStatus;
            }
            else
            {
                throw new ArgumentException("The vehicle does not exists in the garage.");
            }
        }

        public void RefuelVehicle(string i_LicenseNumber, float i_AmountFuelToAdd, eFuelType i_FuelType)
        {
            VehicleInGarage vehicle = null;

            vehicle = SearchVehicleInTheGarage(i_LicenseNumber);
            if (vehicle.VehicleToRepair.Engine.EngineType == eEngineType.Fuel) 
            {
                if (vehicle != null)
                {
                    vehicle.VehicleToRepair.Engine.AddFuelOrChargeBattery(i_AmountFuelToAdd, i_FuelType);
                }
                else
                {
                    throw new ArgumentException("The vehicle does not exists in the garage.");
                }
            }
            else
            {
                throw new ArgumentException("The vehicle has electric engine! You can not refuel it.");
            }
        }

        public void ChargeBatteryVehicle(float i_HoursToCharge, string i_LicenseNumber)
        {
            VehicleInGarage vehicle = null;

            vehicle = SearchVehicleInTheGarage(i_LicenseNumber);
            if (vehicle.VehicleToRepair.Engine.EngineType == eEngineType.Electric) 
            {
                if (vehicle != null)
                {
                    vehicle.VehicleToRepair.Engine.AddFuelOrChargeBattery(i_HoursToCharge, eFuelType.None);
                }
                else
                {
                    throw new ArgumentException("The vehicle does not exists in the garage.");
                }
            }
            else
            {
                throw new ArgumentException("The vehicle has fuel engine! You can not charge it.");
            }
        }

        public string GetDetailsByLicense(string i_LicenseNumber)
        {
            string o_DetailsOfVehicle = string.Empty;
            VehicleInGarage vehicle = null;

            vehicle = SearchVehicleInTheGarage(i_LicenseNumber);
            if (vehicle != null)
            {
                o_DetailsOfVehicle = string.Format(
@"License number  = {0}

Model name = {1}

Owner name = {2}

Phone number = {3}

Energy Porcentage remaining = {4}%

Garage status = {5}

Wheels information : 
{6}
Energy information : 
{7}

Other information : 
{8}
",
               vehicle.LicenseNumber,
               vehicle.VehicleToRepair.ModelName,
               vehicle.OwnerName,
               vehicle.PhoneNumber,
               vehicle.VehicleToRepair.Engine.EnergyPercent.ToString(),
               vehicle.VehicleStatus.ToString(),
               vehicle.VehicleToRepair.GetWheelsDetails(),
               vehicle.VehicleToRepair.Engine.GetDetailsEngine(),
               vehicle.VehicleToRepair.GetOtherDetails());
            }
            else
            {
                throw new ArgumentException("The vehicle does not exists in the garage.");
            }

            return o_DetailsOfVehicle;
        }

        public List<VehicleInGarage> GetVehicleByStatus(eVehicleStatus i_VehicleStatus)
        {
            List<VehicleInGarage> o_ListVehiclesByStatus = new List<VehicleInGarage>();

            if (r_VehiclesInGarage.Count == 0)
            {
                throw new ArgumentException("The garage is empty");
            }

            foreach (VehicleInGarage vehicle in r_VehiclesInGarage)
            {
                if (vehicle.VehicleStatus.Equals(i_VehicleStatus))
                {
                    o_ListVehiclesByStatus.Add(vehicle);
                }
            }

            if (o_ListVehiclesByStatus.Count == 0)
            {
                throw new ArgumentException("There are 0 vechiles at the garage in this status");
            }

            return o_ListVehiclesByStatus;
        }
    }
}
