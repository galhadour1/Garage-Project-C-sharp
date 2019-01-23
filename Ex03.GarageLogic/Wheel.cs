using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private const float k_MinAirPressure = 0;
        private readonly ushort m_IndexWheel = 1;
        private readonly float m_MaxAirPressure;
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;

        internal Wheel(float i_MaxAirPressure, ushort i_IndexWheel)
            : this(k_MinAirPressure, i_MaxAirPressure,  i_IndexWheel)
        {
        }

        internal Wheel(float i_AirPressure, float i_MaxAirPressure, ushort i_IndexWheel)
        {
            m_MaxAirPressure = i_MaxAirPressure;
            m_IndexWheel = i_IndexWheel;
            if (i_AirPressure > m_MaxAirPressure)
            {
                throw new ArgumentException("Desired air pressure above max air pressure.");
            }
            else
            {
                m_CurrentAirPressure = i_AirPressure;
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return m_MaxAirPressure;
            }
        }

        public float MinAirPressure
        {
            get
            {
                return k_MinAirPressure;
            }
        }

        public string ManufacturerName
        {
            get
            {
                return m_ManufacturerName;
            }

            set
            {
                m_ManufacturerName = value;
            }
        }

        public float AirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }

            set 
            {
                m_CurrentAirPressure = value;
            }
        }

        public void AddAirToWheel(float i_AirToAdd)
        {
            if (m_CurrentAirPressure + i_AirToAdd <= m_MaxAirPressure)
            {
                m_CurrentAirPressure += i_AirToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException("Air pressure can be only in range ", k_MinAirPressure, m_MaxAirPressure);
            }
        }

        public void BlowWheelToMax()
        {
            m_CurrentAirPressure = m_MaxAirPressure;
        }

        public string GetWheelDetails()
        {
            string o_WheelDetails = null;

            o_WheelDetails = string.Format(
@"
Wheel number {0}:
Manufacture name = {1}
Current air pressure = {2}
Max air pressure = {3}
",
m_IndexWheel.ToString(),
m_ManufacturerName,
m_CurrentAirPressure.ToString(),
m_MaxAirPressure.ToString());

            return o_WheelDetails;
        }

        public void AddWheelToQustionList(List<string> i_QuestionList)
        {
            i_QuestionList.Add(@"Please enter wheel number " + m_IndexWheel.ToString() + " manifacturer:");
            i_QuestionList.Add(@"Please enter wheel number " + m_IndexWheel.ToString() + " current air presure:");
        }

        public void CheckWheelAirPressure(float i_airPressure)
        {
            if (i_airPressure < k_MinAirPressure || i_airPressure > MaxAirPressure)
            {
                throw new ValueOutOfRangeException("Air pressure is invalid!", k_MinAirPressure, MaxAirPressure);
            }
        }
    }
}
