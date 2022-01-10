namespace EVLib.Extensions
{
    public static class ObjectExtension
    {
        /// <summary>
        /// Copies all fields and properties from one Object to another.
        /// </summary>
        /// <param name="destinationObject">The destination Object.</param>
        /// <param name="sourceObject">The source Object.</param>
        public static void ShallowCopy(this object destinationObject, object sourceObject)
        {
            var sourceType = sourceObject.GetType();
            var destinationType = destinationObject.GetType();
            foreach (var field in sourceType.GetFields())
            {
                var destinationField = destinationType.GetField(field.Name);
                if (destinationField == null)
                {
                    continue;
                }
                destinationField.SetValue(destinationObject, field.GetValue(sourceObject));
            }

            foreach (var property in sourceType.GetProperties())
            {
                var destinationProperty = destinationType.GetProperty(property.Name);
                if (destinationProperty == null)
                {
                    continue;
                }
                destinationProperty.SetValue(destinationObject, property.GetValue(sourceObject, null), null);
            }
        }
    }
}
