using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;


namespace DataTypes
{

    [DebuggerDisplay("Name = {NaamToString}")]
    public class Wedstrijd : BaseINotify, IComparable<Wedstrijd>, INotifyPropertyChanged, ISearchable, INaamToString, IGeslacht, IMemberwiseClone<Wedstrijd>, IDelete
    {
        private Misc.GeslachtEnum _geslacht;

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
        [Key]
        public Guid WedstrijdId { get; set; }

        //Mark as deleted
        public bool Deleted { get; set; }

        public string NaamToString
        {
            get { return this.ThuisTeam.Naam + " " + this.UitTeam.Naam; }
        }
        public string Uitslag
        {
            get
            {
                if (IsGespeeld || IsBezig)
                {
                    return $"{this.ThuisTeamDoelpunten} - {this.UitTeamDoelpunten}";

                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                OnPropertyChanged(nameof(Uitslag));
            }
        }
        public string Naam { get => ThuisTeam.Naam + " " + UitTeam.Naam; }

        private VoetbalTeam _thuisTeam;
        public VoetbalTeam ThuisTeam
        {
            get => this._thuisTeam;
            set
            {
                this._thuisTeam = value;
                this.OnPropertyChanged(nameof(ThuisTeam));
            }
        }

        private VoetbalTeam _uitTeam;
        public VoetbalTeam UitTeam
        {
            get => this._uitTeam;
            set
            {
                this._uitTeam = value;
                this.OnPropertyChanged(nameof(UitTeam));
            }
        }

        public VoetbalTeam Winnaar
        {
            get
            {
                if (this.ThuisTeamDoelpunten > UitTeamDoelpunten)
                {
                    return ThuisTeam;
                }
                if (this.ThuisTeamDoelpunten < UitTeamDoelpunten)
                {
                    return UitTeam;
                }
                else
                {
                    var gelijkSpel = new VoetbalTeam();
                    return gelijkSpel;
                }
            }
        }

        public int ThuisTeamDoelpunten
        {
            get
            {
                if (Doelpunten != null)
                {

                    return Doelpunten.Count(doelpunt => doelpunt.Team == ThuisTeam);
                }
                else
                { return 0; }
            }
        }


        private ObservableCollection<Doelpunt> _doelPunten = new ObservableCollection<Doelpunt>();
        public ObservableCollection<Doelpunt> Doelpunten
        {
            get { return _doelPunten; }
            set
            {
                _doelPunten = value;
                this.OnPropertyChanged(nameof(Uitslag));
            }
        }



        public int UitTeamDoelpunten
        {
            get
            {
                if (Doelpunten != null)
                {

                    return Doelpunten.Count(dp => dp.Team == UitTeam);
                }
                else
                {
                    return 0;
                }
            }

        }

        private DateTimeOffset _datum;
        public DateTimeOffset Datum
        {
            get => this._datum; set
            {
                this._datum = value;
                OnPropertyChanged(nameof(Datum));
            }
        }
        private TimeSpan _tijd;
        public TimeSpan Tijd
        {
            get => this._tijd; set
            {
                this._tijd = value;
                OnPropertyChanged(nameof(Tijd));
            }
        }
        private bool _isGespeeld;
        public bool IsGespeeld
        {
            get => _isGespeeld; set
            {
                this._isGespeeld = value;
                this.OnPropertyChanged(nameof(Uitslag));
            }
        }
        public bool _isBezig;
        public bool IsBezig
        {
            get => _isBezig;
            set
            {
                if (this.IsGespeeld == true)
                {
                    this._isBezig = false;
                }
                else
                {
                    this._isBezig = value;
                }
                OnPropertyChanged(nameof(Uitslag));
            }
        }

        public override string ToString()
        {
            return this.ThuisTeam.Naam + " " + this.UitTeam.Naam;
        }

        public Wedstrijd()
        {
            this.ThuisTeam = new VoetbalTeam();
            this.UitTeam = new VoetbalTeam();
            this.WedstrijdId = Guid.NewGuid();
            this.IsGespeeld = false;
            this.Doelpunten = new ObservableCollection<Doelpunt>();

        }

        public int CompareTo(Wedstrijd other)
        {
            if (other == null)
                return 1;
            else
                return this.Datum.CompareTo(other.Datum);
        }

        public List<string> SearchablePropertiesToList()
        {
            return new List<string>
           {
             //  nameof(Datum),
               nameof(ThuisTeam),
               nameof(UitTeam),
               nameof(Uitslag),
               nameof(Datum)
           };
        }

        public void MemberwiseClone(Wedstrijd objectToClone)
        {
            this.IsBezig = objectToClone.IsBezig;
            this.IsGespeeld = objectToClone.IsGespeeld;
            this.ThuisTeam = objectToClone.ThuisTeam;
            this.UitTeam = objectToClone.UitTeam;
            this.Doelpunten.Clear();
            foreach (var item in objectToClone.Doelpunten)
            {

                this.Doelpunten.Add(item);
            }
            this.Datum = objectToClone.Datum;
            this.Tijd = objectToClone.Tijd;
            this.Geslacht = objectToClone.Geslacht;
            this.WedstrijdId = objectToClone.WedstrijdId;
        }
    }
}
