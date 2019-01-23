﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class Program 
    {
        public static void Main()
        {
            Garage garage = new Garage();
            GarageManager garageManager = new GarageManager(garage);

            garageManager.RunGarage();
        }
    }
}
