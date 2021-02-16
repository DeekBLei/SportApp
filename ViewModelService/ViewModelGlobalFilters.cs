using ControlService;
using DataBaseService;
using DataTypes;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelService
{
    public class ViewModelGlobalFilters : ViewModelDataBase
    {
        //<summary>
        //Alle members zijn static zodat vanuit het frame GlobalFilters waardes 
        //kunnen worden gezet in ViewModelGlobalFilters. Deze static waardes worden gebruikt
        // in instanties van andere ViewModels die op hun beurt weer erven van ViewModelGlobalFilter
        //<summary>

        //Properties
        // List voor dropdown geslacht
        public List<string> GeslachtList { get; set; }

        protected static Misc.GeslachtEnum _geslacht;
        public static string Geslacht
        {
            get => _geslacht.ToString();
            set
            {
                if (value == Misc.GeslachtEnum.Man.ToString())
                {
                    _geslacht = Misc.GeslachtEnum.Man;
                }
                if (value == Misc.GeslachtEnum.Vrouw.ToString())
                {
                    _geslacht = Misc.GeslachtEnum.Vrouw;
                }
                FilterChanged?.Invoke();

            }
        }

        private static string _filterString;
        public static string FilterString
        {
            get { return _filterString; }
            set
            {
                _filterString = value;
                FilterChanged?.Invoke();
            }
        }




        //Delegates

        protected static event Action FilterChanged;


        //Constructor
        public ViewModelGlobalFilters()
        {
            this.GeslachtList = Misc.GeslachtsList;
        }
        static ViewModelGlobalFilters()
        {
            Geslacht = Misc.GeslachtEnum.Man.ToString();
        }

    }
}

