using System;
using System.Collections.Generic;
using System.Reflection;

namespace Emp37.Utility
{
      using Flags = BindingFlags;

      public static class ReflectionUtility
      {
            private const Flags DefaultFlags = Flags.Instance | Flags.Static | Flags.Public | Flags.NonPublic;

            private static readonly Dictionary<string, MemberInfo> cachedMembers = new();
            public static MemberInfo FetchMember(string name, Type type, Flags binds = DefaultFlags)
            {
                  if (cachedMembers.TryGetValue(name, out var member))
                  {
                        return member;
                  }
                  while (type != null)
                  {
                        member = type.GetField(name, binds) ?? type.GetProperty(name, binds)?.GetGetMethod(binds.HasFlag(Flags.NonPublic)) ?? type.GetMethod(name, binds) as MemberInfo;
                        if (member != null)
                        {
                              cachedMembers[name] = member;
                              return member;
                        }
                        type = type.BaseType;
                  }
                  return null;
            }
            public static object GetValue(string name, object target, Flags bindings = DefaultFlags) => FetchMember(name, target?.GetType(), bindings).GetValue(target);

            public static bool GetBoolean(string name, object target, Flags bindings = DefaultFlags)
            {
                  if (GetValue(name, target, bindings) is bool value)
                  {
                        return value;
                  }
                  return false;
            }
      }
}