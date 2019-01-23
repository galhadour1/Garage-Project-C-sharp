using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eVehicleStatus
    {
        None,
        InRepair,
        Repaired,
        Paid
    }

    public class VehicleInGarage
    {
        private readonly string m_OwnerName;
        private readonly string m_OwnerPhoneNumber;
        private Vehicle m_VehicleInGarage;
        private eVehicleStatus m_VehicleStatus = eVehicleStatus.InRepair;

        public VehicleInGarage(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_VehicleToRepair)
        {
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNumber = i_OwnerPhoneNumber;
            m_VehicleInGarage = i_VehicleToRepair;
        }

        public Vehicle VehicleToRepair
        {
            get
            {
                return m_VehicleInGarage;
            }

            set
            {
                m_VehicleInGarage = value;
            }
        }

        public string PhoneNumber
        {
            get
            {
                return m_OwnerPhoneNumber;
            }
        }

        public eVehicleStatus VehicleStatus
        {
            get
            {
                return m_VehicleStatus;
            }

            set
            {
                m_VehicleStatus = value;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return m_VehicleInGarage.LicenseNumber;
            }
        }

        public string OwnerName
        {
            get
            {
                return m_OwnerName;
            }
        }

        public void BlowWheelsToMax()
        {
            foreach (Wheel wheel in m_VehicleInGarage.Wheels)
            {
                wheel.BlowWheelToMax();
            }
        }
    }
}
