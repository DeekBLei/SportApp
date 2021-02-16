using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelService
{
    // Viewmodel voor de navigatie. Dit is voor de hele app gelijk en wordt
    // weergegeven in het bovenste frame.

    public class ViewModelNavigation :ViewModelINotify
    {
        public Command GaNaarSpelersPage { get; private set; }
        public Action GaNaarSpelersDelegate; public 
            Command GaNaarCoachesPage { get; private set; }
        public Action GaNaarCoachesDelegate;
        public Command GaNaarMainPage { get; private set; }
        public Action GaNaarMainPageDelegate;
        public Command GaNaarViewEditTeams { get; private set; }
        public Action GaNaarViewEditTeamsDelegate { get; set; }
        public Command GaNaarViewEditWedstrijdSchema { get; private set; }
        public Action GaNaarViewEditWedstrijdSchemaDelegate { get; set; }

        public ViewModelNavigation()
        {
           
            //Navigation Commands
           
            this.GaNaarSpelersPage = new Command(this.GaNaarSpelersExecute, () => true);
            this.GaNaarViewEditTeams = new Command(this.GaNaarViewEditTeamsExecute, () => true);
            this.GaNaarMainPage = new Command(this.GaNaarMainPageExecute, () => true);
            this.GaNaarViewEditWedstrijdSchema = new Command(this.GaNaarViewEditWedstrijdSchemaExecute, () => true);
            this.GaNaarCoachesPage = new Command(this.GaNaarCoachesExecute, () => true);
        }



        private void GaNaarSpelersExecute()
        {
            GaNaarSpelersDelegate?.Invoke();
        }
        private void GaNaarCoachesExecute()
        {
            GaNaarCoachesDelegate?.Invoke();
        }
        private void GaNaarViewEditTeamsExecute()
        {
            GaNaarViewEditTeamsDelegate?.Invoke();
        }
        private void GaNaarMainPageExecute()		
        {
            GaNaarMainPageDelegate?.Invoke();
        }
        private void GaNaarViewEditWedstrijdSchemaExecute()
        {
            GaNaarViewEditWedstrijdSchemaDelegate?.Invoke();
        }
    }
}
