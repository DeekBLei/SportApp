using ControlService;
using DataTypes;
using Interfaces;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelService
{
    //Deze class bevat alle lijsten gefiltered door geslacht en de filterstring
    public class ViewModelFilteredLists : ViewModelGlobalFilters
    {
        public ObservableCollection<VoetbalTeam> FilteredTeamList { get; set; }
        public ObservableCollection<Speler> FilteredSpelersList { get; set; }
        public ObservableCollection<Coach> FilteredCoachesList { get; set; }
        public ObservableCollection<Wedstrijd> FilteredWedstrijdList { get; set; }
        private ObservableCollection<VoetbalTeam> _filteredRangList;
        public ObservableCollection<VoetbalTeam> FilteredRangList
        {
            get
            {
                // FilterRangList(_filteredRangList);
                return _filteredRangList;
            }
            set { _filteredRangList = value; }
        }


        //Constructor
        public ViewModelFilteredLists()
        {
            FilteredRangList = new ObservableCollection<VoetbalTeam>();
            FilteredTeamList = new ObservableCollection<VoetbalTeam>();
            FilteredSpelersList = new ObservableCollection<Speler>();
            FilteredCoachesList = new ObservableCollection<Coach>();
            FilteredWedstrijdList = new ObservableCollection<Wedstrijd>();
            UpdateAllLists();
            var ws = new WedstrijdSecretariaat(DataBaseRepository);
            FilterChanged += UpdateAllLists;
            //De DataBaseRepository raist een event als er een wijziging wordt gedaan in de lijsten. Hieronder wordt de bijbehoorende lijst dan geupdatet
            DataBaseRepository.WedstrijdenGewijzigd += () => UpdateList(FilteredWedstrijdList, DataBaseRepository.GetAlleWedstrijden());
            DataBaseRepository.TeamsGewijzigd += () => UpdateList(FilteredTeamList, DataBaseRepository.GetAlleTeams());
            DataBaseRepository.SpelersGewijzigd += () => UpdateList(FilteredSpelersList, DataBaseRepository.GetAlleSpelers(), "AchterNaam", "VoorNaam");
            DataBaseRepository.CoachesGewijzigd += () => UpdateList(FilteredCoachesList, DataBaseRepository.GetAlleCoaches(), "AchterNaam", "VoorNaam");
            DataBaseRepository.DoelpuntenGewijzigd += () => UpdateList(FilteredWedstrijdList, DataBaseRepository.GetAlleWedstrijden());
            DataBaseRepository.DoelpuntenGewijzigd += () => UpdateList(FilteredRangList, ws.RangLijst.ToList(), "WedstrijdSaldo", "DoelSaldo", "NaamToString", true);
        }

        //Methods
        private void UpdateList<T>(ObservableCollection<T> FilterResult, IEnumerable<T> SourceListToFilter, string OrderByProperty = "NaamToString", string ThenByProperty = "", string ThenByProperty2 = "", bool reverse = false) where T : ISearchable, IGeslacht, INaamToString
        {
            //TODO: Sorteer op property keuze in GUI -- Gaan we niet meer doen
            FilterResult.Clear();
            var orderByPropertyInfo = typeof(T).GetProperty(OrderByProperty);
            if (!String.IsNullOrEmpty(ThenByProperty))
            {
                var thenByPropertyInfo = typeof(T).GetProperty(ThenByProperty);
                if (!String.IsNullOrEmpty(ThenByProperty2))
                {
                    var thenByPropertyInfo2 = typeof(T).GetProperty(ThenByProperty2);

                    SourceListToFilter = SourceListToFilter
                        .OrderBy(i => orderByPropertyInfo.GetValue(i, null))
                        .ThenBy(i => thenByPropertyInfo.GetValue(i, null))
                        .ThenByDescending(i => thenByPropertyInfo2.GetValue(i, null))
                        .ToList();
                }
                else
                {
                    SourceListToFilter = SourceListToFilter.OrderBy(i => orderByPropertyInfo.GetValue(i, null))
                     .ThenBy(i => thenByPropertyInfo.GetValue(i, null))
                    .ToList();
                }
            }
            else
            {
                SourceListToFilter = SourceListToFilter.OrderBy(i => orderByPropertyInfo.GetValue(i, null)).ToList();
            }
            if (reverse)
            {
                SourceListToFilter = SourceListToFilter.Reverse();
            }
            foreach (var item in Utilities.FilterList(SourceListToFilter, FilterString, Geslacht, null).ToList())
            {
                FilterResult.Add(item);
            }
        }
    
        private void UpdateAllLists()
        {
            WedstrijdSecretariaat ws = new WedstrijdSecretariaat(DataBaseRepository);
            UpdateList(FilteredTeamList, DataBaseRepository.GetAlleTeams());
            UpdateList(FilteredSpelersList, DataBaseRepository.GetAlleSpelers(), "AchterNaam", "VoorNaam");
            UpdateList(FilteredCoachesList, DataBaseRepository.GetAlleCoaches(), "AchterNaam", "VoorNaam");
            UpdateList(FilteredWedstrijdList, DataBaseRepository.GetAlleWedstrijden());
            UpdateList(FilteredRangList, ws.RangLijst, "WedstrijdSaldo", "DoelSaldo", "NaamToString", true);
        }
    }
}
