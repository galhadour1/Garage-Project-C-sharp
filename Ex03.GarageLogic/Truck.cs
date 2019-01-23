using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private const int k_NumberOfWheels = 12;
        private const int k_MaxAirPressure = 32;
        private const float k_TankCapacity = 135;
        private const eFuelType k_TruckFuelType = eFuelType.Octan96;
        private bool m_CarriesToxicMetirials;
        private float m_CarryWeight;

        public Truck()
           : base(k_NumberOfWheels, k_MaxAirPressure)
        {
            m_Engine = new FuelEngine(eFuelType.Soler, k_TankCapacity);
        }

        public override List<string> GetQuestionList()
        {
            m_QuestionList.Add(@"Please enter the current energy capacity amount: ");
            m_QuestionList.Add(@"Please enter 'True'  if the truck the truck carries toxic metirials else enter 'False': ");
            m_QuestionList.Add(@"Please enter the maximum carrying weight allowed: ");

            return m_QuestionList;
        }

        public override string GetOtherDetails()
        {
            string o_OtherTruckDetails = null;

            o_OtherTruckDetails = string.Format(
@"Carry toxic : {0}
Max weight carrying  : {1} ",
m_CarriesToxicMetirials.ToString(),
m_CarryWeight.ToString());

            return o_OtherTruckDetails;
        }

        public override void SetPropertiesFromInput(List<string> i_PropertiesToAdd)
        {
            float currentEnergyCapacity = 0;

            base.SetPropertiesFromInput(i_PropertiesToAdd);
            m_Engine = new FuelEngine(k_TruckFuelType, k_TankCapacity);
            if (float.TryParse(i_PropertiesToAdd[2], out currentEnergyCapacity))
            {
                m_Engine.CurrentEnergyCapacity = currentEnergyCapacity;
                m_Engine.EnergyPercent = m_Engine.CurrentEnergyCapacity / 100;
                if (!bool.TryParse(i_PropertiesToAdd[3], out m_CarriesToxicMetirials))
                {
                    throw new FormatException("Please enter True or False for carries toxic metirials.");
                }

                if (!float.TryParse(i_PropertiesToAdd[4], out m_CarryWeight))
                {
                    throw new FormatException("Carry weight invalid.");
                }
            }
            else
            {
                throw new FormatException("Current energy capacity is invalid!");
            }
        }
    }
}
