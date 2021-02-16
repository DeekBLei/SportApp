using DataTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlService
{
    public class WedstrijdSimulatie
    {
         public MessageService Message { get; set; } = new MessageService();
        private WedstrijdSecretariaat WedstrijdSecretariaat { get; set; }


        public WedstrijdSimulatie(WedstrijdSecretariaat wedstrijdSecretariaat)
        {
            this.WedstrijdSecretariaat = wedstrijdSecretariaat;
        }



        async public Task SpeelWedstrijdAsync(Wedstrijd currentWedstrijd)
        {

            await StartWedstrijdAsync(currentWedstrijd);
            Message.SetMessageHeader(FeedbackMessages.GetWedstrijdWordtGespeeld(currentWedstrijd));
            await SimuleerAanvallenAsync(currentWedstrijd);
            //return 
            await BeeindigWedstrijd(currentWedstrijd);



            //return t;

        }


        async private Task StartWedstrijdAsync(Wedstrijd wedstrijdCopy)
        {
            Message.SetMessageHeader($"De wedstrijd {wedstrijdCopy.NaamToString} wordt voorbereid");
            await Task.Delay(1500);
        }

        async private Task SimuleerAanvallenAsync(Wedstrijd currentWedstrijd)
        {
            //Zoveel aanvallen als er speler zijn
            for (int i = 0; i < currentWedstrijd.ThuisTeam.TeamLeden.Count + currentWedstrijd.UitTeam.TeamLeden.Count; i++)
            {

                Random rand = new Random();

                // bepaal welk team aanvalt
                int kiesTeam = rand.Next(201);
                VoetbalTeam teamAanval = kiesTeam % 2 == 0 ? currentWedstrijd.ThuisTeam : currentWedstrijd.UitTeam;
                VoetbalTeam teamVerdedig = kiesTeam % 2 == 0 ? currentWedstrijd.UitTeam : currentWedstrijd.ThuisTeam;

                //kies aanvaller en verdediger

                Speler aanvaller = teamAanval.TeamLeden[rand.Next(teamAanval.TeamLeden.Count)];
                Speler verdediger = teamVerdedig.TeamLeden[rand.Next(teamVerdedig.TeamLeden.Count)];

                //Doe aanval
                if (await AanvalAsync(aanvaller, verdediger))
                {
                    //Als aanval succesvol => registreer dan doelpunt en pas ervaring nieveau betrokken spelers aan
                    WedstrijdSecretariaat.RegistreerDoelpunt(aanvaller, teamAanval, verdediger, currentWedstrijd);
                    currentWedstrijd.Uitslag = "updatePropertyChanged"; // Slaat nergens op maar triggert wel het PropertyChanged event
                    Message.SetMessageBody($"{aanvaller.NaamToString} scoorde een doelpunt voor {teamAanval.NaamToString}");
                    Message.AddLineToMessageBody(FeedbackMessages.GetWedstrijdTussenStand(currentWedstrijd.Uitslag));
                }
                else
                {
                    //Als aanval niet succesvol =>  pas ervaring niveau betrokken spelers aan
                    WedstrijdSecretariaat.RegistreerRedding(aanvaller, verdediger);
                    Message.SetMessageBody($"{verdediger.NaamToString}({teamVerdedig.NaamToString}) stopte een aanval van {aanvaller.NaamToString}({teamAanval.NaamToString})");
                    Message.AddLineToMessageBody(FeedbackMessages.GetWedstrijdTussenStand(currentWedstrijd.Uitslag));
                }
                await Task.Delay(1500);
            }
        }

        async private Task BeeindigWedstrijd(Wedstrijd currentWedstrijd)
        {
            Message.SetMessageBody(string.Empty);
            Message.SetMessageHeader($"De eindstand van wedstrijd {currentWedstrijd.NaamToString} is {currentWedstrijd.Uitslag}.");
            await Task.Delay(2000);
            Message.SetMessageHeader(string.Empty);
            //wedstrijdCopy.UitTeam.Wedstrijden.Add(wedstrijdCopy);
            //wedstrijdCopy.ThuisTeam.Wedstrijden.Add(wedstrijdCopy);

        }

        async private Task<bool> AanvalAsync(Speler aanvaller, Speler verdediger)
        {

            Task<bool> t = Task.Run(async () =>
            {
                bool aanvalSuccesvol = false;
                Random sterkte = new Random();

                //TODO mischien nog een trainings component? Meer training => sterkere speler
                int aanvalsterkte = aanvaller.Ervaring + 20;
                int verdedigingssterkte = verdediger.Ervaring + 20;
                if (sterkte.Next(0, aanvalsterkte) > sterkte.Next(0, verdedigingssterkte))
                {

                    aanvalSuccesvol = true;


                }
                await Task.Delay(1000);
                return aanvalSuccesvol;
            });
            await t;
            return t.Result;

        }
    }
}
