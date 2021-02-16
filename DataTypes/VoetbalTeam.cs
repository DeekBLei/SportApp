using Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes
{
   
    public class VoetbalTeam : Team, IMemberwiseClone<VoetbalTeam>, IHasSamePropValues<VoetbalTeam>
    {


        public string SportSoort
        {
            get { return "voetbal"; }
        }

        //[Key]
        public Guid VoetbalTeamId { get;  set; }

        public VoetbalTeam(string naam, Misc.GeslachtEnum geslacht) : base(naam, geslacht)
        {
            this.VoetbalTeamId = new Guid();
        }

        public VoetbalTeam()
        {
            this.VoetbalTeamId = new Guid();
        }


        
        public void MemberwiseClone(VoetbalTeam other)
        {
                       this.Naam = other.Naam;
            this.Doelpunten.Clear();
            foreach (var item in other.Doelpunten)
            {
                this.Doelpunten.Add(item);
               
            }
            
            this.Coaches.Clear();
            foreach (var item in other.Coaches)
            {
                this.Coaches.Add(item);
            }
            this.TeamLeden.Clear();
            foreach (var item in other.TeamLeden)
            {
                this.TeamLeden.Add(item);
            }
            this.VoetbalTeamWedstrijds.Clear();
            foreach (var item in other.VoetbalTeamWedstrijds)
            {
                this.VoetbalTeamWedstrijds.Add(item);
            }
            this.Geslacht = other.Geslacht;
            
        }

        public bool HasSamePropValues(VoetbalTeam other)
        {
            bool hasSameValues = false;
            if (this.Naam == other.Naam &&
            this.Doelpunten.SequenceEqual(other.Doelpunten) &&
            this.Coaches.SequenceEqual(other.Coaches) &&
            this.TeamLeden.SequenceEqual(other.TeamLeden) &&
            this.VoetbalTeamWedstrijds.SequenceEqual(other.VoetbalTeamWedstrijds) &&
            this.Geslacht == other.Geslacht)
            {
                hasSameValues = true;
            }
            return hasSameValues;
        }
    }

  
}
