namespace SGSTools.Util
{
    public static class PropertyDrawerUtils
    {
        public static string ToBackingFieldName(this string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                // TODO: Log warning.
                return "";
            }
            return $"<{propertyName}>k__BackingField";
        }
    }
}