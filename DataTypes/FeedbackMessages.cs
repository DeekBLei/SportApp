
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes
{
    public static class FeedbackMessages
    {
      
        public static string PasWedstrijdAan { get ; private set ; }
        
        public static string GetWedstrijdWordtGespeeld(Wedstrijd wedstrijd)
        {
            return $"{wedstrijd.NaamToString} wordt nu gespeeld";
        }

        public static string GetWedstrijdTussenStand(string uitslag)
        {
            return $"De stand is {uitslag}";
        }
        public static string GetWedstrijdIsAlGespeeld(String wedstrijdNaam)
        {
            return $"{wedstrijdNaam} is al gespeeld";
        }
       
    }
}
