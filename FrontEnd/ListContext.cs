using System;
using System.Collections.Generic;
using BackEnd;
using BackEnd.db;

namespace FrontEnd
{
    public class ListContext
    {
        private static EntityService entityService = new EntityService();
        private List<String> _entityNames;

        public List<string> EntityNames
        {
            get => _entityNames;
            set => _entityNames = value;
        }

        public ListContext()
        {
            EntityNames = entityService.getAllClassesNames();
        }
    }
}