using System;
using System.Collections.Generic;
using System.Text;

namespace EVLlib.Extentensions
{
    public static class ObjectExtension
    {
        /// <summary>
        /// Copies all fields and properties from one Object to another.
        /// </summary>
        /// <param name="destinationObjct">The destination Object.</param>
        /// <param name="sourceObject">The source Object.</param>
        public static void ShallowCopy(this object destinationObjct, object sourceObject)
        {
            var sourceType = sourceObject.GetType();
            var destinationType = destinationObjct.GetType();
            foreach (var field in sourceType.GetFields())
            {
                var destinationField = destinationType.GetField(field.Name);
                if (destinationField == null)
                {
                    continue;
                }
                destinationField.SetValue(destinationObjct, field.GetValue(sourceObject));
            }

            foreach (var property in sourceType.GetProperties())
            {
                var destinationProperty = destinationType.GetProperty(property.Name);
                if (destinationProperty == null)
                {
                    continue;
                }
                destinationProperty.SetValue(destinationObjct, property.GetValue(sourceObject, null), null);
            }
        }
    }
}
