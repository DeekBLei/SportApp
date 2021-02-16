using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    //implementeert een clone funtie voor het 
    //gebruik in de edit functie van de Gui
    public interface IMemberwiseClone<T>
    {
        void MemberwiseClone(T objectToClone);
    }
}
