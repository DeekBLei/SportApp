using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Interfaces;


namespace DataTypes
{
    
    [DebuggerDisplay("Name = {NaamToString}")]
    public class Team : BaseINotify, ISearchable, IComparable<Team>, IGeslacht, INaamToString, IDelete
    {

        //delegates
        //public Func<int> GetRang;
        //public Func<int> GetWedstrijdSaldo;
        //properties 
        private Misc.GeslachtEnum _geslacht;
        public int Ervaring
        {
            get
            {
                int ervaring=0;
                foreach (var speler in this.TeamLeden)
                {
                    ervaring += speler.Ervaring;
                }
                return ervaring;
            }
        }

        public string Geslacht
        {
            get => _geslacht.ToString();
            set
            {
                if (value == Misc.GeslachtEnum.Man.ToString())
                {
                    this._geslacht = Misc.GeslachtEnum.Man;
                }
                if (value == Misc.GeslachtEnum.Vrouw.ToString())
                {
                    this._geslacht = Misc.GeslachtEnum.Vrouw;
                }
                this.OnPropertyChanged(nameof(Geslacht));
            }
        }
        private ObservableCollection<Speler> _teamleden;
        public ObservableCollection<Speler> TeamLeden
        {
            get
            {
                return _teamleden;
            }
            set
            {
                this._teamleden = value;
            }
        }

        public string NaamToString
        {
            get { return this.Naam + " " + Geslacht; }
        }

        public string Punten { get => WedstrijdSaldo + "." + DoelSaldo; }
        public int DoelSaldo { get { return this.Doelpunten.Count; } }
        public int WedstrijdSaldo
        {
            get
            {

                var i = 0;
                foreach (var vbw in VoetbalTeamWedstrijds)
                {
                    if (vbw.Wedstrijd.Winnaar != null && vbw.Wedstrijd.Winnaar == this)
                    {
                        i++;
                    }
                }
                return i;
            }
        }

        private ObservableCollection<VoetbalTeamWedstrijd> _voetbalTeamWedstrijds = new ObservableCollection<VoetbalTeamWedstrijd>();
        public ObservableCollection<VoetbalTeamWedstrijd> VoetbalTeamWedstrijds
        {
            get { return _voetbalTeamWedstrijds; }
            set { _voetbalTeamWedstrijds = value; }
        }

        public int TeamGrootte { get => TeamLeden.Count(); }

        private ObservableCollection<Doelpunt> _doelPunten = new ObservableCollection<Doelpunt>();
        public ObservableCollection<Doelpunt> Doelpunten
        {
            get { return _doelPunten; }
            set
            {
                _doelPunten = value;
                OnPropertyChanged(nameof(Doelpunten));
            }
        }

        private ObservableCollection<Coach> _coaches;
        public ObservableCollection<Coach> Coaches
        {
            get
            {
                return _coaches;
            }
            set
            {
                this._coaches = value;
                this.OnPropertyChanged(nameof(Coaches));
            }
        }

        [Key]
        public Guid TeamId { get;  set; }
        private string _naam;
        public string Naam
        {
            get => _naam;
            set
            {
                this._naam = value;
                OnPropertyChanged(nameof(Naam));
            }
        }

        public bool Deleted { get; set; }

        public enum Spelerfunctie
        {
            spits = 1,
            spitsRechts,
            spitsLinks,
            rechtsvoor,
            linksvoor,
            middenmid,
            linksmidden,
            rechtsmidden,
            linksachter,
            rechtsAchter,
            midAchter,
            laatsteMan,
            keeper,
            Reserve1,
            reserve2,
            reserve3,
            none
        }

        public Team(string naam, Misc.GeslachtEnum geslacht)
        {
            this.Geslacht = geslacht.ToString();
            this.Naam = naam;
            TeamLeden = new ObservableCollection<Speler>();
            Coaches = new ObservableCollection<Coach>();
        }

        public Team()
        {
            TeamLeden = new ObservableCollection<Speler>();
            Coaches = new ObservableCollection<Coach>();
            TeamId = Guid.NewGuid();
        }

        public List<string> SearchablePropertiesToList()
        {
            return new List<string>
            {
                nameof(this.Naam),
            nameof(this.TeamLeden),
            //nameof(this.DoelSaldo),
             nameof(this.Coaches)
            };
        }

        public int getDoelpunten()
        {
            return Doelpunten.Count;
        }

        public int CompareTo(Team other)
        {
            return other == null ? 1 : this.Naam.CompareTo(other.Naam);
        }

    }
}
