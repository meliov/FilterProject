using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BackEnd;

namespace FrontEnd;

public class Utils
{
    public static string MAIN_FILTER_SEPARATOR = "___";
    
    private  static EntityService entityService = new EntityService();


     public static ObservableCollection<Object> populateTableWithEntries(String SelectedEntityName, ObservableCollection<string> PickedFilters )
    {
        Dictionary<string, string> filters = new Dictionary<string, string>();
        foreach (var pickedFilter in PickedFilters)
        {
            string filterValueToAdd = "";
            var elementsSeparator = MAIN_FILTER_SEPARATOR;
            var choppedFilter = pickedFilter.Split(new string[]{elementsSeparator},StringSplitOptions.None);
            //0 SelectedEntityName + MAIN_FILTER_SEPARATOR + 1 SelectedProperty + MAIN_FILTER_SEPARATOR +2 SelectedOperator+ MAIN_FILTER_SEPARATOR + 3 FilterValue;
            string filterPropertyToAdd = choppedFilter[1];
            string filterOperator = choppedFilter[2];
            if (filterOperator.Equals("is"))
            {
                filterValueToAdd = choppedFilter[3];
            }
            else
            {
                filterValueToAdd = choppedFilter[2] + choppedFilter[3];
            }
            filters.Add(filterValueToAdd,filterPropertyToAdd);
        }

        return new ObservableCollection<Object>(entityService
            .FetchEntriesByClassNameAndFilterThem(new FilterObject(SelectedEntityName, filters)));
    }

     public static ObservableCollection<string> getAllClassesNames()
     {
         return new ObservableCollection<string>(entityService.getAllClassesNames());
     }

     public static ObservableCollection<string> getSelectedClassFields(String value)
     {
         return new ObservableCollection<string>(entityService.getSelectedClassFields(value).Select(it => it.PropName)
             .ToList());
     }
}