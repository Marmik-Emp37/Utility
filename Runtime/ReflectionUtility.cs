using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Emp37.Utility
{
      public static class ReflectionUtility
      {
            private const BindingFlags DefaultBindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;


            private static readonly Dictionary<string, MemberInfo> members = new();
            public static MemberInfo FetchMember(string name, object target, BindingFlags flags = DefaultBindingFlags)
            {
                  if (members.TryGetValue(name, out var member))
                  {
                        return member;
                  }
                  if (target != null)
                  {
                        var type = target.GetType();
                        while (type != null)
                        {
                              member = type.GetField(name, flags) ?? type.GetProperty(name, flags) ?? type.GetMethod(name, flags) as MemberInfo;
                              if (member != null)
                              {
                                    members[name] = member;
                                    return member;
                              }
                              type = type.BaseType;
                        }
                  }
                  return null;
            }
            public static bool TryFetchMember(out MemberInfo member, string name, object target, BindingFlags flags = DefaultBindingFlags)
            {
                  member = FetchMember(name, target, flags);
                  return member != null;
            }

            public static object FetchMemberValue(MemberInfo member, object target, object[] parameters = null) => member switch
            {
                  FieldInfo field => field.GetValue(target),
                  PropertyInfo property => property.GetValue(target),
                  MethodInfo method => method.Invoke(target, parameters),
                  _ => null
            };
      }
}