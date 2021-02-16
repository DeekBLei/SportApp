using DataBaseService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Deze class zorgt er voor data alle ViewModels een databaseRepository erven*/

namespace ViewModelService
{
    public abstract class ViewModelDataBase : ViewModelINotify
    {
        // public static Func<DataBase> getDatabaseFunc;
        // public static Func<DataBaseControl> getDatabaseControlFunc;
        //private ApplicationDBContext _dataBase;
        //public static Action<DataBase> setDatabaseAction;
        //  public static Action<DataBaseControl> setDatabaseControlAction;


        public static DataBaseRepository DataBaseRepository { get; set; }
        //{
        //    get
        //    {
        //        return getDatabaseControlFunc?.Invoke();
        //    }
        //    set
        //    {
        //        setDatabaseControlAction(value);
        //        this.OnPropertyChanged(nameof(DataBaseController));
        //    }
        //}
        //public DataBase dataBase
        //{
        //    get
        //    {
        //        return getDatabaseFunc?.Invoke();
        //    }
        //    set
        //    {
        //        setDatabaseAction(value);
        //        this.OnPropertyChanged(nameof(dataBase));
        //    }
        //}

        public ViewModelDataBase()
        {
            //  this.dataBase = getDatabaseFunc?.Invoke();

            //_dataBase = new ApplicationDBContext();
            //DataBaseController = new DataBaseControl(_dataBase);

            DataBaseRepository = new DataBaseRepository();

            // Als de spelers lijst geen elementen bevat seed deze dan
            if (DataBaseRepository.GetAlleSpelers().Count == 0)
            {
                DataBaseRepository.SeedDataBase();
            }
        }
    }
}
