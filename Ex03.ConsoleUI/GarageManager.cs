using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public enum eMenuOperation
    {
        AddVehicleToGarage = 1,
        PrintAllVehicleListByFilter,
        ChangeVehicleStatus,
        BlowAir,
        FuelVehicle,
        ChargeVehicle,
        PrintVehicleData,
        Exit,
    }

    public class GarageManager
    {
        private const int k_NumberOfManuOptions = 8;
        private readonly int m_VehiclesTypeAmount = 0;
        private readonly Garage m_Garage;

        public GarageManager(Garage i_Garage)
        {
            m_Garage = i_Garage;
            Array vehiclesValues = Enum.GetValues(typeof(VehicleCreator.eVehicleType));

            foreach (VehicleCreator.eVehicleType val in vehiclesValues)
            {
                m_VehiclesTypeAmount++;
            }
        }

        public static void DisplayMenu()
        {
            Console.Clear();
            string manuMsg = string.Format(
@"Welcome to Gal & Chen Garage 
Press The number of your request as follows:
=====================================================
1. Add vehicle to garage
2. Present vehicle list by filter
3. Change vehicle status (need license & next status)
4. Blow air to Max by license number
5. Fuel vehicle (need license num, type and amount)
6. Charge vehicle (need license num and amount)
7. Present vehicle data by license num
8. Exit garage
=====================================================");

            Console.WriteLine(manuMsg);
        }

        public void RunGarage()
        {
            bool exit = false;
            eMenuOperation userChoice;

            while (!exit)
            {
                DisplayMenu();
                userChoice = GetMenuChoice();
                switch(userChoice)
                {
                    case eMenuOperation.AddVehicleToGarage:
                        AddVehicleToGarage();
                        break;
                    case eMenuOperation.PrintAllVehicleListByFilter:
                        PrintAllVehicleListByFilterUI();
                        break;
                    case eMenuOperation.ChangeVehicleStatus:
                        ChangeVehicleStatusUI();
                        break;
                    case eMenuOperation.BlowAir:
                         BlowAirToMaxUI();
                        break;
                    case eMenuOperation.FuelVehicle:
                        FuelVehicleUI();
                        break;
                    case eMenuOperation.ChargeVehicle:
                        ChargeVehicleUI();
                        break;
                    case eMenuOperation.PrintVehicleData:
                        PrintDetailsByLicenseUI();
                        break;
                    case eMenuOperation.Exit:
                        exit = true;
                        Console.WriteLine("Bye Bye! :)\n");
                        break;
                }

                if (!exit)
                {
                    DisplayMenu();
                }
            }
        }

        public eMenuOperation GetMenuChoice()
        {
            eMenuOperation o_ReturnMenuOperation = eMenuOperation.Exit;
            string userChoice = "8";
            int chosenOption = 8;
            try
            {
                userChoice = Console.ReadLine();
                if (!int.TryParse(userChoice, out chosenOption))
                {
                    throw new FormatException("Invalid input please try again");
                }

                if (chosenOption >= 1 && chosenOption <= k_NumberOfManuOptions)
                {
                    o_ReturnMenuOperation = (eMenuOperation)Enum.Parse(typeof(eMenuOperation), userChoice);
                    Console.Clear();
                }
                else
                {
                    throw new ArgumentException("You entered invalid key! Please try again.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter a number.");
                System.Threading.Thread.Sleep(5000);
                DisplayMenu();
                o_ReturnMenuOperation = GetMenuChoice();
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Please enter a number between 1 to {0}.", k_NumberOfManuOptions);
                System.Threading.Thread.Sleep(5000);
                DisplayMenu();
                o_ReturnMenuOperation = GetMenuChoice();
            }

            return o_ReturnMenuOperation;
        }

        public void AddVehicleToGarage()
        {
            int chosenOption = 0;
            VehicleCreator.eVehicleType? vehicleToAdd = null;
            Vehicle newVehicleToAdd = null;
            string userChoice = null;
            string ownerName = null;
            string ownerPhone = null;
            int index = 1;

            Console.WriteLine("Please choose the vehicle index you want to add to the garage from the list:");
            Array vehiclesValues = Enum.GetValues(typeof(VehicleCreator.eVehicleType));
            foreach (VehicleCreator.eVehicleType val in vehiclesValues)
            {
                Console.WriteLine(string.Format("{0}: {1}", index, val.ToString()));
                index++;
            }

            userChoice = Console.ReadLine();
            try
            {
                if (!int.TryParse(userChoice, out chosenOption))
                {
                    throw new FormatException("Invalid input Please try again");
                }
                else if (chosenOption < 1 || chosenOption > m_VehiclesTypeAmount)
                {
                    throw new ArgumentException("Chosen type does not exist");
                }
                else
                {
                    vehicleToAdd = (VehicleCreator.eVehicleType)Enum.Parse(typeof(VehicleCreator.eVehicleType), userChoice);
                    if (vehicleToAdd.HasValue)
                    {
                        newVehicleToAdd = VehicleCreator.CreateNewVehicle(vehicleToAdd);
                    }
                    else
                    {
                        throw new FormatException("Chosen type does not exist");
                    }
                }

                CharacterizedNewVehicleWithUser(newVehicleToAdd);
                ownerName = GetOwnerName();
                ownerPhone = GetOwnerPhoneNumber();
                m_Garage.AddVehicleToTheGarage(new VehicleInGarage(ownerName, ownerPhone, newVehicleToAdd));
            }
            catch (FormatException i_FormatException)
            {
                Console.WriteLine(i_FormatException.Message);
                System.Threading.Thread.Sleep(5000);
                Console.Clear();
                AddVehicleToGarage();
            }
            catch (ArgumentException i_ArgumentException)
            {
                Console.WriteLine(i_ArgumentException.Message);
                System.Threading.Thread.Sleep(5000);
                Console.Clear();
                AddVehicleToGarage();
            }
            catch (ValueOutOfRangeException ValueOutOfRangeException)
            {
                Console.WriteLine(
                        "{0}{3}Minimum value:{1}{3}Maximum value:{2}",
                        ValueOutOfRangeException.Message,
                        ValueOutOfRangeException.MinValue,
                        ValueOutOfRangeException.MaxValue,
                        Environment.NewLine);
                System.Threading.Thread.Sleep(5000);
                Console.Clear();
                AddVehicleToGarage();
            }

            Console.Clear();
        }

        public string GetOwnerName()
        {
            string o_OwnerName = null;

            Console.WriteLine("Please enter the name of the vehicle owner: ");
            o_OwnerName = Console.ReadLine();

            return o_OwnerName;
        }

        private string GetOwnerPhoneNumber()
        {
            string o_PhoneNumberStr = null;
            int o_PhoneNumber = 0;

            try
            {
                Console.WriteLine("Please enter your phone number: ");
                o_PhoneNumberStr = Console.ReadLine();
                if (!int.TryParse(o_PhoneNumberStr, out o_PhoneNumber))
                {
                    throw new FormatException("Invalid phone number!");
                }
            }
            catch (FormatException i_FormatException)
            {
                Console.WriteLine(i_FormatException.Message);
                System.Threading.Thread.Sleep(5000);
                Console.Clear();
                GetOwnerPhoneNumber();
            }

            return o_PhoneNumberStr;
        }

        private void CharacterizedNewVehicleWithUser(Vehicle i_NewVehicleToAdd)
        {
                List<string> questionToAsk;
                List<string> vehiclePropertiesToSet = new List<string>();
            
                questionToAsk = i_NewVehicleToAdd.GetQuestionList();
                i_NewVehicleToAdd.AddWheelsToQustionList(questionToAsk);

                // Here is the proccese of asking and getting veahicles details from user
                Console.WriteLine("Please insert the following information by order:");
                foreach (string question in questionToAsk) 
                {
                    Console.WriteLine(question);
                    vehiclePropertiesToSet.Add(Console.ReadLine());
                }

                i_NewVehicleToAdd.SetPropertiesFromInput(vehiclePropertiesToSet);
        }

        public void PrintDetailsByLicenseUI()
        {
            string licenseNumber = null;

            try
            {
                licenseNumber = GetLiecenseNumber();
                Console.WriteLine(m_Garage.GetDetailsByLicense(licenseNumber));
                Console.WriteLine("Please Press Any Key for menu:");
                Console.ReadLine();
            }
            catch (ArgumentException i_ArgumentException)
            {
                Console.WriteLine(i_ArgumentException.Message);
                System.Threading.Thread.Sleep(5000);
                Console.Clear();
                PrintDetailsByLicenseUI();
            }
        }

        public void PrintAllVehicleListByFilterUI()
        {
            List<VehicleInGarage> statusedVehicles = null;
            eVehicleStatus o_RequestedStatus = CheckVehicleStatusUI();

            try
            {
                if (o_RequestedStatus == eVehicleStatus.None)
                {
                    statusedVehicles = m_Garage.VehiclesInGarage;
                }
                else
                {
                    statusedVehicles = m_Garage.GetVehicleByStatus(o_RequestedStatus);
                }
            }
            catch (ArgumentException i_ArgumentException)
            {
                Console.WriteLine(i_ArgumentException.Message);
                System.Threading.Thread.Sleep(5000);
                Console.Clear();
            }

            Console.WriteLine("=======The statused license list======");
                foreach (VehicleInGarage vehicleToprint in statusedVehicles)
                {
                    Console.WriteLine(vehicleToprint.LicenseNumber);
                }

                Console.WriteLine("======================================\n");
                Console.WriteLine("Please Press Any Key for menu:");
                Console.ReadLine();
        }

        public void ChangeVehicleStatusUI()
        {
            string licenseNumber = null;
            eVehicleStatus requestedStatus = CheckVehicleStatusUI();

            try
            {
                licenseNumber = GetLiecenseNumber();
                m_Garage.ChangeVehicleStatus(licenseNumber, requestedStatus);
                Console.Clear();
            }
            catch (ArgumentException i_ArgumentException)
            {
                Console.WriteLine(i_ArgumentException.Message);
                System.Threading.Thread.Sleep(5000);
                Console.Clear();
                ChangeVehicleStatusUI();
            }
        }

        public eVehicleStatus CheckVehicleStatusUI()
        {
            eVehicleStatus o_RequestedStatus = eVehicleStatus.None;

            Console.WriteLine("Please enter The status from the following:");
            Console.WriteLine("None, InRepair, Repaired, Paid");
            try
            {
                if (!Enum.TryParse(Console.ReadLine(), out o_RequestedStatus))
                {
                    throw new FormatException("Status invalid.");
                }
            }
            catch (FormatException)
            {
                Console.Clear();
                Console.WriteLine("Status was invalid try again");
                System.Threading.Thread.Sleep(5000);
                Console.Clear();
            }

            return o_RequestedStatus;
        }

        public void BlowAirToMaxUI()
        {
            VehicleInGarage vehicle = null;
            string licenseNumber = null;

            try
            {
                licenseNumber = GetLiecenseNumber();
                vehicle = m_Garage.SearchVehicleInTheGarage(licenseNumber);
                if (vehicle != null)
                {
                    vehicle.BlowWheelsToMax();
                }
                else
                {
                    throw new ArgumentException("The vehicle does not exists in the garage.");
                }
            }
            catch (ArgumentException i_ArgumentException)
            {
                Console.WriteLine(i_ArgumentException.Message);
                System.Threading.Thread.Sleep(5000);
                Console.Clear();
                BlowAirToMaxUI();
            }
        }

        public string GetLiecenseNumber() 
        {
            string o_licenseNumber = null;

            Console.WriteLine("Please enter license number:");
            o_licenseNumber = Console.ReadLine();

            return o_licenseNumber;
        }

        public void FuelVehicleUI()
        {
            string licenseNumber = null;
            eFuelType fuelType;
            float amountToFill = 0;

            try
            {
                licenseNumber = GetLiecenseNumber();
                Console.WriteLine("Please enter fuel type from the following:");
                Console.WriteLine("Octan98, Octan96, Octan95, Soler");
                if (Enum.TryParse(Console.ReadLine(), out fuelType))
                {
                    Console.WriteLine("Please enter amount to fill:");
                    if (!float.TryParse(Console.ReadLine(), out amountToFill))
                    {
                        throw new FormatException("Invalid amount!");
                    }

                    m_Garage.RefuelVehicle(licenseNumber, amountToFill, fuelType); 
                }
                else
                {
                    throw new FormatException("The fule type is invalid!");
                }
            }
            catch (FormatException i_FormatException)
            {
                Console.WriteLine(i_FormatException.Message);
                System.Threading.Thread.Sleep(5000);
                Console.Clear();
                FuelVehicleUI();
            }
            catch (ArgumentException i_ArgumentException)
            {
                Console.WriteLine(i_ArgumentException.Message);
                System.Threading.Thread.Sleep(5000);
                Console.Clear();
                DisplayMenu();
            }
            catch (ValueOutOfRangeException i_ValueOutOfRangeException)
            {
                Console.WriteLine(
                        "{0}{3}Minimum value:{1}{3}Maximum value:{2}",
                        i_ValueOutOfRangeException.Message,
                        i_ValueOutOfRangeException.MinValue,
                        i_ValueOutOfRangeException.MaxValue,
                        Environment.NewLine);
                System.Threading.Thread.Sleep(5000);
                Console.Clear();
                FuelVehicleUI();
            }
        }

        public void ChargeVehicleUI()
        {
            string licenseNumber = null;
            float amountToFill = 0;

            try
            {
                licenseNumber = GetLiecenseNumber();
                Console.WriteLine("Please enter battery time to add in minutes:");
                if (!float.TryParse(Console.ReadLine(), out amountToFill))
                {
                    throw new FormatException("Invalid amount!");
                }
                else
                {
                    m_Garage.ChargeBatteryVehicle(amountToFill, licenseNumber);
                }
            }
            catch (FormatException i_FormatException)
            {
                Console.WriteLine(i_FormatException.Message);
                System.Threading.Thread.Sleep(5000);
                Console.Clear();
                ChargeVehicleUI();
            }
            catch (ArgumentException i_ArgumentException)
            {
                Console.WriteLine(i_ArgumentException.Message);
                System.Threading.Thread.Sleep(5000);
                Console.Clear();
                DisplayMenu();
            }
            catch (ValueOutOfRangeException i_ValueOutOfRangeException)
            {
                Console.WriteLine(
                        "{0}{3}Minimum value:{1}{3}Maximum value:{2}",
                        i_ValueOutOfRangeException.Message,
                        i_ValueOutOfRangeException.MinValue,
                        i_ValueOutOfRangeException.MaxValue,
                        Environment.NewLine);
                System.Threading.Thread.Sleep(5000);
                Console.Clear();
                FuelVehicleUI();
            }
        }
    }
}
