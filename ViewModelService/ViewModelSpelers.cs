using ControlService;
using DataTypes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace ViewModelService
{
    public class ViewModelSpelers : ViewModelFilteredLists
    {
        /*
         * CurrentSpeler is de selected speler uit de FilteredSpelersList
         * CurrentSpelerCopy is de speler die wordt weergegeven in het gedetailleerde overzicht. 
         * Dit is altijd een copy van CurrentSpeler. Save en delete knoppen
         * worden aktief naar aanleiding van de staat van CurrentSpelerCopy en 
         * of deze dezelfde waardes bevat als CurrenSpeler.
               */

        //Commands
        public Command SaveSpeler_Click { get; set; }
        public Command ShowHideSelectTeamPopUp_Click { get; set; }
        public Command CancelNewSpeler_Click { get; set; }
        public Command CancelEditSpeler_Click { get; set; }
        public Command DeleteSpeler_Click { get; set; }
        public Command NewSpeler_Click { get; set; }


        //properties
        public LedenAdministratie LedenAdministratie { get; set; }
        private Speler _currentSpeler;
        public Speler CurrentSpeler
        {
            get => this._currentSpeler;
            set
            {
                if (value != null)
                {
                    this._currentSpeler = (Speler)value;
                    //kopieer CurrentSpeler
                    this.CurrentSpelerCopy.MemberwiseClone(CurrentSpeler);
                    this.OnPropertyChanged(nameof(CurrentSpeler));

                }
            }
        }

        private VoetbalTeam _selectedTeam;

        public VoetbalTeam SelectedTeam
        {
            get { return _selectedTeam; }
            set
            {if (value != null)
                {
                _selectedTeam = value;
                CurrentSpelerCopy.Team = (VoetbalTeam)value;
                OnPropertyChanged(nameof(SelectedTeam));
                //Bij selectie van een team wordt de lijst weer verborgen
                //
                
                ShowHideSelectTeamExecute();

                }
            }
        }


        private Speler _currentSpelerCopy;
        public Speler CurrentSpelerCopy
        {
            get => _currentSpelerCopy;
            set
            {
                _currentSpelerCopy = value;
                OnPropertyChanged(nameof(CurrentSpelerCopy));
            }
        }



        //Propertie waar Xaml aan bind om lijst teams wel of niet te tonen
        private string _teamSelectListVisible = "Collapsed";
        public string TeamSelectListVisible
        {
            get => _teamSelectListVisible;
            set
            {
                _teamSelectListVisible = value;
                OnPropertyChanged(nameof(TeamSelectListVisible));
            }
        }

        //Constructor 
        public ViewModelSpelers()
        {
            this.CurrentSpelerCopy = new Speler();
            this.LedenAdministratie = new LedenAdministratie(DataBaseRepository);
            SetCurrentSpeler();
            this.SaveSpeler_Click = new Command(SaveSpelerExecute, () =>
            {
                if (CurrentSpeler != null)
                {
                    //Save button aktief als waardes CurrentSpeler en CurrrentSpelerCopy
                    //niet overeen komen
                    return !CurrentSpeler.HasSamePropValues(CurrentSpelerCopy);
                }
                else
                {
                    //aktief als er geen selectie is (dus als een nieuwe speler wordt gecreeert)
                    return true;
                }
            }); //TODO validate
            FilterChanged += SetCurrentSpeler;
            this.CancelEditSpeler_Click = new Command(CancelEditSpelerExecute, () =>
            {
                if (CurrentSpeler != null)
                {
                    //Cancel button aktief als waardes CurrentSpeler en CurrrentSpelerCopy
                    //niet overeen komen
                    return !CurrentSpeler.HasSamePropValues(CurrentSpelerCopy);
                }
                else
                {
                    //aktief als er geen selectie is (dus als een nieuwe speler wordt gecreeert)
                    return true;
                }
            });
            this.ShowHideSelectTeamPopUp_Click = new Command(ShowHideSelectTeamExecute, () => true);
            this.DeleteSpeler_Click = new Command(DeleteSpelerExecute, () =>
            {
                if (CurrentSpeler != null)
                {
                    //Delete button aktief als waardes CurrentSpeler en CurrrentSpelerCopy
                    //wel overeen komen
                    return CurrentSpeler.HasSamePropValues(CurrentSpelerCopy);
                }
                else
                {
                    //inactief als er geen selectie is
                    return false;
                }
            });
            //Maak nieuwe Speler
            this.NewSpeler_Click = new Command(NewSpelerExecute, () => true);
            // Gebruik het PropertyChanged event van het object(speler) om staat van buttons te triggeren.
            this.CurrentSpelerCopy.PropertyChanged += DeleteSpeler_Click.TriggerCanExecuteChanged;
            this.CurrentSpelerCopy.PropertyChanged += CancelEditSpeler_Click.TriggerCanExecuteChanged;
            this.CurrentSpelerCopy.PropertyChanged += SaveSpeler_Click.TriggerCanExecuteChanged;
        }

        //methods 

        private void NewSpelerExecute()
        {
            //Maak een nieuwe speler aan en plaats als selectie
            //en dus een kopie ervan in CurrentSpelerCopy.
            Speler temp = new Speler();
            CurrentSpeler = temp;

        }

        private void UpdateButtonState()
        {
            DeleteSpeler_Click?.TriggerCanExecuteChanged();
            CancelEditSpeler_Click?.TriggerCanExecuteChanged();
            SaveSpeler_Click?.TriggerCanExecuteChanged();
        }
        private void DeleteSpelerExecute()
        {
            LedenAdministratie.VerwijderSpeler(CurrentSpeler);
            SetCurrentSpeler();
            UpdateButtonState();

        }
        private void CancelEditSpelerExecute()
        {
            this.CurrentSpelerCopy.MemberwiseClone(CurrentSpeler);
            UpdateButtonState();
        }

        private void ShowHideSelectTeamExecute()
        {
            // Laat teamlijst wel of niet zien
            if (TeamSelectListVisible == "Visible")
            {
                TeamSelectListVisible = "Collapsed";
            }
            else
            { TeamSelectListVisible = "Visible"; }
        }




        async public void SaveSpelerExecute()
        {
            //Als de CurrentSpeler nog niet bestaat betreft het een nieuwe Speler.
            //Pas currentSpeler aan naar de nieuwe waarde van currentSpelerCopy
            //en save naar de db. 
            if (!FilteredSpelersList.Contains(CurrentSpeler))
            {
                CurrentSpeler.MemberwiseClone(CurrentSpelerCopy);
                LedenAdministratie.VoegNieuweSpelerToe(CurrentSpeler);
                //CurrentSpeler = FilteredSpeleresList?.Where(c=>c.PersoonId == CurrentSpelerCopy.PersoonId).FirstOrDefault();
            }
            else
            // Als CurrentSpeler wel bestaat pas deze dan aan.
            {
                //als CurrentSpeler verschillend is van CurrentSpelerCopy
                if (!CurrentSpeler.HasSamePropValues(CurrentSpelerCopy))
                {
                    await LedenAdministratie.PasSpelerAanAsync(CurrentSpeler, CurrentSpelerCopy);
                    
                }
            }
            //om een of andere reden werd team van CurrentSpelerCopy op null gezet 
             //   CurrentSpelerCopy.MemberwiseClone(CurrentSpeler);
            UpdateButtonState();
        }

        //initialiseer selectie speler
        private void SetCurrentSpeler()
        {
            if (FilteredSpelersList.Any())
            {
                this.CurrentSpeler = FilteredSpelersList.FirstOrDefault();
            }
            //als de lijst leeg is
            else
            {
                CurrentSpeler = new Speler();
            }
            UpdateButtonState();
        }
    }
}
