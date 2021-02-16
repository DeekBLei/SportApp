using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes
{
    
    public class VoetbalTeamWedstrijd
    {
        public VoetbalTeamWedstrijd() { }
        [Key]
        public int Id { get; set; }
        public VoetbalTeam VoetbalTeam { get; set; }
        public Wedstrijd Wedstrijd { get; set; }
    }
}
