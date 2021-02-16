using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Interfaces;
using System.Reflection;
using DataTypes;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;

namespace DataTypes
{
    
    [DebuggerDisplay("Name = {NaamToString}")]
    public abstract class Persoon : BaseINotify, ISearchable, INaamToString, IGeslacht, IDelete
    {
        //Abstracte class om coach en spelers te modelleren
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

        private string _voorNaam;
        public string VoorNaam
        {
            get => _voorNaam;
            
            set 
            {
                this._voorNaam = value;
                this.OnPropertyChanged(nameof(VoorNaam));
            }
        }
        private string _achterNaam;
        public string AchterNaam
        {
            get => _achterNaam;
            set
            {
                this._achterNaam = value;
                this.OnPropertyChanged(nameof(AchterNaam));
            }
        }
        private DateTime _geboorteDatum;
        public DateTime GeboorteDatum
        {
            get => _geboorteDatum;
            set
            {
                this._geboorteDatum = value;
                this.OnPropertyChanged(nameof(GeboorteDatum));
                this.OnPropertyChanged(nameof(Leeftijd));
            }
        }


        public int Leeftijd
        {
            get
            {
                if (GeboorteDatum != null)
                {
                    DateTime now = DateTime.Now;
                    int age = now.Year - GeboorteDatum.Year;
                    if (now.Month < GeboorteDatum.Month)
                    {
                        age--;
                    }
                    if (now.Month == GeboorteDatum.Month && now.Day < GeboorteDatum.Day)
                    {
                        age--;
                    }
                    return age;
                }
                else
                {
                    return 0;
                }
            }

            set
            {
                if (value != 0)
                {

                DateTime now = DateTime.Now;
                this._geboorteDatum = now.AddYears(-value);
                }
                else
                {
                    this._geboorteDatum = DateTime.Now;
                }
                this.OnPropertyChanged(nameof(GeboorteDatum));
                this.OnPropertyChanged(nameof(Leeftijd));
            }

        }

        public string NaamToString
        {
            get { return this.VoorNaam + " " + this.AchterNaam; }
        }


        public abstract List<string> SearchablePropertiesToList();
        [Key]
        public Guid PersoonId { get; set; }
        public bool Deleted { get; set; }

        public Persoon()
        {
            this.PersoonId = Guid.NewGuid();
            this._geboorteDatum = DateTime.Now;

        }
       
        public Persoon(string voorNaam, string achterNaam, DateTime geboorteDatum, string geslacht)
        {

            //AlleSpelers.Add(this);
            this.VoorNaam = voorNaam;
            this.AchterNaam = achterNaam;
            this.GeboorteDatum = geboorteDatum;
            this.Geslacht = geslacht;
            this.PersoonId = Guid.NewGuid();
                    }


    }
}

