using ControlService;
using DataBaseService;
using DataTypes;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace ViewModelService
    //Werking class is hetzelfde als ViewModel Speler en ViewModel Coach
{
    public class ViewModelViewEditTeams : ViewModelFilteredLists
    {
        //Commands
        public Command ShowHideSpelersPopUp_Click { get; set; }
        public Command ShowHideCoachesPopUp_Click { get; set; }
        public Command SaveTeam_Click { get; set; }
        public Command CancelEditTeam_Click { get; set; }
        public Command DeleteTeam_Click { get; set; }
        public Command NewTeam_Click { get; set; }

        //properties
        private LedenAdministratie LedenAdministratie { get; set; }
        public WedstrijdSecretariaat WedstrijdSecretariaat { get; set; }

        //geselecteerde speler uit popup list om voor speler toevoegen
        private Speler _selectedSpeler;
        public Speler VoegSelectedSpelerToeAanTeam
        {
            get => _selectedSpeler;
            set
            {
                if (value != null)
                {
                    this._selectedSpeler = value;
                    //als de speler nog niet in het team zit voeg deze toe
                    if (!CurrentTeamCopy.TeamLeden.Any(s => s == value))
                    {
                        VoegSpelerToeAanTeamExecute(value);
                    }
                    OnPropertyChanged(nameof(VoegSelectedSpelerToeAanTeam));
                }
            }
        }

        private Coach _selectedCoach;
        public Coach VoegSelectedCoachToeAanTeam
        {
            get => _selectedCoach;
            set
            {
                if (value != null)
                {
                    //als de coach nog niet in het team zit voeg deze toe
                    this._selectedCoach = value;
                    if (!CurrentTeamCopy.Coaches.Any(s => s == value))
                    {
                        VoegCoachToeAanTeamExecute(value);
                    }
                    OnPropertyChanged(nameof(VoegSelectedCoachToeAanTeam));
                }
            }
        }

        private int _rang;

        public int Rang
        {
            get { return _rang; }
            set { _rang = value; }
        }

        private ObservableCollection<Speler> _beschikbareSpelerList;

        public ObservableCollection<Speler> BeschikbareSpelerList
        {
            get
            {

                //_beschikbareSpelerList.Clear();
                //foreach (Speler speler in FilteredSpelersList/*.Except(CurrentTeamCopy.TeamLeden)*/)
                //{
                //    _beschikbareSpelerList.Add(speler);
                //}
                //foreach (Speler speler in FilteredSpelersList)
                //{
                //    if (!CurrentTeamCopy.TeamLeden.Any(s=>s==speler))
                //    {
                //       
                //    }

                //}
                // return _beschikbareSpelerList;
                return FilteredSpelersList;
            }
            //set { _beschikbareSpelerList = value; }
        }


        private bool _popUpSpelerVisible;
        public bool PopUpSpelerVisible
        {
            get => _popUpSpelerVisible; set
            {
                _popUpSpelerVisible = value;
                OnPropertyChanged(nameof(PopUpSpelerVisible));
            }
        }

        private bool _popupCoachesVisible;
        public bool PopUpCoachesVisible
        {
            get => _popupCoachesVisible;
            set
            {
                this._popupCoachesVisible = value;
                OnPropertyChanged(nameof(PopUpCoachesVisible));
            }
        }

        private Speler _removeSpelerFromCurrentTeamCopy;
        public Speler RemoveSpelerFromCurrentTeamCopy
        {
            get { return _removeSpelerFromCurrentTeamCopy; }
            set
            {
                this._removeSpelerFromCurrentTeamCopy = (Speler)value;
                this.CurrentTeamCopy.TeamLeden.Remove(value);
                // this.LedenAdministratie.VerwijderSpelerUitTeam(value, CurrentTeamCopy);
                UpdateButtonState();

            }
        }

        private Coach _removeCoachFromCurrentTeamCopy;
        public Coach RemoveCoachFromCurrentTeamCopy
        {
            get { return _removeCoachFromCurrentTeamCopy; }
            set
            {
                this._removeCoachFromCurrentTeamCopy = (Coach)value;
                this.CurrentTeamCopy.Coaches.Remove(value);
                //this.LedenAdministratie.VerwijderCoachuitTeam(value, CurrentTeamCopy);
                UpdateButtonState();

            }
        }

        private VoetbalTeam _currentTeam;
        public VoetbalTeam CurrentTeam
        {
            get
            {
                return _currentTeam;
            }
            set
            {
                if (value != null)
                {
                    this._currentTeam = value;
                    //kopier CurrentTeam naar CurrenTeamCopy 
                    this.CurrentTeamCopy.MemberwiseClone(CurrentTeam);
                    this.OnPropertyChanged(nameof(CurrentTeam));
                }
            }
        }
        private VoetbalTeam _currentTeamCopy;
        public VoetbalTeam CurrentTeamCopy
        {
            get => _currentTeamCopy;
            set
            {
                if (value != null)
                {
                    _currentTeamCopy = value;
                    OnPropertyChanged(nameof(CurrentTeamCopy));
                }
            }
        }

        //constructor

        public ViewModelViewEditTeams()
        {
            this.CurrentTeamCopy = new VoetbalTeam();
            this._beschikbareSpelerList = new ObservableCollection<Speler>();
            SetCurrentTeam();
            this.LedenAdministratie = new LedenAdministratie(DataBaseRepository);
            this.WedstrijdSecretariaat = new WedstrijdSecretariaat(DataBaseRepository);
            this.ShowHideSpelersPopUp_Click = new Command(ShowHideSpelersPopUpExecute, () => true);
            this.ShowHideCoachesPopUp_Click = new Command(ShowHideCoachesPopUpExecute, () => true);
            this.PopUpSpelerVisible = false;

            this.SaveTeam_Click = new Command(SaveTeamExecute, () =>
            {
                if (CurrentTeam != null)
                {
                    //Save button is actief als waardes CurrentSpeler en CurrrentSpelerCopy
                    //niet overeen komen
                    return !CurrentTeam.HasSamePropValues(CurrentTeamCopy);
                }
                else
                {
                    //actief als er geen selectie is (dus als een nieuwe speler wordt gecreert)
                    return true;
                }
            }); //TODO validate

            this.CancelEditTeam_Click = new Command(CancelEditSpelerExecute, () =>
            {
                if (CurrentTeam != null)
                {
                    //Cancel button aktief als waardes CurrentSpeler en CurrrentSpelerCopy
                    //niet overeen komen
                    return !CurrentTeam.HasSamePropValues(CurrentTeamCopy);
                }
                else
                {
                    //aktief als er geen selectie is (dus als een nieuwe speler wordt gecreeert)
                    return true;
                }
            });

            this.DeleteTeam_Click = new Command(DeleteTeamExecute, () =>
            {
                if (CurrentTeam != null)
                {
                    //Delete button aktief als waardes CurrentSpeler en CurrrentSpelerCopy
                    //wel overeen komen
                    return CurrentTeam.HasSamePropValues(CurrentTeamCopy);
                }
                else
                {
                    //inactief als er geen selectie is
                    return false;
                }
            });

            this.NewTeam_Click = new Command(NewTeamExecute, () => true);
            // Gebruik het PropertyChanged event van het object(speler) om staat van buttons te triggeren.
            this.CurrentTeamCopy.PropertyChanged += DeleteTeam_Click.TriggerCanExecuteChanged;
            this.CurrentTeamCopy.PropertyChanged += CancelEditTeam_Click.TriggerCanExecuteChanged;
            this.CurrentTeamCopy.PropertyChanged += SaveTeam_Click.TriggerCanExecuteChanged;
            FilterChanged += SetCurrentTeam;
        }


        //methods 

        private void ShowHideCoachesPopUpExecute()
        {

            PopUpCoachesVisible = !PopUpCoachesVisible;

        }

        private void ShowHideSpelersPopUpExecute()
        {

            PopUpSpelerVisible = !PopUpSpelerVisible;

        }

        private void VoegSpelerToeAanTeamExecute(Speler speler)
        {

            CurrentTeamCopy.TeamLeden.Add(speler);
            UpdateButtonState();




        }

        private void VoegCoachToeAanTeamExecute(Coach coach)
        {

            this.CurrentTeamCopy.Coaches.Add(coach);

            UpdateButtonState();
        }



        private void NewTeamExecute()
        {
            //Maak een nieuw team aan en plaats als selectie
            //en dus een kopie ervan in CurrentTeamCopy.
            VoetbalTeam temp = new VoetbalTeam();
            CurrentTeam = temp;

        }

        private void UpdateButtonState()
        {
            DeleteTeam_Click?.TriggerCanExecuteChanged();
            CancelEditTeam_Click?.TriggerCanExecuteChanged();
            SaveTeam_Click?.TriggerCanExecuteChanged();
        }
        private void DeleteTeamExecute()
        {
            LedenAdministratie.VerwijderTeam(CurrentTeam);
            SetCurrentTeam();


        }
        private void CancelEditSpelerExecute()
        {
            this.CurrentTeamCopy.MemberwiseClone(CurrentTeam);
            UpdateButtonState();
        }

        async public void SaveTeamExecute()
        {
            //Als de CurrentTeam nog niet bestaat betreft het een nieuwe Team.
            //Pas currentTeam aan naar de nieuwe waarde van currentTeamCopy
            //en save naar de db. 
            if (!FilteredTeamList.Contains(CurrentTeam))
            {
                CurrentTeam.MemberwiseClone(CurrentTeamCopy);
                LedenAdministratie.VoegNieuwTeamToe(CurrentTeam);
                CurrentTeam = FilteredTeamList?.Where(t => t.TeamId == CurrentTeamCopy.TeamId).FirstOrDefault();
            }
            else
            // Als CurrentTeam wel bestaat pas deze dan aan.
            {
                //als CurrentTeam verschillend is van CurrentTeamCopy
                if (!CurrentTeam.HasSamePropValues(CurrentTeamCopy))
                {
                    await LedenAdministratie.PasTeamAanAsync(CurrentTeam, CurrentTeamCopy);
                }
            }
            UpdateButtonState();
        }

        //initialiseer selectie speler
        private void SetCurrentTeam()
        {
            if (FilteredTeamList.Any())
            {
                this.CurrentTeam = FilteredTeamList.FirstOrDefault();
            }
            //als de lijst leeg is
            else
            {
                CurrentTeam = new VoetbalTeam();
            }
            UpdateButtonState();
        }
    }
}
