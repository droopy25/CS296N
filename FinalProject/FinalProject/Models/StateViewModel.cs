using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Models
{
    public class StateViewModel
    {
        public List<State> StateList = new List<State>();
        public int SelectedStateID { get; set; }
        public IEnumerable<SelectListItem> StateIEnum
        {
            get
            {
                return new SelectList(StateList, "StateID", "StateName");
            }
        }
    }
}