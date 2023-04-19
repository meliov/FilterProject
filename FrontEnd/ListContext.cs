using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using BackEnd;
using BackEnd.db;

namespace FrontEnd
{
    public class ListContext
    {
        private static EntityService entityService = new EntityService();
        private List<String> _entityNames;
        private List<String> _properties;
        private List<String> _filterActions;

        public List<string> FilterActions
        {
            get => _filterActions;
            set => _filterActions = value;
        }

        public List<string> Properties
        {
            get => _properties;
            set => _properties = value;
        }

        public List<string> EntityNames
        {
            get => _entityNames;
            set => _entityNames = value;
        }

        public ListContext()
        {
            EntityNames = entityService.getAllClassesNames();
            Properties = entityService.getSelectedClassFields("Car").Select(it => it.Key).ToList();
            FilterActions = new List<string> { "==", ">", "<", ">=", "<=" };
        }
        
    }
}