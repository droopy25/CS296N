using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Models
{
    public class CityViewModel
    {
        public List<City> CityList = new List<City>();
        public int SelectedCityID { get; set; }
        public IEnumerable<SelectListItem> CityIEnum
        {
            get
            {
                return new SelectList(CityList, "CityID", "CityName");
            }
        }
    }
}