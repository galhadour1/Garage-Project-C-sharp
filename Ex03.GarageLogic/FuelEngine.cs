using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eFuelType
    {
        Octan98,
        Octan96,
        Octan95,
        Soler,
        None
    }

    public class FuelEngine : Engine
    {
        private const int k_MinFuelAmount = 0;
        private readonly float r_MaxFuelAmount;
        private readonly eFuelType r_FuelType;
        private float m_CurrentFuelAmount;

        public FuelEngine(eFuelType i_FuelType, float i_MaxFuelAmount)
           : base(eEngineType.Fuel)
        {
            r_FuelType = i_FuelType;
            r_MaxFuelAmount = i_MaxFuelAmount;
        }

        public eFuelType FuelType
        {
            get
            {
                return r_FuelType;
            }
        }

        public float MaxFuelAmount
        {
            get
            {
                return r_MaxFuelAmount;
            }
        }

        public override float CurrentEnergyCapacity
        {
            get
            {
                return m_CurrentFuelAmount;
            }

            set
            {
                if (value > r_MaxFuelAmount || value < 0) 
                {
                    throw new ValueOutOfRangeException("Energy capacity amount above max capacity amount.", k_MinFuelAmount, r_MaxFuelAmount);
                }
                else
                {
                    m_CurrentFuelAmount = value;
                }
            }
        }

        public override void AddFuelOrChargeBattery(float i_AmountFuelToAddOrHoursToCharge, eFuelType i_FuelType)
        {
            if (r_FuelType == i_FuelType)
            {
                if ((i_AmountFuelToAddOrHoursToCharge + m_CurrentFuelAmount <= r_MaxFuelAmount) && (i_AmountFuelToAddOrHoursToCharge > 0))
                {
                    m_CurrentFuelAmount += i_AmountFuelToAddOrHoursToCharge;
                }
                else
                {
                    throw new ValueOutOfRangeException("You can select only in range", k_MinFuelAmount, r_MaxFuelAmount - m_CurrentFuelAmount);
                }
            }
            else
            {
                throw new ArgumentException("Incorrect fuel type");
            }
        }

        public override string GetDetailsEngine()
        {
            string o_DetailsFuelEngine = null;

            o_DetailsFuelEngine = string.Format(
@"Fuel type = {0}
Current fuel amount = {1}",
m_EngineType.ToString(),
m_CurrentFuelAmount.ToString());

            return o_DetailsFuelEngine;
        }
    }
}
