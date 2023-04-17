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

        public void getAllClassesNames()
        {
            getEntitClasses().Select(it => it.Name);
        }

        public void getSelectedClassFields(String className)
        {
            getEntitClasses().Where(it => it.Name.Equals(className)).First().GetProperties().ToList()
                .Select(it => it.Name);
                // .ForEach(it => Console.WriteLine(it.Name));
        }
    }
}