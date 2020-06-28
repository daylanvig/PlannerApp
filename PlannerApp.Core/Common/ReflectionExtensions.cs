using System;
using System.Linq;

namespace PlannerApp.Shared.Common
{
    public static class ReflectionExtensions
    {
        public static T GetAttributeFrom<T>(this object instance, string propertyName) where T : Attribute
        {
            var attrType = typeof(T);
            var property = instance.GetType().GetProperty(propertyName);
            return (T)property.GetCustomAttributes(attrType, false).FirstOrDefault();
        }
    }
}
