using System.Reflection;
using System.Collections.Generic;

namespace Emp37.Utility
{
      public static class Utility
      {
            private static readonly Dictionary<string, MemberInfo> cachedMembers = new();
            /// <summary>
            /// Looks up a field or property with the specified name in the type hierarchy of the target object using reflection.
            /// </summary>
            /// <param name="name">The name of the field or property to look up.</param>
            /// <param name="target">The target object whose type hierarchy will be traversed.</param>
            /// <param name="flags">The binding flags used in reflection to specify and other options.</param>
            /// <returns>
            /// The value of the field or property if found in the type hierarchy; otherwise, null.
            /// </returns>
            public static object ReflectiveLookup(string name, object target, BindingFlags flags = (BindingFlags) 60)
            {
                  var type = target.GetType();

                  while (type != null)
                  {
                        if (cachedMembers.TryGetValue(name, out var member)) return member switch
                        {
                              FieldInfo info => info.GetValue(target),
                              MethodInfo info => info?.Invoke(target, null),
                              _ => null
                        };

                        var field = type.GetField(name, flags);
                        if (field != null)
                        {
                              cachedMembers[name] = field;
                              continue;
                        }

                        var property = type.GetProperty(name, flags);
                        if (property != null)
                        {
                              var accessor = property.GetGetMethod(flags.HasFlag(BindingFlags.NonPublic));
                              if (accessor != null)
                              {
                                    cachedMembers[name] = accessor;
                                    continue;
                              }
                        }

                        type = type.BaseType;
                  }
                  return null;
            }
      }
}