using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using BackEnd.db;

namespace BackEnd
{
    public class EntityService
    {
        private static string MATH_SYMBOLS = "[><=]";

        private List<Type> getEntityClasses()
        {
            Console.WriteLine("in getEntityClasses");

            return (from t in Assembly.GetExecutingAssembly().GetTypes()
                where t.IsClass && t.IsSubclassOf(typeof(Entity.Entity))
                select t).ToList();
        }

        public List<String> getAllClassesNames()
        {
            Console.WriteLine("in getAllClassesNames");
            return getEntityClasses().Select(it => it.Name).ToList();
        }

        public List<PropertyObject> getSelectedClassFields(String className)
        {
            Console.WriteLine("in GetSelectedClassFields");
            Type entityType = getEntityClasses().FirstOrDefault(it => it.Name.Equals(className));
            if (entityType != null)
            {
                PropertyInfo[] properties = entityType.GetProperties();
                return properties.Select(prop =>new PropertyObject( prop.Name,  prop.PropertyType) ).ToList();
            }

            return null;
        }
        
        /**
         * filters key is filtering value and key is property name
         */
        public List<object> FetchEntriesByClassNameAndFilterThem(FilterObject filterObject)
        {
            Console.WriteLine("in FetchEntitiesByClassNameAndFilterThem");

            Type entityTypeByName = getEntityTypeByName(filterObject.ClassName);

            List<object> dbEntities = new List<object>();

            if (entityTypeByName.IsSubclassOf(typeof(Entity.Entity)))
            {
                Type dbContextType = typeof(DatabaseContext<>).MakeGenericType(entityTypeByName);
                dynamic dbContext = Activator.CreateInstance(dbContextType);
                dynamic entities = dbContext.Entities;
                IEnumerable entitiesEnumerable = entities;

                List<Func<object, bool>> filterFunctions = fetchFilterFunctions(filterObject, entityTypeByName);
                
                dbEntities = new List<object>(entitiesEnumerable.Cast<object>().Where(entity =>
                    filterFunctions.All(filterFunction => applyFilterFunctions(entity, filterFunctions))).ToList());
            }

            return dbEntities.ToList();
        }

        private Type getEntityTypeByName(String entityName)
        {
            Console.WriteLine("in getEntityTypeByName");
            return getEntityClasses().FirstOrDefault(t => t.Name == entityName);
        }
        

        private List<Func<object, bool>> fetchFilterFunctions(FilterObject filterObject, Type entityTypeByName)
        {
            List<Func<object, bool>> filterFunctions = new List<Func<object, bool>>();
            foreach (var filter in filterObject.Filters)
            {
                string propertyName = filter.Value;
                string propertyValue = filter.Key;
                filterFunctions.Add(entity => decideFunction(entity, entityTypeByName, propertyName, propertyValue));
            }

            return filterFunctions;
        }

        private Boolean applyFilterFunctions(Object entity,List<Func<object, bool>> filterFunctions )
        {
            foreach (var filterFunction in filterFunctions)
            {
                if (!filterFunction(entity))
                {
                    return false;
                }
            }
            return true;
        }
        private bool decideFunction(object entity, Type entityTypeByName, string propertyName, string propertyValue)
        {
            if (spotNumericFilter(propertyValue))
            {
                string symbol = ExtractSpecialChars(propertyValue);
                string stringNumber = propertyValue.Replace(symbol, "");
                dynamic value = null;
                if (entityTypeByName.GetProperty(propertyName).PropertyType == typeof(double))
                {
                    value = double.Parse(stringNumber);
                }
                else
                {
                    value = int.Parse(stringNumber);
                }
                
                  return  numericFilter(entity, entityTypeByName, propertyName, value, symbol);
            }
            else
            {
               
                   return characterFilter(entity, entityTypeByName, propertyName, propertyValue);
                        
            }
        }

        private bool numericFilter(object entity, Type entityTypeByName, String propertyName, dynamic value,
            string symbol)
        {
            PropertyInfo propertyInfo = entityTypeByName.GetProperty(propertyName);
            if (propertyInfo != null)
            {
                dynamic entityValue = propertyInfo.GetValue(entity);
                if (entityValue != null && value.GetType() == entityValue.GetType())
                {
                    switch (symbol)
                    {
                        case ">":
                            return entityValue > value;
                        case "<":
                            return entityValue < value;
                        case "==":
                            return entityValue == value;
                        case ">=":
                            return entityValue >= value;
                        case "<=":
                            return entityValue <= value;
                        default:
                            return false;
                    }
                }
            }

            return false;
        }

        private bool characterFilter(object entity, Type entityTypeByName, string propertyName, string propertyValue)
        {
            PropertyInfo propertyInfo = entityTypeByName.GetProperty(propertyName);
            if (propertyInfo != null)
            {
                object value = propertyInfo.GetValue(entity);
                if (value != null && value.ToString().Equals(propertyValue))
                {
                    return true;
                }
            }

            return false;
        }

        private bool spotNumericFilter(string value)
        {
            return Regex.IsMatch(value, MATH_SYMBOLS);
        }


        private string ExtractSpecialChars(string input)
        {
            MatchCollection matches = Regex.Matches(input, MATH_SYMBOLS);
            StringBuilder extractedValues = new StringBuilder();

            for (int i = 0; i < matches.Count; i++)
            {
                extractedValues.Append(matches[i].Value);
            }

            return extractedValues.ToString();
        }
    }
}