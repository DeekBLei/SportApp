using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    //Maakt een string van de naam
    //TODO: Had beter de ToString method kunnen overriden
    public interface INaamToString
    {
        string NaamToString { get; }
    }
}
