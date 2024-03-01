using UnityEditor;

namespace Emp37.Utility.Editor
{
      public static class SerializedPropertyExtensions
      {
            public static bool IsArrayElement(this SerializedProperty property) => property.propertyPath.Contains(".Array.data");
            public static SerializedProperty FindParentProperty(this SerializedProperty property)
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
                  return null;
            }
      }
}