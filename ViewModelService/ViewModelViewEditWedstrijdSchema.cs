using DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlService;
using Interfaces;
using System.Reflection;
using System.Collections;
using DataBaseService;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using System.Collections.ObjectModel;
using System.Threading;


namespace ViewModelService
{
    //Deze class maakt gebruikt van de InEditingMode of bepaalde knoppen wel of niet actief(zichtbaar) zijn

    public class ViewModelViewEditWedstrijdSchema : ViewModelFilteredLists
    {
        //Commands
        public Command SaveNieuweWedstrijd { get; set; }
        public Command SpeelWedstrijden { get; set; }
        public Command PasWedstrijdAan { get; set; }
        public Command Delete { get; set; }
        public Command Cancel { get; set; }


        //Constructor
        public ViewModelViewEditWedstrijdSchema()
        {
            this.WedstrijdSecretariaat = new WedstrijdSecretariaat(DataBaseRepository);
            this.TeamListCombobox = new CommuncatingCombobox<VoetbalTeam>(FilteredTeamList.ToList());
            this.ResetUI();
            this.SaveNieuweWedstrijd = new Command(this.SaveNieuweWedstrijdExecute, () => !InEditingMode);
            this.PasWedstrijdAan = new Command(this.PasWedstrijdAanExecute, () => InEditingMode);
            this.Delete = new Command(this.DeleteWedstrijdExecute, () => InEditingMode);
            this.Cancel = new Command(this.CancelExecute, () => true);
            this.SpeelWedstrijden = new Command(this.SpeelWedstrijdenExecute, () => true);
            
            //TeamListCombobox laat de teams in de dropdown zien na keuze van een team voor bijv 
            //ThuisTeam is dit Team niet meer beschikbaar in de lijst van UitTeam
            this.TeamListCombobox.InputLeftChanged += () => TeamListCombobox.UpdateListRight(TeamListCombobox.InputLeft);
            this.TeamListCombobox.InputLeftChanged += UpdateComboBox;
            this.TeamListCombobox.InputRightChanged += () => TeamListCombobox.UpdateListLeft(TeamListCombobox.InputRight);
            this.TeamListCombobox.InputRightChanged += UpdateComboBox;
            this.TeamListCombobox.SourceListChanged += () => TeamListCombobox.UpdateListLeft(TeamListCombobox.InputRight);
            this.TeamListCombobox.SourceListChanged += () => TeamListCombobox.UpdateListRight(TeamListCombobox.InputLeft);
            this.TeamListCombobox.SourceListChanged += UpdateComboBox;
        }

        //Properties
        public string Tegen { get { return "tegen"; } }
        public WedstrijdSecretariaat WedstrijdSecretariaat { get; set; }
        private bool _editingMode;
        private bool InEditingMode
        {
            get => _editingMode;
            set
            {
                _editingMode = value;
                OnPropertyChanged(nameof(this.InEditingMode));
                this.SaveNieuweWedstrijd?.TriggerCanExecuteChanged();
                this.PasWedstrijdAan?.TriggerCanExecuteChanged();
                this.Delete?.TriggerCanExecuteChanged();
            }
        }

        public string ShowEditingMode
        {
            get
            {
                if (InEditingMode)
                {
                    return "Pas wedstrijd aan:";
                }
                else
                {
                    return "Plan nieuwe wedstrijd";
                }
            }
        }

        public bool WedstrijdEditable { get => WedstrijdSecretariaat.ValidateWedstrijd(CurrentWedstrijdCopy); }

        public Wedstrijd CurrentWedstrijd
        {
            set
            {
                if (value != null)
                {
                    this.CurrentWedstrijdCopy.MemberwiseClone(value);
                    TeamListCombobox.InputLeft = CurrentWedstrijdCopy.ThuisTeam.NaamToString;
                    TeamListCombobox.InputRight = CurrentWedstrijdCopy.UitTeam.NaamToString;
                    this.OnPropertyChanged(nameof(CurrentWedstrijdCopy));
                    InEditingMode = true;
                }
            }
        }

        public Wedstrijd _currentWedstrijdCopy;
        public Wedstrijd CurrentWedstrijdCopy
        {
            get => _currentWedstrijdCopy;
            set
            {
                this._currentWedstrijdCopy = value;
                this.OnPropertyChanged(nameof(CurrentWedstrijdCopy));
            }
        }
        private string _invoerFeedbackmessage;
        public string InvoerFeedbackMessage
        {
            get => _invoerFeedbackmessage;
            set
            {
                this._invoerFeedbackmessage = value;
                OnPropertyChanged(nameof(InvoerFeedbackMessage));
            }
        }

        public List<string> GeslachtsList { get; set; } = Misc.GeslachtsList; // Lijst voor combobox


        private CommuncatingCombobox<VoetbalTeam> _TeamListCombobox;
        public CommuncatingCombobox<VoetbalTeam> TeamListCombobox
        {
            get { return _TeamListCombobox; }
            private set
            {
                _TeamListCombobox = value;
                OnPropertyChanged(nameof(TeamListCombobox));
            }
        }

        //Methods
        private void UpdateComboBox()
        {
            CommuncatingCombobox<VoetbalTeam> temp = TeamListCombobox.ShallowCopy();
            this.TeamListCombobox = temp;
            OnPropertyChanged(nameof(TeamListCombobox));
        }



        async private void SpeelWedstrijdenExecute()
        {
            await this.WedstrijdSecretariaat.SpeelWedstrijdenAsync(DataBaseRepository.GetAlleWedstrijden());
        }

        private void DeleteWedstrijdExecute()
        {
            //verwijder wedstrijd als deze bestaat
            if (!CurrentWedstrijdCopy.IsGespeeld || !CurrentWedstrijdCopy.IsBezig)
            {
                DataBaseRepository.VerwijderItem(DataBaseRepository.GetAlleWedstrijden().Single(p => p.WedstrijdId == CurrentWedstrijdCopy.WedstrijdId));
                this.InvoerFeedbackMessage = $"Wedstrijd {CurrentWedstrijdCopy.NaamToString} is verwijderd";
                this.ResetUI();
            }
            else
            {
                if (!CurrentWedstrijdCopy.IsGespeeld)
                {

                    this.InvoerFeedbackMessage = $"Wedstrijd {CurrentWedstrijdCopy.NaamToString} is bezig en kan niet worden verwijderd";
                }
                else
                {
                    this.InvoerFeedbackMessage = $"Wedstrijd {CurrentWedstrijdCopy.NaamToString} is al gespeeld en kan niet worden verwijderd";
                }
            }


        }

        private void CancelExecute()
        {
            ResetUI();
        }

        private void SaveNieuweWedstrijdExecute()
        {

            this.CurrentWedstrijdCopy.ThuisTeam = (VoetbalTeam)DataBaseRepository.GetAlleTeams().Where(p => p.NaamToString == this.TeamListCombobox.InputLeft).FirstOrDefault();
            this.CurrentWedstrijdCopy.UitTeam = (VoetbalTeam)DataBaseRepository.GetAlleTeams().Where(p => p.NaamToString == this.TeamListCombobox.InputRight).FirstOrDefault();
            this.CurrentWedstrijdCopy.Geslacht = Geslacht;
            this.CurrentWedstrijdCopy.Datum = this.CurrentWedstrijdCopy.Datum.Date + this.CurrentWedstrijdCopy.Tijd;

            //Valideer wedstrijdCopy
            if (WedstrijdSecretariaat.ValidateWedstrijd(CurrentWedstrijdCopy))
            {
                //pas wedstrijd aan als deze bestaat
                var bestaandeWedstrijd = DataBaseRepository.GetAlleWedstrijden().Where(p => p.WedstrijdId == CurrentWedstrijdCopy.WedstrijdId).FirstOrDefault();
                if (bestaandeWedstrijd != null)
                {
                    WedstrijdSecretariaat.PasWedtrijdAan(bestaandeWedstrijd, CurrentWedstrijdCopy);
                }
                //Als wedstrijd niet bestaat voeg deze dan toe
                else
                {
                    WedstrijdSecretariaat.ValideerWedstrijdEnVoegToe(CurrentWedstrijdCopy);
                }
            }
            //reset UI
            this.ResetUI();
        }

        private void PasWedstrijdAanExecute()
        {
            SaveNieuweWedstrijdExecute();
        }

        //reset UI
        private void ResetUI()
        {
            this.CurrentWedstrijdCopy = new Wedstrijd();
            this.TeamListCombobox.ResetInput();
            this.UpdateComboBox();
            this.CurrentWedstrijdCopy.Datum = DateTimeOffset.Now;
            this.CurrentWedstrijdCopy.Tijd = this.CurrentWedstrijdCopy.Datum.TimeOfDay;
            InEditingMode = false;
        }
    }
}
