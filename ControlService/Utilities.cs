using DataTypes;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DataBaseService;

namespace ControlService
{
    static public class Utilities
    {
        //Funtie waar de lists worden gefiltert op geslacht en zoekterm
        
        public static IEnumerable<T> FilterList<T>(IEnumerable<T> listT, string filterString, string geslacht, List<T> excludeListT) where T : IGeslacht, ISearchable
        {
            if (listT != null)
            {
                //filter een input lijst op een excludelist, daarna op geslacht en daarna op een searchstring
                return FilterOpFilterString(FilterOpGeslacht(FilterOpObject(listT, excludeListT), geslacht), filterString);
            }
            return listT;
        }

        private static IEnumerable<T> FilterOpObject<T>(IEnumerable<T> listT, IEnumerable<T> excludeListT)
        {
            if (excludeListT != null)
            {
                return (List<T>)listT.Except(excludeListT);
            }
            return listT;
        }

        private static IEnumerable<T> FilterOpGeslacht<T>(IEnumerable<T> listT, string geslacht) where T : IGeslacht
        {
            return new List<T>(listT.Where(p => p.Geslacht == geslacht));
        }

        private static IEnumerable<T> FilterOpFilterString<T>(IEnumerable<T> listT, string filterString) where T : ISearchable
        {
            return Utilities.Search(listT, filterString);
        }

        private static IEnumerable<T> Search<T>(IEnumerable<T> searchList, string searchString) where T : ISearchable
        {
            List<T> returnList = new List<T>();
            if (!string.IsNullOrEmpty(searchString))
            {
                foreach (T tItem in searchList) // zoek in alle objecten uit de lijst
                {
                    if (SearchObject(tItem, searchString))
                    {
                        returnList.Add(tItem);
                    }
                }
                return returnList;
            }
            //als lijst en zoekterm leeg zijn stuur de lijst als kopie terug
            else
            {
                foreach (var item in searchList)
                {
                    returnList.Add(item);
                }
                return returnList;
            }
        }

        private static bool SearchObject<T>(T tItem, String searchString) where T : ISearchable
        {
            List<string> propertyNames = tItem.SearchablePropertiesToList();
            searchString = searchString.ToLower();
            if (propertyNames?.Count != 0)
            {
                foreach (string propertyName in propertyNames) // zoek in alle properties
                {
                    var reflectedPropertie = tItem.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);
                    var propValue = reflectedPropertie.GetValue(tItem);
                    if (reflectedPropertie.PropertyType.GetInterfaces().ToList().Contains(typeof(ISearchable)))
                    {

                        ISearchable property = propValue as ISearchable;
                        if (property != null)
                        {
                            if (SearchObject(property, searchString))
                            // als ie het vind true anders verderkijken (false zou loop afbreken)
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {

                        if (propValue is String || propValue is Int32)
                        {
                            var propertyValueToString = propValue.ToString().ToLower();
                            //Als het Type string is en het eerste karakter een int:
                            //ga er dan van uit dat het een uitlag betreft en verwijder whitespaces
                            //Is messy maar werkt voor nu.......
                            if (propValue is String && int.TryParse(propValue.ToString().ElementAt(0).ToString(), out int parsedValue))
                            {
                                propertyValueToString = propertyValueToString.Replace(" ", "");
                                searchString = searchString.Replace(" ", "");
                            }
                            if (propertyValueToString.Contains(searchString))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}
