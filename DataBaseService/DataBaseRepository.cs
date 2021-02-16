
using DataTypes;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace DataBaseService
{
    //Deze class fungeert als tussenpersoon tussen de DBContext en de app.
    //Alle bewerking of aanvragen lopen via deze class

    public class DataBaseRepository
    {
        //
        public event Action WedstrijdenGewijzigd;
        public event Action TeamsGewijzigd;
        public event Action SpelersGewijzigd;
        public event Action DoelpuntenGewijzigd;
        public event Action CoachesGewijzigd;
        private ApplicationDBContext _dBContext;

        public DataBaseRepository()//ApplicationDBContext dataBase)
        {
            //DBContext = dataBase;

            _dBContext = new ApplicationDBContext();
          //  ExportXML exportXML = new ExportXML();
           // exportXML.exportDbToXml<Speler>(GetAlleSpelers());

        }
        public List<Wedstrijd> GetAlleWedstrijden()
        {
            return _dBContext.AlleWedstrijden.Where(w => !w.Deleted).Include(w => w.Doelpunten).Include(w => w.ThuisTeam).Include(w => w.UitTeam).ToList();
        }
        public List<Coach> GetAlleCoaches()
        {
            return _dBContext.AlleCoaches.Where(w => !w.Deleted).Include(c => c.Team).ToList();
        }
        public List<Speler> GetAlleSpelers()
        {
            return _dBContext.AlleSpelers.Where(w => !w.Deleted).Include(c => c.Team).Include(s => s.Doelpunten).ToList();
        }
        public List<Doelpunt> GetAlleDoelpunten()
        {
            return _dBContext.AlleDoelpunten.Include(d => d.Speler).Include(d => d.Team).Include(d => d.Wedstrijd).ToList();
        }
        public List<VoetbalTeam> GetAlleTeams()
        {
            return _dBContext.AlleTeams.Where(w => !w.Deleted).Include(t => t.TeamLeden)
                .Include(t => t.Coaches)
                .Include(t => t.VoetbalTeamWedstrijds).ThenInclude(vtw => vtw.Wedstrijd)
                .Include(t => t.VoetbalTeamWedstrijds).ThenInclude(vtw => vtw.VoetbalTeam)
                .Include(t => t.Doelpunten).ToList();
        }


        // Methods

        public bool VoegNieuwItemToe<T>(T item)
        {
            _dBContext.Add(item);
            bool gelukt = _dBContext.SaveChanges() > 0;
            if (gelukt)
            {
                switch (item)
                {

                    case Wedstrijd s:
                        this.WedstrijdenGewijzigd?.Invoke();
                        break;
                    case VoetbalTeam t:
                        this.TeamsGewijzigd?.Invoke();
                        break;
                    case Speler s:
                        this.SpelersGewijzigd?.Invoke();
                        break;
                    case Coach c:
                        this.CoachesGewijzigd?.Invoke();
                        break;
                    case Doelpunt doelpunt:
                        this.DoelpuntenGewijzigd?.Invoke();
                        break;

                }
            }
            return gelukt;
        }

        
        public void FlagWedstrijdIsBezig(Wedstrijd wedstrijd)
        {
            wedstrijd.IsBezig = true;
            _dBContext.SaveChanges();
        }

        public void VoegWedstrijdToeAanTeams(Wedstrijd wedstrijd)
        {

            var voetbalTeamWedstrijd = new VoetbalTeamWedstrijd
            {
                Wedstrijd = wedstrijd,
                VoetbalTeam = wedstrijd.UitTeam
            };

            wedstrijd.UitTeam.VoetbalTeamWedstrijds.Add(voetbalTeamWedstrijd);
            _dBContext.SaveChanges();

            var voetbalTeamWedstrijd2 = new VoetbalTeamWedstrijd
            {
                Wedstrijd = wedstrijd,
                VoetbalTeam = wedstrijd.ThuisTeam
            };

            wedstrijd.ThuisTeam.VoetbalTeamWedstrijds.Add(voetbalTeamWedstrijd2);
            _dBContext.SaveChanges();
        }


        //registreer het doelpunt en update de Currentwedstrijd met de waardes uit WedstrijdCopy
        async public void RegistreerDoelpunt(Speler aanvaller, VoetbalTeam team, Speler verdediger, Wedstrijd currentWedstrijd)
        {
            //voeg doelpunt toe met currentwedstrijd. Anders wordt het doelpunt aan de verkeerde wedstrijd gelinkt
            Doelpunt doelpunt = new Doelpunt(aanvaller, currentWedstrijd, team);
            VoegNieuwItemToe(doelpunt);

            // voeg doelpunt toe aan wedstrijd, aanvaller en team
            currentWedstrijd.Doelpunten.Add(doelpunt);
            aanvaller.Doelpunten.Add(doelpunt);
            team.Doelpunten.Add(doelpunt);

            //Update ervaring
            verdediger.Ervaring++;
            aanvaller.Ervaring += 2;


            try
            {
                await _dBContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Doelpunt is niet geregistreerd: {ex}");
            }
        }

        async public Task<bool> PasTeamAan(VoetbalTeam currentTeam, VoetbalTeam tempModifiedTeam)
        {
            currentTeam.MemberwiseClone(tempModifiedTeam);
            return await _dBContext.SaveChangesAsync() > 0;
        }

        public void PasWedstrijdAan(Wedstrijd bestaandeWedstrijd, Wedstrijd currentWedstrijdCopy)
        {
            bestaandeWedstrijd.MemberwiseClone(currentWedstrijdCopy);
            _dBContext.SaveChanges();
        }

        public void FlagWedstrijdIsGespeeld(Wedstrijd currentWedstrijd)
        {
            currentWedstrijd.IsGespeeld = true;
            currentWedstrijd.IsBezig = false;
            _dBContext.SaveChanges();
        }

        public bool RegistreerRedding(Speler aanvaller, Speler verdediger)
        {
            aanvaller.Ervaring++;
            verdediger.Ervaring += 2;
            return _dBContext.SaveChanges() > 0;
        }

        public bool VerwijderItem<T>(T item) where T : IDelete
        {
            try
            {
                //Mark as deleted
                item.Deleted = true;
                // _dBContext.Remove(item); Dit doen we niet meer
                bool gelukt = _dBContext.SaveChanges() > 0;
                if (gelukt)
                {
                    switch (item)
                    {
                        case Wedstrijd s:
                            this.WedstrijdenGewijzigd?.Invoke();
                            break;
                        case VoetbalTeam t:
                            this.TeamsGewijzigd?.Invoke();
                            break;
                        case Speler s:
                            this.SpelersGewijzigd?.Invoke();
                            break;
                        case Coach c:
                            this.CoachesGewijzigd?.Invoke();
                            break;
                        case Doelpunt doelpunt:
                            this.DoelpuntenGewijzigd?.Invoke();
                            break;
                    }
                }
                return gelukt;
            }
            catch (Exception ex)
            {
                throw new Exception($"Het item kon niet gevonden worden in de DB : {ex.Message}");
            }

        }

        async public Task<bool> PasSpelerAanAsync(Speler spelerToModify, Speler tempModifiedSpeler)
        {
            bool gelukt = false;
            if (spelerToModify != null && tempModifiedSpeler != null)
            {
                spelerToModify.AchterNaam = tempModifiedSpeler.AchterNaam;
                spelerToModify.VoorNaam = tempModifiedSpeler.VoorNaam;
                gelukt = await this.VoegSpelerToeAanTeam(spelerToModify, this.GetAlleTeams().Where(t => t == tempModifiedSpeler.Team).FirstOrDefault());
                spelerToModify.Geslacht = tempModifiedSpeler.Geslacht;
                spelerToModify.GeboorteDatum = tempModifiedSpeler.GeboorteDatum;
                if (gelukt)
                {
                    try
                    {
                        await _dBContext.SaveChangesAsync();
                    }
                    catch (Exception)
                    {
                        throw new Exception("Speler kon niet worden aangepast");
                    }
                }
                else
                {
                    try
                    {
                        gelukt = await _dBContext.SaveChangesAsync() > 0;
                    }
                    catch
                    {
                        throw new Exception("Speler kon niet worden aangepast");
                    }
                }
                this.SpelersGewijzigd?.Invoke();
            }
            return gelukt;

        }

        async public Task<bool> VoegSpelerToeAanTeam(Speler speler, VoetbalTeam team)
        {
            bool gelukt = false;
            if (speler != null && team != null)
            {
                speler.Team?.TeamLeden.Remove((Speler)speler);
                speler.Team = team;
                team.TeamLeden.Add((Speler)speler);
                gelukt = await _dBContext.SaveChangesAsync() > 0;
                if (gelukt)
                {
                    this.TeamsGewijzigd?.Invoke();
                    this.SpelersGewijzigd?.Invoke();
                }
            }
            return gelukt;
        }


        public bool VoegCoachToeAanTeam(Coach coach, VoetbalTeam team)
        {
            //verwijder deze coach uit de lijst van het team waar hij nu inzit
            bool gelukt = false;
            coach.Team?.Coaches.Remove(coach);
            team?.Coaches.Add(coach);
            coach.Team = team;
            gelukt = _dBContext.SaveChanges() > 0;
            if (gelukt)
            {
                this.TeamsGewijzigd?.Invoke();
                this.CoachesGewijzigd?.Invoke();
            }
            return gelukt;

        }

        public void VerwijderSpelerUitTeam(Speler speler, VoetbalTeam team)
        {
            team?.TeamLeden.Remove(speler);
            speler.Team = null;
            _dBContext.SaveChanges();
            this.SpelersGewijzigd?.Invoke();
            this.CoachesGewijzigd?.Invoke();
        }

        public void VerwijderCoachuitTeam(Coach coach, VoetbalTeam team)
        {
            team?.Coaches.Remove(coach);
            coach.Team = null;
            _dBContext.SaveChanges();
            this.TeamsGewijzigd?.Invoke();
            this.CoachesGewijzigd?.Invoke();
        }



        public bool PasCoachAan(Coach coachToModify, Coach tempModifiedCoach)
        {
            bool gelukt = false;
            if (coachToModify != null && tempModifiedCoach != null)
            {

                coachToModify.AchterNaam = tempModifiedCoach.AchterNaam;
                coachToModify.VoorNaam = tempModifiedCoach.VoorNaam;
                gelukt = this.VoegCoachToeAanTeam(coachToModify, tempModifiedCoach.Team);
                coachToModify.Geslacht = tempModifiedCoach.Geslacht;
                coachToModify.GeboorteDatum = tempModifiedCoach.GeboorteDatum;
                _dBContext.Entry(coachToModify).State = EntityState.Modified;
                if (gelukt)
                {
                    _dBContext.SaveChanges();
                }
                else
                {
                    gelukt = _dBContext.SaveChanges() > 0;
                }
                if (gelukt)
                {
                    this.CoachesGewijzigd?.Invoke();
                }
            }

            return gelukt;
        }


        public void MigrateDataBase()
        {
            _dBContext.Database.Migrate();
        }

        public void SeedDataBase()
        {
            DataBaseSeed seedDatabase = new DataBaseSeed();
            if (!_dBContext.AlleSpelers.Any())
            {
                _dBContext.AlleSpelers.AddRange(seedDatabase.GetSeedDataBaseSpelers());
                _dBContext.SaveChanges();
            }
            if (!_dBContext.AlleCoaches.Any())
            {
                _dBContext.AlleTeams.AddRange(seedDatabase.GetSeedDataBaseTeams());
                _dBContext.SaveChanges();
            }

        }
    }
}
