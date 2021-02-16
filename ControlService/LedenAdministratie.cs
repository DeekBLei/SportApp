using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseService;
using DataTypes;


namespace ControlService
{
    //Deze class valideert spelers, coaches en teams en de 
    //toewijzingen van coaches en spelers naar een team
    public class LedenAdministratie
    {
        private readonly DataBaseRepository _dataBaseRepository;

        public MessageService ValidationMessage { get; private set; }

        public LedenAdministratie(DataBaseRepository dataBaseRepository)
        {
            _dataBaseRepository = dataBaseRepository;
            ValidationMessage = new MessageService();
        }



        async public void VoegSpelerToeAanTeam(Speler speler, VoetbalTeam team)
        {
            if (!team.TeamLeden.Any(s => s == speler))
            {
                await _dataBaseRepository.VoegSpelerToeAanTeam(speler, team);
            }
            else
            {
                ValidationMessage.AddLineToMessageBody("Deze Speler heeft u al toegevoegd");
            }

        }
        public void VoegCoachToeAanTeam(Coach coach, VoetbalTeam team)
        {
            //verwijder deze coach uit delijst van het team waar hij nu inzit
            _dataBaseRepository.VoegCoachToeAanTeam(coach, team);
        }

        public void VerwijderSpelerUitTeam(Speler speler, VoetbalTeam currentTeam)
        {
            //kijk of speler bestaat
            if (_dataBaseRepository.GetAlleTeams().Where(t => t == currentTeam).FirstOrDefault().TeamLeden.Any(s => s == speler))
            {
                //Verwijder speler als deze bestaat
                _dataBaseRepository.VerwijderSpelerUitTeam(speler, currentTeam);
            }
        }

        public void VoegNieuwTeamToe(VoetbalTeam newTeam)
        {
            if (ValidateTeam(newTeam))
            {

                if (_dataBaseRepository.VoegNieuwItemToe(newTeam))
                {

                    ValidationMessage.SetMessageHeader($"Team {newTeam.NaamToString} is toegevoegd");

                }
                else
                {
                    ValidationMessage.SetMessageHeader($"Team {newTeam.NaamToString} kon niet worden toegevoegd");
                }
            }
        }

        public void VoegNieuweSpelerToe(Speler speler)
        {
            if (ValidatePersoon(speler))
            {
                if (_dataBaseRepository.VoegNieuwItemToe(speler))
                {

                    ValidationMessage.SetMessageHeader($"Speler {speler.NaamToString} is toegevoegd");

                }
                else
                {
                    ValidationMessage.SetMessageHeader($"Speler {speler.NaamToString} kon niet worden toegevoegd");
                }
            }
        }

        async public Task PasSpelerAanAsync(Speler spelerToModify, Speler tempModifiedSpeler)
        {
            if (ValidatePersoon(tempModifiedSpeler))
            {
                if (await _dataBaseRepository.PasSpelerAanAsync(spelerToModify, tempModifiedSpeler))
                {
                    ValidationMessage.SetMessageHeader($"Speler {spelerToModify.NaamToString} is aangepast");
                }
                else
                {
                    ValidationMessage.SetMessageHeader($"Speler {spelerToModify.NaamToString} kon niet worden aangepast");
                }
            }
        }


        public void VoegNieuweCoachToe(Coach tempCoach)
        {
            if (ValidatePersoon(tempCoach))
            {
                if (_dataBaseRepository.VoegNieuwItemToe(tempCoach))
                {
                    ValidationMessage.SetMessageHeader($"Coach {tempCoach.NaamToString} is toegevoegd");
                }
                else
                {
                    ValidationMessage.SetMessageHeader($"Coach {tempCoach.NaamToString} kon niet worden toegevoegd");

                }
            }
        }

        public void PasCoachAan(Coach currentCoach, Coach tempModifiedCoach)
        {
            //validateCoach
            if (ValidatePersoon(tempModifiedCoach))
            {
                if (_dataBaseRepository.PasCoachAan(currentCoach, tempModifiedCoach))

                {
                    ValidationMessage.SetMessageHeader($"Coach {currentCoach.NaamToString} is aangepast");
                }
                else
                {
                    ValidationMessage.SetMessageHeader($"Coach {currentCoach.NaamToString} kon niet worden aangepast");

                }

            }
        }

        public void VerwijderCoachuitTeam(Coach coach, VoetbalTeam currentTeam)
        {
            //kijk of coach bestaat
            if (_dataBaseRepository.GetAlleTeams().Where(t => t == currentTeam).FirstOrDefault().Coaches.Any(c => c == coach))
            {
                //4Verwijder coach als deze bestaat
                _dataBaseRepository.VerwijderCoachuitTeam(coach, currentTeam);
            }
        }

        public bool ValidatePersoon(Persoon persoon)
        {
            bool validated = true;
            ValidationMessage.Clear();
            if (String.IsNullOrWhiteSpace(persoon?.VoorNaam))
            {
                validated = false;
                ValidationMessage.AddLineToMessageBody("Voornaam is verplicht");
            }
            if (String.IsNullOrWhiteSpace(persoon?.AchterNaam))
            {
                validated = false;
                ValidationMessage.AddLineToMessageBody("Achternaam is verplicht");
            }
            if (persoon?.Leeftijd < 6)
            {
                validated = false;
                ValidationMessage.AddLineToMessageBody($"Een {nameof(Persoon)} moet minimaal 6 jaar oud zijn");
            }
            return validated;
        }

        public void VerwijderCoach(Coach currentCoach)
        {
            //Als coach bestaat
            if (_dataBaseRepository.GetAlleCoaches().Any(c => c == currentCoach))
            {

                if (_dataBaseRepository.VerwijderItem(currentCoach))

                {
                    ValidationMessage.SetMessageHeader($"Coach {currentCoach.NaamToString} is verwijderd");
                }
                else
                {
                    ValidationMessage.SetMessageHeader($"Coach {currentCoach.NaamToString} kon niet worden verwijderd");
                                    }
            }
        }

        public void VerwijderSpeler(Speler currentSpeler)
        {
            if (_dataBaseRepository.GetAlleSpelers().Any(c => c == currentSpeler))
            {
                if (_dataBaseRepository.VerwijderItem(currentSpeler))

                {
                    ValidationMessage.SetMessageHeader($"Coach {currentSpeler.NaamToString} is verwijderd");
                }
                else
                {
                    ValidationMessage.SetMessageHeader($"Coach {currentSpeler.NaamToString} kon niet worden verwijderd");

                }
            }

        }

        async public Task PasTeamAanAsync(VoetbalTeam currentTeam, VoetbalTeam tempModifiedTeam)
        {
            if (ValidateTeam(tempModifiedTeam))
            {
                if (await _dataBaseRepository.PasTeamAan(currentTeam, tempModifiedTeam))

                {
                    ValidationMessage.SetMessageHeader($"Team {currentTeam.NaamToString} is aangepast");
                }
                else
                {
                    ValidationMessage.SetMessageHeader($"Team {currentTeam.NaamToString} kon niet worden aangepast");

                }

            }
        }

        private bool ValidateTeam(VoetbalTeam voetbalTeam)
        {
            bool validated = true;
            ValidationMessage.Clear();
            if (String.IsNullOrWhiteSpace(voetbalTeam?.Naam))
            {
                validated = false;
                ValidationMessage.AddLineToMessageBody("Naam is verplicht");
            }
            //als er iemand van het andere geslacht in het team zit
            if (voetbalTeam.TeamLeden.Where(tl => tl.Geslacht == voetbalTeam.Geslacht).Count() < voetbalTeam.TeamLeden.Count())
            {
                validated = false;
                ValidationMessage.AddLineToMessageBody($"U kunt alleen teamleden van het geslacht {voetbalTeam.Geslacht} toevoegen");
            }

            return validated;
        }

        public void VerwijderTeam(VoetbalTeam currentTeam)
        {
            if (_dataBaseRepository.GetAlleTeams().Any(c => c == currentTeam))
            {
                if (_dataBaseRepository.VerwijderItem(currentTeam))

                {
                    ValidationMessage.SetMessageHeader($"Team {currentTeam.NaamToString} is verwijderd");
                }
                else
                {
                    ValidationMessage.SetMessageHeader($"Team {currentTeam.NaamToString} kon niet worden verwijderd");

                }

            }
        }
    }
}
