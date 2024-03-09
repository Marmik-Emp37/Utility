using System;
using System.Collections.Generic;

using UnityEngine;

namespace Emp37.Utility
{
      using Object = UnityEngine.Object;


      /// <summary>
      /// Singleton manager for registering and managing MonoBehaviours.
      /// </summary>
      public static class MonoRegistry
      {
            private static readonly Dictionary<Type, MonoBehaviour> database = new();

            /// <summary>
            /// Returns a formatted list of registered types from this registry.
            /// <br>Intended for logging and debugging purposes.</br>
            /// </summary>
            /// <remarks>This property provides a newline-separated string containing all registered types on this registry.</remarks>
            public static string Read => string.Join('\n', database.Keys);

            /// <summary>
            /// Retrieves the registered instance of a specific type.
            /// </summary>
            /// <typeparam name="TBehaviour">Type of the MonoBehaviour to retrieve.</typeparam>
            /// <returns>Instance of type T if found, otherwise null.</returns>
            /// <remarks>It is recommended to cache the return value for improved performance when conducting frequent lookups of the same type.</remarks>
            public static TBehaviour Lookup<TBehaviour>() where TBehaviour : MonoBehaviour
            {
                  var type = typeof(TBehaviour);

                  if (database.TryGetValue(type, out var instance))
                  {
                        return instance as TBehaviour;
                  }
                  else
                  {
                        Debug.LogWarning($"Instance of type '{type}' not found in the registry.");
                        return null;
                  }
            }

            /// <summary>
            /// Registers a MonoBehaviour instance in the registry.
            /// </summary>
            /// <param name="instance">The MonoBehaviour instance to register.</param>
            public static void Register(this MonoBehaviour instance)
            {
                  if (instance == null)
                  {
                        Debug.LogWarning("Instance is null.");
                        return;
                  }
                  var type = instance.GetType();
                  var message = $"Unable to register instance of type '{type}': ";

                  if (database.ContainsKey(type))
                  {
                        Debug.LogWarning(message + "This instance already exists in the database.");
                  }
                  else
                  {
                        database.Add(type, instance);
                        Debug.Log($"Registered instance of type '{type.Name}'.");
                  }
            }

            /// <summary>
            /// Unregisters a MonoBehaviour instance from the registry.
            /// </summary>
            /// <param name="instance">The MonoBehaviour instance to unregister.</param>
            public static void Unregister(this MonoBehaviour instance)
            {
                  if (instance == null)
                  {
                        Debug.LogWarning("Instance is null.");
                        return;
                  }
                  var type = instance.GetType();
                  var message = $"Unable to unregister instance of type '{type}': ";

                  if (database.ContainsKey(type))
                  {
                        if (database[type] != instance)
                        {
                              Debug.LogWarning(message + "The provided instance does not match the registered instance.");
                              return;
                        }
                        database.Remove(type);
                        Debug.Log($"Unregistered instance of type '{type.Name}'.");
                  }
                  else
                  {
                        Debug.LogWarning(message + "This instance does not exist in the database.");
                  }
            }

            /// <summary>
            /// Erases all entries from the registry, unregistering all registered instances.
            /// </summary>
            public static void Wipe()
            {
                  foreach (var key in database.Keys)
                  {
                        Debug.Log($"Unregistering instance of type '{key.FullName}'.");
                  }
                  database.Clear();
                  Debug.Log("Registry Wiped!");
            }
      }
}