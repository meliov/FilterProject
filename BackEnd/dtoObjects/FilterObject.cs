using System;
using System.Collections.Generic;

namespace BackEnd
{
    public class FilterObject
    {
        private String _className;
        private Dictionary<string, string> _filters;

        public string ClassName => _className;

        public Dictionary<string, string> Filters => _filters;

        public FilterObject(string className, Dictionary<string, string> filters)
        {
            _className = className;
            _filters = filters;
        }
    }
}