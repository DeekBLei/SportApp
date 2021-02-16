
using Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes
{
    
    public class Coach : Persoon , IMemberwiseClone<Coach>, IHasSamePropValues<Coach>
    { 
        
        
        private VoetbalTeam _team;
        public VoetbalTeam Team
        {
            get => _team;
            set
            {
                
                this._team = value;
                this.OnPropertyChanged(nameof(Team));
            }
        }
        public void MemberwiseClone(Coach other)
        {
            this.VoorNaam = other.VoorNaam;
            this.AchterNaam = other.AchterNaam;
            //this.Doelpunten = other.Doelpunten;
            this.GeboorteDatum = other.GeboorteDatum;
            this.Geslacht = other.Geslacht;
            this.Team = other.Team;
            this.Ervaring = other.Ervaring;


        }
        public bool HasSamePropValues(Coach other)
        {
            bool sameValues = false;
            if (this.VoorNaam == other.VoorNaam &&
                this.AchterNaam == other.AchterNaam &&
                    //this.Doelpunten == other.Doelpunten &&
                    this.GeboorteDatum == other.GeboorteDatum &&
                    this.Geslacht == other.Geslacht &&
                    this.Team == other.Team &&
                    this.Ervaring == other.Ervaring)
            {
                sameValues = true;
            }
            return sameValues;
        }


        public int Ervaring { get; set; }

        public Coach()
        {
          
        }

        public Coach(string voorNaam, string achterNaam, DateTime geboorteDatum, string geslacht) : base(voorNaam, achterNaam, geboorteDatum, geslacht)
        {
         
        }

       
        public override List<string> SearchablePropertiesToList()
        {
            return new List<string>
            {
                nameof(this.VoorNaam),
                nameof(this.AchterNaam),
                //nameof(this.GeboorteDatum),
               // nameof(this.Geslacht),
                nameof(this.Team),
           

        };
        }

       
    }
}

