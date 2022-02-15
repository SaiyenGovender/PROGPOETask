using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROGPOETask.Models
{
    
    public class UserAccount
    {

        //login
        public string username { get; set; }

        public string password { get; set; }

        public string gender { get; set; }

        //monthly tab

        public string gross { get; set; } 

        public string tax { get; set; }

        public string groc { get; set; }

        public string water { get; set; }

        public string travel { get; set; }

        public string phones { get; set; }

        public string other { get; set; }

        //monthly accomodation

        public string propPrice { get; set; }
        public string accDep { get; set; }
        public string accRate { get; set; }
        public string accMonths { get; set; }
        public string rent { get; set; }

        //monthly vehicles
        public Boolean vehCheck { get; set; }
        public string model { get; set; }
        public string vehPrice { get; set; }
        public string vehDep { get; set; }
        public string vehRate { get; set; }
        public string vehInsu { get; set; }

        //savings
        public string savTotal { get; set; }
        public string savYears { get; set; }
        public string savInt { get; set; }
        
    
        //totals
        public string saveMonthly { get; set; }
        public string incomeTotal { get; set; }
        public string accTotal { get; set; }
        public string vehTotal { get; set; }
        public string monthTotal { get; set; }


    }
}