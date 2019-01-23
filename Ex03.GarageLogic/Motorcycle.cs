using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eTypeLicense
    {
        B,
        A2,
        A1,
        A,
    }

    public class Motorcycle : Vehicle
    {
        private const int k_NumberOfWheels = 2;
        private const int k_MaxAirPressure = 33;
        private const eFuelType k_MotorcycleFuelType = eFuelType.Octan95;
        private const float k_MaxBatteryCapacity = 2.7f;
        private const float k_TankCapacity = 5.5f;
        protected int m_EngineCapacity;
        private eTypeLicense m_TypeLicense;

        public Motorcycle() : base(k_NumberOfWheels, k_MaxAirPressure)
        {
        }

        public override List<string> GetQuestionList()
        {
            m_QuestionList.Add(@"Please enter 'True' if the motorcycle has electric engine or 'False' the motorcycle has Fuel engine: ");
            m_QuestionList.Add(@"Please enter the current energy capacity amount: ");
            m_QuestionList.Add(@"Please enter the type of licence. Please choose from the following: B, A2, A1, A: ");
            m_QuestionList.Add(@"Please enter the engine capacity: ");

            return m_QuestionList;
        }

        public override string GetOtherDetails()
        {
            string o_OtherMotorcycleDetails = " ";

            o_OtherMotorcycleDetails = string.Format(
@"License type = {0}
Engine capacity = {1}",
m_TypeLicense.ToString(),
m_EngineCapacity.ToString());

            return o_OtherMotorcycleDetails;
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
                    m_Engine = new FuelEngine(k_MotorcycleFuelType, k_TankCapacity);
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

            if (!Enum.TryParse(i_PropertiesToAdd[4], out m_TypeLicense))
            {
                throw new FormatException("Type license invalid.");
            }

            if (!int.TryParse(i_PropertiesToAdd[5], out m_EngineCapacity))
            {
                throw new FormatException("Engine capacity invalid.");
            }
        }
    }
}
