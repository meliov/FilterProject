using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BackEnd.db;

namespace BackEnd
{
    public class EntityService
    {
        private List<Type> getEntitClasses()
        {
            Console.WriteLine("in getEntitClasses");

            return (from t in Assembly.GetExecutingAssembly().GetTypes()
                where t.IsClass && t.Namespace == "BackEnd.Entity" && t.IsSubclassOf(typeof(Entity.Entity))
                select t).ToList();
        }

        public List<String> getAllClassesNames()
        {
            Console.WriteLine("in getAllClassesNames");
           return getEntitClasses().Select(it => it.Name).ToList();
        }

        public Dictionary<String, Type> getSelectedClassFields(String className)
        {
            Console.WriteLine("in GetSelectedClassFields");
            Type entityType = getEntitClasses().FirstOrDefault(it => it.Name.Equals(className));
            if (entityType != null)
            {
                PropertyInfo[] properties = entityType.GetProperties();
                return properties.ToDictionary(prop => prop.Name, prop => prop.PropertyType);
            }
            return null;
        }

        public Type getEntityTypeByName(String entityName)
        {
            Console.WriteLine("in getEntityTypeByName");
            return getEntitClasses().FirstOrDefault(t => t.Name == entityName);
        }
        
          public List<object> FetchEntitiesByClassNameAndFilterThem(String className,Dictionary<string,string> filters)
        {
            Console.WriteLine("in FetchEntitiesByClassName");
            Type entityTypeByName = getEntityTypeByName(className);
            List<object> dbEntities = null;
            if (entityTypeByName.IsSubclassOf(typeof(Entity.Entity)))
            {
                Type dbContextType = typeof(DatabaseContext<>).MakeGenericType(entityTypeByName);
                dynamic dbContext = Activator.CreateInstance(dbContextType);
                dynamic entities = dbContext.Entities;
                IEnumerable entitiesEnumerable = entities;

                dbEntities = entitiesEnumerable.Cast<object>().ToList();
            }

            return dbEntities;
        }

        
    }
}