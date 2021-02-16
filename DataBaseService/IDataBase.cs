using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTypes;
using Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataBaseService
{
    public interface IDataBase
    {


        ICollection<Persoon> AllePersonen { get; set; }

        ICollection<VoetbalTeam> AlleTeams { get; set; }


        ICollection<Speler> AlleSpelers { get; set; }
        ICollection<Coach> AlleCoaches { get; set; }

        ICollection<Wedstrijd> AlleWedstrijden
        { get; set; }

        ICollection<Doelpunt> AlleDoelpunten
        { get; set; }

    }
}
