using System;

namespace BackEnd
{
    public class PropertyObject
    {
        private string _propName;
        private Type _propType;

        public PropertyObject(string propName, Type propType)
        {
            _propName = propName;
            _propType = propType;
        }

        public string PropName
        {
            get => _propName;
            set => _propName = value;
        }

        public Type PropType
        {
            get => _propType;
            set => _propType = value;
        }
    }
}