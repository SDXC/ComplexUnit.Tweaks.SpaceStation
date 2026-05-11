using System;
using System.Reflection;


namespace ComplexUnit.Tweaks.SpaceStation
{
    internal static class Utility
    {
        internal static void SetStaticField(Type type, string fieldName, object newValue)
        {
            FieldInfo field = type.GetField(fieldName, BindingFlags.Public | BindingFlags.Static);
            field?.SetValue(null, newValue);
        }
    }
}
