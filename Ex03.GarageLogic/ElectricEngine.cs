using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        private const float k_MinBatteryTime = 0;
        private readonly float r_MaxBatteryTime;
        private float m_CurrentBatteryTime;

        public ElectricEngine(float i_MaxBatteryTime)
          : base(eEngineType.Electric)
        {
            r_MaxBatteryTime = i_MaxBatteryTime;
        }

        public override float CurrentEnergyCapacity
        {
            get
            {
                return m_CurrentBatteryTime;
            }

            set
            {
                if (value > r_MaxBatteryTime || value < 0) 
                {
                    throw new ValueOutOfRangeException("Energy capacity amount above max capacity amount.", k_MinBatteryTime, r_MaxBatteryTime);
                }
                else
                {
                    m_CurrentBatteryTime = value;
                }
            }
        }

        public override void AddFuelOrChargeBattery(float i_AmountFuelToAddOrHoursToCharge, eFuelType i_FuelType)
        {
                if ((i_AmountFuelToAddOrHoursToCharge + m_CurrentBatteryTime <= r_MaxBatteryTime) && (i_AmountFuelToAddOrHoursToCharge > 0))
                {
                    m_CurrentBatteryTime += i_AmountFuelToAddOrHoursToCharge;
                }
                else
                {
                    throw new ValueOutOfRangeException("You can select only in range", k_MinBatteryTime, r_MaxBatteryTime - m_CurrentBatteryTime);
                }
        }

        public override string GetDetailsEngine()
        {
            string o_DetailsElectricEngine = null;

            o_DetailsElectricEngine = string.Format(
@"Current battery time = {0}",
m_CurrentBatteryTime.ToString());

            return o_DetailsElectricEngine;
        }
    }
}
