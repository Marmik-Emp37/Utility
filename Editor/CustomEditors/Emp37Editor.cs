using System;
using System.Collections.Generic;
using System.Reflection;

using UnityEngine;

using UnityEditor;
using UnityEngine.UIElements;

namespace Emp37.Utility.Editor
{
      internal class Emp37Editor : UnityEditor.Editor
      {
            private SerializedProperty[] properties = null;

            private bool hideDefaultField;
            private const string defaultFieldIdentifier = "m_Script";

            private Type type;



            private void OnEnable()
            {
                  type = target.GetType();

                  hideDefaultField = type.IsDefined(typeof(HideDefaultScriptAttribute));

                  #region F E T C H I N G   P R O P E R T I E S
                  if (properties == null)
                  {
                        var properties = new List<SerializedProperty>();
                        var iterator = serializedObject.GetIterator();
                        while (iterator.NextVisible(true))
                        {
                              var property = serializedObject.FindProperty(iterator.name);
                              if (property != null)
                              {
                                    properties.Add(property);
                              }
                        }
                        this.properties = properties.ToArray();
                  }
                  #endregion
            }

            public override void OnInspectorGUI()
            {
                  serializedObject.Update();
                  {
                        foreach (var property in properties)
                        {
                              if (EvaluateVisibility(property))
                              {
                                    GUI.enabled = EvaluateEnabled(property);
                                    EditorGUILayout.PropertyField(property);
                              }
                        }
                  }
                  serializedObject.ApplyModifiedProperties();
            }

            private bool EvaluateEnabled(SerializedProperty property)
            {
                  if (property.name == defaultFieldIdentifier)
                  {
                        return hideDefaultField;
                  }
                  if (property.TryGetAttribute<EnableWhenAttribute>(out var enable))
                  {
                        return ReflectionUtility.GetBoolean(enable.Name, target);
                  }
                  if (property.TryGetAttribute<DisableWhenAttribute>(out var disable))
                  {
                        return !ReflectionUtility.GetBoolean(disable.Name, target);
                  }
                  return true;
            }
            private bool EvaluateVisibility(SerializedProperty property)
            {
                  if (property.name == defaultFieldIdentifier)
                  {
                        return !hideDefaultField;
                  }
                  if (property.TryGetAttribute<ShowWhenAttribute>(out var show))
                  {
                        return ReflectionUtility.GetBoolean(show.Name, target);
                  }
                  if (property.TryGetAttribute<HideWhenAttribute>(out var hide))
                  {
                        return !ReflectionUtility.GetBoolean(hide.Name, target);
                  }
                  return true;
            }
      }
}