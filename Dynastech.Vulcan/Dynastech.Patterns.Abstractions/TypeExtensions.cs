using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace System
{
    public static class TypeExtensions
    {
        public static TAttribute GetAttribute<TAttribute>(this Type type) where TAttribute : Attribute
        {
            var attributes = type.GetCustomAttributes(typeof(TAttribute), false)?.OfType<TAttribute>();
            if (attributes != null)
                return attributes.FirstOrDefault();
            return null;
        }
    }
}
