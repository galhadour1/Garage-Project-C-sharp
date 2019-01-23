using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eEngineType
    {
        Fuel,
        Electric
    }

    public abstract class Engine
    {
        protected eEngineType m_EngineType;
        protected float m_EnergyPercent;

        public Engine(eEngineType i_EngineType)
        {
            m_EngineType = i_EngineType;
        }

        public eEngineType EngineType
        {
            get
            {
                return m_EngineType;
            }
        }

        public float EnergyPercent
        {
            get
            {
                return m_EnergyPercent;
            }

            set
            {
                m_EnergyPercent = value;
            }
        }

        public abstract float CurrentEnergyCapacity
        {
            get;
            set;
        }

        public abstract void AddFuelOrChargeBattery(float i_AmountFuelToAddOrHoursToCharge, eFuelType i_FuelType);

        public abstract string GetDetailsEngine();
    }
}
