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

        /**
         * filters key is filtering value and key is property name
         */
        public List<object> FetchEntriesByClassNameAndFilterThem(String className, Dictionary<string, string> filters)
        {
            Console.WriteLine("in FetchEntitiesByClassNameAndFilterThem");

            Type entityTypeByName = getEntityTypeByName(className);

            List<object> dbEntities = null;

            if (entityTypeByName.IsSubclassOf(typeof(Entity.Entity)))
            {
                Type dbContextType = typeof(DatabaseContext<>).MakeGenericType(entityTypeByName);
                dynamic dbContext = Activator.CreateInstance(dbContextType);
                dynamic entities = dbContext.Entities;
                IEnumerable entitiesEnumerable = entities;

                dbEntities = entitiesEnumerable.Cast<object>().ToList();

                // Filter entities based on property names and values
                foreach (var filter in filters)
                {
                    string propertyName = filter.Value;
                    string propertyValue = filter.Key;

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

                        dbEntities = dbEntities.Where(entity =>
                            numericFilter(entity, entityTypeByName, propertyName, value, symbol)).ToList();
                    }
                    else
                    {
                        dbEntities = dbEntities.Where(entity =>
                            characterFilter(entity, entityTypeByName, propertyName, propertyValue)).ToList();
                    }
                }
            }

            return dbEntities;
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