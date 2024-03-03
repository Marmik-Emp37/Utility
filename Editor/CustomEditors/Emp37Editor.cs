using System;
using System.Collections.Generic;
using System.Reflection;

using UnityEngine;

using UnityEditor;

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
                  if (property.name is defaultFieldIdentifier)
                  {
                        return hideDefaultField;
                  }
                  return true;
            }
            private bool EvaluateVisibility(SerializedProperty property)
            {
                  if (property.name is defaultFieldIdentifier)
                  {
                        return !hideDefaultField;
                  }
                  return true;
            }
      }
}