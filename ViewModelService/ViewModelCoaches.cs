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
    public class ViewModelCoaches : ViewModelFilteredLists
    {
        /* 
         * CurrentCoach is de selected speler uit de FilteredCoachesList
         * CurrentCoachCopy is de speler die wordt weergegeven in het gedetailleerde overzicht. 
         * Dit is altijd een copy van CurrentCoach. Save en delete knoppen
         * worden aktief naar aanleiding van de staat van CurrentCoachCopy en 
         * of deze dezelfde waardes bevat als CurrenCoach.
               */

        //Commands
        public Command SaveCoach_Click { get; set; }
        public Command ShowHideSelectTeamPopUp_Click { get; set; }
        public Command CancelNewCoach_Click { get; set; }
        public Command CancelEditCoach_Click { get; set; }
        public Command DeleteCoach_Click { get; set; }
        public Command NewCoach_Click { get; set; }


        //properties
        public LedenAdministratie LedenAdministratie { get; set; }
        private Coach _currentCoach;
        public Coach CurrentCoach
        {
            get => this._currentCoach;
            set
            {
                if (value != null)
                {
                    this._currentCoach = (Coach)value;
                    //kopieer CurrentCoach
                    this.CurrentCoachCopy.MemberwiseClone(CurrentCoach);
                    this.OnPropertyChanged(nameof(CurrentCoach));
                }
            }
        }

        private VoetbalTeam _selectedTeam;

        public VoetbalTeam SelectedTeam
        {
            get { return _selectedTeam; }
            set
            {     if (value != null)
                {
                _selectedTeam = value;
                CurrentCoachCopy.Team = (VoetbalTeam)value;
                OnPropertyChanged(nameof(SelectedTeam));
                //Bij selectie van een team wordt de lijst weer verborgen
           
                    ShowHideSelectTeamExecute();

                }
               
            }
        }


        private Coach _currentCoachCopy;
        public Coach CurrentCoachCopy
        {
            get => _currentCoachCopy;
            set
            {
                _currentCoachCopy = value;
                OnPropertyChanged(nameof(CurrentCoachCopy));
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
        public ViewModelCoaches()
        {
            this.CurrentCoachCopy = new Coach();
            this.LedenAdministratie = new LedenAdministratie(DataBaseRepository);
            SetCurrentCoach();
            this.SaveCoach_Click = new Command(SaveCoachExecute, () =>
            {
                if (CurrentCoach != null)
                {
                    //Save button aktief als waardes CurrentCoach en CurrrentCoachCopy
                    //niet overeen komen
                    return !CurrentCoach.HasSamePropValues(CurrentCoachCopy);
                }
                else
                {
                    //aktief als er geen selectie is (dus als een nieuwe speler wordt gecreeert)
                    return true;
                }
            }); //TODO validate
            FilterChanged += SetCurrentCoach;
            this.CancelEditCoach_Click = new Command(CancelEditCoachExecute, () =>
            {
                if (CurrentCoach != null)
                {
                    //Cancel button aktief als waardes CurrentCoach en CurrrentCoachCopy
                    //niet overeen komen
                    return !CurrentCoach.HasSamePropValues(CurrentCoachCopy);
                }
                else
                {
                    //aktief als er geen selectie is (dus als een nieuwe speler wordt gecreeert)
                    return true;
                }
            });
            this.ShowHideSelectTeamPopUp_Click = new Command(ShowHideSelectTeamExecute, () => true);
            this.DeleteCoach_Click = new Command(DeleteCoachExecute, () =>
            {
                if (CurrentCoach != null)
                {
                    //Delete button aktief als waardes CurrentCoach en CurrrentCoachCopy
                    //wel overeen komen
                    return CurrentCoach.HasSamePropValues(CurrentCoachCopy);
                }
                else
                {
                    //inaktief als er geen selectie is
                    return false;
                }
            });
            //Maak nieuwe Coach
            this.NewCoach_Click = new Command(NewCoachExecute, () => true);
            // Gebruik het PropertyChanged event van het object(speler) om staat van buttons te triggeren.
            this.CurrentCoachCopy.PropertyChanged += DeleteCoach_Click.TriggerCanExecuteChanged;
            this.CurrentCoachCopy.PropertyChanged += CancelEditCoach_Click.TriggerCanExecuteChanged;
            this.CurrentCoachCopy.PropertyChanged += SaveCoach_Click.TriggerCanExecuteChanged;
        }

        //methods 

        private void NewCoachExecute()
        {
            //Maak een nieuwe coach aan en plaats als selectie venster
            //en dus een kopie ervan in CurrentCoachCopy.
            Coach temp = new Coach();
            CurrentCoach = temp;

        }

        private void UpdateButtonState()
        {
            DeleteCoach_Click?.TriggerCanExecuteChanged();
            CancelEditCoach_Click?.TriggerCanExecuteChanged();
            SaveCoach_Click?.TriggerCanExecuteChanged();
        }
        private void DeleteCoachExecute()
        {
            LedenAdministratie.VerwijderCoach(CurrentCoach);
            SetCurrentCoach();
            UpdateButtonState();

        }
        private void CancelEditCoachExecute()
        {
            this.CurrentCoachCopy.MemberwiseClone(CurrentCoach);
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




        public void SaveCoachExecute()
        {
            //Als de CurrentCoach nog niet bestaat betreft het een nieuwe Coach.
            //Pas currentCoach aan naar de nieuwe waarde van currentCoachCopy
            //en save naar de db. 
            if (!FilteredCoachesList.Contains(CurrentCoach))
            {
                CurrentCoach.MemberwiseClone(CurrentCoachCopy);
                LedenAdministratie.VoegNieuweCoachToe(CurrentCoach);
                //CurrentCoach = FilteredCoachesList?.Where(c=>c.PersoonId == CurrentCoachCopy.PersoonId).FirstOrDefault();

            }
            else
            // Als CurrentCoach wel bestaat pas deze dan aan.
            {
                //als CurrentCoach verschillend is van CurrentCoachCopy
                if (!CurrentCoach.HasSamePropValues(CurrentCoachCopy))
                {
                    LedenAdministratie.PasCoachAan(CurrentCoach, CurrentCoachCopy);
                }
            }
           // CurrentCoachCopy.MemberwiseClone(CurrentCoach);
            UpdateButtonState();
        }

        //initialiseer selectie speler
        private void SetCurrentCoach()
        {
            if (FilteredCoachesList.Any())
            {
                this.CurrentCoach = FilteredCoachesList.FirstOrDefault();
            }
            //als de lijst leeg is
            else
            {
                CurrentCoach = new Coach();
            }
            UpdateButtonState();
        }
    }
}
