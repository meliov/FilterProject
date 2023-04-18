using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BackEnd
{
    public class Service
    {
        private List<Type> getEntitClasses()
        {
            return (from t in Assembly.GetExecutingAssembly().GetTypes()
                where t.IsClass && t.Namespace == "BackEnd.Entity"
                select t).ToList();
        }

        public List<String> getAllClassesNames()
        {
           return getEntitClasses().Select(it => it.Name).ToList();
        }

        public List<String> getSelectedClassFields(String className)
        {
           return getEntitClasses().Where(it => it.Name.Equals(className)).First().GetProperties().ToList()
                .Select(it => it.Name).ToList();
                // .ForEach(it => Console.WriteLine(it.Name));
        }
    }
}