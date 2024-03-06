using System;
using System.Reflection;

using UnityEditor;

namespace Emp37.Utility.Editor
{
      using static ReflectionUtility;


      public static class SerializedPropertyExtensions
      {
            public static TAttribute GetAttribute<TAttribute>(this SerializedProperty property, BindingFlags bindings = DEFAULT_FLAGS) where TAttribute : Attribute
            {
                  if (property == null) throw new ArgumentNullException(nameof(property), "SerializedProperty cannot be null.");
                  var type = property.serializedObject.targetObject.GetType() ?? throw new ArgumentException($"Target targetType of property '{property.name}' is null or the serialized object is not set.");

                  return FetchFieldInfo(property.name, type, bindings)?.GetCustomAttribute<TAttribute>();
            }
            public static bool TryGetAttribute<TAttribute>(this SerializedProperty property, out TAttribute attribute, BindingFlags bindings = DEFAULT_FLAGS) where TAttribute : Attribute
            {
                  attribute = default;
                  try
                  {
                        attribute = GetAttribute<TAttribute>(property, bindings);
                        return attribute != null;
                  }
                  catch (ArgumentException)
                  {
                        return false;
                  }
            }
            public static bool HasAttribute<TAttribute>(this SerializedProperty property, BindingFlags bindings = DEFAULT_FLAGS) where TAttribute : Attribute
            {
                  var type = property.serializedObject.targetObject.GetType();
                  if (type != null)
                  {
                        var field = FetchFieldInfo(property.name, type, bindings);
                        if (field != null)
                        {
                              return field.IsDefined(typeof(TAttribute));
                        }
                  }
                  return false;
            }
      }
}