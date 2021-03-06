﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final.Models
{
    public class State
    {
        public int StateID { get; set; }
        public int CityID { get; set; }
        private string state;
        public string StateName
        {
            get
            {
                return state;
            }
            set
            {
                state = value.ToUpper();
            }
        }
        private List<City> cities = new List<City>();
        public List<City> Cities
        {
            get { return cities; }
        }
    }
}