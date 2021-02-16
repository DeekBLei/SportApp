
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes
{
    
    [DebuggerDisplay("Name = {Speler.NaamToString} {Team.NaamToString}  {Wedstrijd.NaamToString}")]
    public class Doelpunt
    {
        [Key]
        public Guid DoelpuntId { get; private set; }
        public Speler Speler { get; private set; }
        public VoetbalTeam Team { get; private set; }
        public Wedstrijd Wedstrijd { get; private set; }

        public DateTimeOffset Datum { get; private set; } = DateTimeOffset.Now;
        public Doelpunt(Speler speler,Wedstrijd wedstrijd, VoetbalTeam team)
        {
            this.DoelpuntId = Guid.NewGuid();
            this.Wedstrijd = wedstrijd;
            this.Team = team;
            this.Speler = speler;
        }
        public Doelpunt()
        {
            this.DoelpuntId = Guid.NewGuid();
        }
        
    }
}
