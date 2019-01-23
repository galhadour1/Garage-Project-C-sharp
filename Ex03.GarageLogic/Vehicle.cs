using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected readonly List<string> m_QuestionList;
        protected readonly List<Wheel> r_Wheels;
        protected string m_ModelName;
        protected string m_LicenseNumber;
        protected Engine m_Engine;

        public Vehicle(int i_NumberOfWheels, float i_MaxAirPressure)
        {
            m_QuestionList = new List<string>();
            HandleVehicleQustionList(m_QuestionList);
            r_Wheels = new List<Wheel>(i_NumberOfWheels);
            for (ushort i = 1; i < i_NumberOfWheels + 1; i++) 
            {
                r_Wheels.Add(new Wheel(i_MaxAirPressure, i));
            }
        }

        public static void HandleVehicleQustionList(List<string> i_QuestionList)
        {
            i_QuestionList.Add(@"Please enter Model Name: ");
            i_QuestionList.Add(@"Please enter License Number: ");
        }

        public abstract List<string> GetQuestionList();

        public void AddWheelsToQustionList(List<string> i_QuestionList)
        {
            foreach (Wheel wheel in r_Wheels)
            {
                wheel.AddWheelToQustionList(i_QuestionList);
            }
        }

        public abstract string GetOtherDetails();

        public string LicenseNumber
        {
            get
            {
                return m_LicenseNumber;
            }

            set
            {
                m_LicenseNumber = value;
            }
        }

        public List<Wheel> Wheels
        {
            get
            {
                return r_Wheels;
            }
        }

        public eEngineType EngineType
        {
            get
            {
                return m_Engine.EngineType;
            }
        }

        public Engine Engine
        {
            get
            {
                return m_Engine;
            }
        }

        public string ModelName
        {
            get
            {
                return m_ModelName;
            }

            set
            {
                m_ModelName = value;
            }
        }

        public string GetWheelsDetails()
        {
            StringBuilder o_WheelsDetails = new StringBuilder();

            foreach (Wheel wheel in r_Wheels) 
            {
                o_WheelsDetails.Append(wheel.GetWheelDetails());
            }

            return o_WheelsDetails.ToString();
        }

        public virtual void SetPropertiesFromInput(List<string> i_PropertiesToAdd)
        {
            int index = i_PropertiesToAdd.Count;
            float airPressure = 0;

            m_ModelName = i_PropertiesToAdd[0];
            m_LicenseNumber = i_PropertiesToAdd[1];
            foreach (Wheel wheel in r_Wheels)
            {
                if (float.TryParse(i_PropertiesToAdd[index - 1], out airPressure))
                {
                    wheel.CheckWheelAirPressure(airPressure);
                    wheel.AirPressure = airPressure;
                }
                else
                {
                    throw new FormatException("Air pressure is invalid!");
                }
               
                wheel.ManufacturerName = i_PropertiesToAdd[index - 2];
                index -= 2;
            }
        }
    }
}
