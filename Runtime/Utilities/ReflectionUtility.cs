using System;
using System.Collections.Generic;
using System.Reflection;

namespace Emp37.Utility
{
      public static class ReflectionUtility
      {
            public const BindingFlags DEFAULT_FLAGS = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

            private static readonly Dictionary<(Type, string), FieldInfo> cachedFieldInfo = new();
            private static readonly Dictionary<(Type, string), PropertyInfo> cachedPropertyInfo = new();
            private static readonly Dictionary<(Type, string), MethodInfo> cachedMethodInfo = new();

            private static void VerifyKeyValues(Type type, string name)
            {
                  if (type == null) throw new ArgumentNullException(nameof(type));
                  if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            }
            public static FieldInfo FetchFieldInfo(string name, Type type, BindingFlags bindings = DEFAULT_FLAGS)
            {
                  VerifyKeyValues(type, name);

                  var key = (type, name);
                  if (!cachedFieldInfo.TryGetValue(key, out FieldInfo field))
                  {
                        while (type != null)
                        {
                              field = type.GetField(name, bindings);
                              if (field != null)
                              {
                                    cachedFieldInfo[key] = field;
                              }
                              type = type.BaseType;
                        }
                  }
                  return field;
            }
            public static PropertyInfo FetchPropertyInfo(string name, Type type, BindingFlags bindings = DEFAULT_FLAGS)
            {
                  VerifyKeyValues(type, name);

                  var key = (type, name);
                  if (!cachedPropertyInfo.TryGetValue(key, out PropertyInfo property))
                  {
                        while (type != null)
                        {
                              property = type.GetProperty(name, bindings);
                              if (property != null)
                              {
                                    cachedPropertyInfo[key] = property;
                              }
                              type = type.BaseType;
                        }
                  }
                  return property;
            }
            public static MethodInfo FetchMethodInfo(string name, Type type, BindingFlags bindings = DEFAULT_FLAGS)
            {
                  VerifyKeyValues(type, name);

                  var key = (type, name);
                  if (!cachedMethodInfo.TryGetValue(key, out MethodInfo method))
                  {
                        while (type != null)
                        {
                              method = type.GetMethod(name, bindings);
                              if (method != null)
                              {
                                    cachedMethodInfo[key] = method;
                              }
                              type = type.BaseType;
                        }
                  }
                  return method;
            }
            public static object GetFieldValue(string name, object target, BindingFlags bindings = DEFAULT_FLAGS)
            {
                  FieldInfo field = FetchFieldInfo(name, target?.GetType(), bindings);
                  if (field != null)
                  {
                        return field.GetValue(target);
                  }
                  return null;
            }
            public static object GetPropertyValue(string name, object target, BindingFlags bindings = DEFAULT_FLAGS)
            {
                  PropertyInfo property = FetchPropertyInfo(name, target?.GetType(), bindings);
                  if (property != null && property.CanRead)
                  {
                        return property.GetValue(target);
                  }
                  return null;
            }
            public static object GetMethodValue(string name, object target, BindingFlags bindings = DEFAULT_FLAGS, object[] parameters = null)
            {
                  MethodInfo method = FetchMethodInfo(name, target?.GetType(), bindings);
                  if (method != null)
                  {
                        return method.Invoke(target, parameters);
                  }
                  return null;
            }
            public static object GetValue(string name, object target, BindingFlags bindings = DEFAULT_FLAGS) => GetFieldValue(name, target, bindings) ?? GetPropertyValue(name, target, bindings) ?? GetMethodValue(name, target, bindings);
            public static bool GetBool(string name, object target, BindingFlags bindings = DEFAULT_FLAGS) => GetValue(name, target, bindings) is bool value && value;
      }
}