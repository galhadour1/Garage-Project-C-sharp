using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eNumberOfDoors
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
    }

    public enum eCarColor
    {
        Yellow,
        Black,
        White,
        Blue
    }

    public class Car : Vehicle
    {
        private const int k_NumberOfWheels = 4;
        private const int k_MaxAirPressure = 30;
        private const eFuelType k_CarFuelType = eFuelType.Octan98;
        private const float k_TankCapacity = 42;
        private const float k_MaxBatteryCapacity = 2.5f;
        private eCarColor m_CarColor;
        private eNumberOfDoors m_NumberOfDoors;
   
        public Car() : base(k_NumberOfWheels, k_MaxAirPressure)
        {
        }

        public override List<string> GetQuestionList()
        {
            m_QuestionList.Add(@"Please enter 'True' if the car has electric engine or 'False' the car has Fuel engine: ");
            m_QuestionList.Add(@"Please enter the current energy capacity amount: ");
            m_QuestionList.Add(@"Please enter the car color. 
Please choose from the following: Yellow, Black, White and Blue: ");
            m_QuestionList.Add(@"Please enter the number of doors in the car: Two, Three, Four or Five: ");

            return m_QuestionList;
        }

        public override string GetOtherDetails()
        {
            string o_OtherCarDetails = null;

            o_OtherCarDetails = string.Format(
@"Number of doors = {0}
Car color = {1}",
m_NumberOfDoors.ToString(),
m_CarColor.ToString());

            return o_OtherCarDetails;
        }

        public override void SetPropertiesFromInput(List<string> i_PropertiesToAdd)
        {
            bool boolProperty;
            float currentEnergyCapacity = 0;

            base.SetPropertiesFromInput(i_PropertiesToAdd);
            if (!bool.TryParse(i_PropertiesToAdd[2], out boolProperty))
            {
                throw new FormatException("You need to enter True or False!");
            }
            else if (float.TryParse(i_PropertiesToAdd[3], out currentEnergyCapacity))
            {
                if (!Convert.ToBoolean(i_PropertiesToAdd[2]))
                {
                    m_Engine = new FuelEngine(k_CarFuelType, k_TankCapacity);
                    m_Engine.CurrentEnergyCapacity = currentEnergyCapacity;
                    m_Engine.EnergyPercent = m_Engine.CurrentEnergyCapacity / 100;
                }
                else
                {
                    m_Engine = new ElectricEngine(k_MaxBatteryCapacity);
                    m_Engine.CurrentEnergyCapacity = currentEnergyCapacity; 
                    m_Engine.EnergyPercent = m_Engine.CurrentEnergyCapacity / 100;
                }
            }
            else
            {
                throw new FormatException("Current energy capacity is invalid!");
            }

            if (!Enum.TryParse(i_PropertiesToAdd[4], out m_CarColor))
            {
                throw new FormatException("Color invalid.");
            }

            if (!eNumberOfDoors.TryParse(i_PropertiesToAdd[4], out m_NumberOfDoors)) 
            {
                throw new FormatException("Number of doors is invalid.");
            }
        }
    }
}
