using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;


namespace DataTypes
{
    
    public class Speler : Persoon, IMemberwiseClone<Speler>, IHasSamePropValues<Speler>
    {
        private int _ervaring;
        public int Ervaring
        {
            get => _ervaring; 
            set
            {
                _ervaring = value;
                OnPropertyChanged(nameof(Ervaring));

            }
        }
        private VoetbalTeam _team;
        public VoetbalTeam Team
        {
            get => _team;
            set
            {
                if (value != null)
                {

                    // TeamId = value.TeamId;
                }
                this._team = value;
                this.OnPropertyChanged(nameof(Team));
            }
        }

        private ObservableCollection<Doelpunt> _doelPunten;
        public ObservableCollection<Doelpunt> Doelpunten
        {
            get { return _doelPunten; }
            set
            {
                _doelPunten = value;
                this.OnPropertyChanged(nameof(Doelpunten));
            }
        }

        public int DoelSaldo
        {
            get { return Doelpunten.Count; }
        }
       

        public Speler(string voorNaam, string achterNaam, DateTime geboorteDatum, string _geslacht) : base(voorNaam, achterNaam, geboorteDatum, _geslacht)
        {
            this.Doelpunten = new ObservableCollection<Doelpunt>();
        }

        public Speler()
        {
            this.Doelpunten = new ObservableCollection<Doelpunt>();
        }

        public bool HasSamePropValues(Speler other)
        {
            bool sameValues = false;
            if (this.VoorNaam == other.VoorNaam &&
                this.AchterNaam == other.AchterNaam &&
                    this.Doelpunten.SequenceEqual(other.Doelpunten) &&
                    this.GeboorteDatum == other.GeboorteDatum &&
                    this.Geslacht == other.Geslacht &&
                    this.Team == other.Team &&
                    this.Ervaring == other.Ervaring)
            {
                sameValues = true;
            }
            return sameValues;
        }

        public override List<string> SearchablePropertiesToList()
        {
            return new List<string>
            {
                nameof(this.VoorNaam),
                nameof(this.AchterNaam),
               // nameof(this.GeboorteDatum),
               // nameof(this.Geslacht),
                nameof(this.Team)
            };
        }
        public void MemberwiseClone(Speler other)
        {
            this.VoorNaam = other.VoorNaam;
            this.AchterNaam = other.AchterNaam;
            this.Doelpunten.Clear();
            foreach (var item in other.Doelpunten)
            {
             this.Doelpunten.Add(item);

            }
            this.GeboorteDatum = other.GeboorteDatum;
            this.Geslacht = other.Geslacht;
            this.Team = other.Team;
            this.Ervaring = other.Ervaring;


        }






    }
}
