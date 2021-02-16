using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes
{
    public class Misc
    {
        public enum GeslachtEnum { Man, Vrouw };

        public static List<string> GeslachtsList
        {
            get => Enum.GetValues(typeof(Misc.GeslachtEnum)).Cast<Misc.GeslachtEnum>().ToList().Select(p => p.ToString()).ToList();
        }
    }
}
