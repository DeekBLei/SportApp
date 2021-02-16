using DataBaseService;
using DataTypes;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace ControlService
{
    public class WedstrijdSecretariaat
    {
        public MessageService Message { get; private set; }
        public ObservableCollection<VoetbalTeam> RangLijst { get { return this.SetRangLijst(); } } //= new ObservableCollection<VoetbalTeam>();
        private DataBaseRepository _dataBaseRepository;
        public WedstrijdSimulatie WedstrijdSimulatie { get; set; }

        public WedstrijdSecretariaat(DataBaseRepository dataBaseControl)
        {
            this.Message = new MessageService();
            this.WedstrijdSimulatie = new WedstrijdSimulatie(this);
            this._dataBaseRepository = dataBaseControl;
            SetRangLijst();
        }

        public void RegistreerDoelpunt(Speler speler, VoetbalTeam team, Speler verdediger, Wedstrijd CurrentWedstrijd)
        {
            
            _dataBaseRepository.RegistreerDoelpunt(speler, team, verdediger, CurrentWedstrijd);

        }

        internal void RegistreerRedding(Speler aanvaller, Speler verdediger)
        {
            
            this._dataBaseRepository.RegistreerRedding(aanvaller, verdediger);
        }

        public void PasWedtrijdAan(Wedstrijd bestaandeWedstrijd, Wedstrijd currentWedstrijdCopy)
        {
            if (ValidateWedstrijd(currentWedstrijdCopy))
            {
                _dataBaseRepository.PasWedstrijdAan(bestaandeWedstrijd, currentWedstrijdCopy);
                Message.SetMessageHeader($"Wedstijd {bestaandeWedstrijd.NaamToString} is aangepast");
            }
        }

        public ObservableCollection<VoetbalTeam> SetRangLijst()
        {
            var rangLijst = new ObservableCollection<VoetbalTeam>(
                _dataBaseRepository.GetAlleTeams()
                .OrderBy(team => team.WedstrijdSaldo)
                .ThenBy(team => team.DoelSaldo).Reverse());
            return rangLijst;
        }

        public void VerwijderWedstrijd(Wedstrijd wedstrijd)
        {
            if (!(wedstrijd.IsBezig || wedstrijd.IsGespeeld))
            {
                _dataBaseRepository.VerwijderItem(wedstrijd);
            }
        }

        public void ValideerWedstrijdEnVoegToe(Wedstrijd wedstrijd)
        {
            if (ValidateWedstrijd(wedstrijd))
            {
                _dataBaseRepository.VoegNieuwItemToe(wedstrijd);
            }

        }

        //simulate wedstrijd
        async public Task SpeelWedstrijdenAsync(IList<Wedstrijd> wedstrijden)
        {

            for (int i = 0; i < wedstrijden.Count; i++)
            {
                if (this.ValidateForPlay(wedstrijden[i]))
                {

                    _dataBaseRepository.FlagWedstrijdIsBezig(wedstrijden[i]);
                    Message.Clear();
                    await this.WedstrijdSimulatie.SpeelWedstrijdAsync(wedstrijden[i]);
                    _dataBaseRepository.VoegWedstrijdToeAanTeams(wedstrijden[i]);
                    _dataBaseRepository.FlagWedstrijdIsGespeeld(wedstrijden[i]);
                }
                else
                {
                    await Task.Delay(500);
                }
            }
        }

        public bool ValidateWedstrijd(Wedstrijd wedstrijd)
        {
            bool validated = true;
            Message.Clear();
            if (wedstrijd.IsBezig || wedstrijd.IsGespeeld)
            {
                validated = false;
                Message.SetMessageHeader($"Wedstijd {wedstrijd.NaamToString} is al gespeeld");
            }
            else
            {

                if (wedstrijd.ThuisTeam == null)
                {
                    validated = false;
                    Message.AddLineToMessageBody("Selecteer een thuisteam.");
                }
                if (wedstrijd.UitTeam == null)
                {
                    validated = false;
                    Message.AddLineToMessageBody("Selecteer een uitteam.");
                }
                if (wedstrijd.ThuisTeam?.Geslacht != wedstrijd.UitTeam?.Geslacht)
                {
                    validated = false;
                    Message.AddLineToMessageBody("U kunt alleen wedstrijden van teams met hetzelfde geslacht maken.");
                }
                if (wedstrijd.Datum < DateTimeOffset.Now)
                {
                    validated = false;
                    Message.AddLineToMessageBody("Selecteer een tijd in de toekomst.");
                }
            }
            return validated;
        }

        private bool ValidateForPlay(Wedstrijd wedstrijd)
        {
            bool validated = true;
            Message.Clear();
            if (wedstrijd.IsGespeeld)
            {
                Message.AddLineToMessageBody($"Wedstrijd {wedstrijd.NaamToString} is al gespeeld");
                validated = false;
            }
            if (wedstrijd.IsBezig)
            {
                Message.AddLineToMessageBody($"Wedstrijd {wedstrijd.NaamToString} is bezig");
                validated = false;
            }
            if (wedstrijd.ThuisTeam.TeamLeden.Count() < 4)
            {
                validated = false;
                Message.AddLineToMessageBody($"{wedstrijd.ThuisTeam.NaamToString} heeft te weinig spelers (min 4)");
            }
            if (wedstrijd.UitTeam.TeamLeden.Count() < 4)
            {
                validated = false;
                Message.AddLineToMessageBody($"{wedstrijd.UitTeam.NaamToString} heeft te weinig spelers (min 4)");
            }
            return validated;
        }

      
    }
}


