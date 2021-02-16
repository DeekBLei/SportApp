using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    //implementeert een lijst met doorzoekbare propertiesd voor het 
    //gebruik in de search functie van de Gui
    public interface ISearchable
    {       
        List<string> SearchablePropertiesToList();
    }
}
