﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private readonly float m_MaxValue;
        private readonly float m_MinValue;

        public ValueOutOfRangeException(string i_Message, float i_MinValue, float i_MaxValue)
            : base(i_Message)
        {
            m_MinValue = i_MinValue;
            m_MaxValue = i_MaxValue;
        }

        public float MaxValue
        {
            get
            {
                return m_MaxValue;
            }
        }

        public float MinValue
        {
            get
            {
                return m_MinValue;
            }
        }
    }
}
