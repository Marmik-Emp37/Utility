using System;
using System.Reflection;

using UnityEditor;

namespace Emp37.Utility.Editor
{
      public static class SerializedPropertyExtensions
      {
            public static bool IsArrayElement(this SerializedProperty property) => property.propertyPath.Contains(".Array.data");
            public static SerializedProperty FindParentProperty(this SerializedProperty property)
            {
                  if (property != null)
                  {
                        var path = property.propertyPath;
                        if (path.Contains('.'))
                        {
                              var names = path.Split('.');
                              for (int i = names.Length - 2; i >= 0; i--)
                              {
                                    if (names[i] != "Array")
                                          return property.serializedObject.FindProperty(names[i]);
                              }
                        }
                  }
                  return null;
            }
            public static TAttribute GetAttribute<TAttribute>(this SerializedProperty property) where TAttribute : Attribute
            {
                  if (property == null) throw new ArgumentNullException();
                  var target = property.serializedObject.targetObject ?? throw new ArgumentException($"The type of the type object for the serialized property of name '{property.name}' is null.");

                  return ReflectionUtility.FetchMember(property.name, target.GetType())?.GetCustomAttribute<TAttribute>(true);
            }
            public static bool TryGetAttribute<TAttribute>(this SerializedProperty property, out TAttribute attribute) where TAttribute : Attribute
            {
                  attribute = GetAttribute<TAttribute>(property);
                  return attribute != null;
            }
      }
}