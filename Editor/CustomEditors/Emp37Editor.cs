using System;
using System.Collections.Generic;
using System.Reflection;

using UnityEngine;

using UnityEditor;

namespace Emp37.Utility.Editor
{
      internal class Emp37Editor : UnityEditor.Editor
      {
            private Type targetType;

            private SerializedProperty m_Script;
            private SerializedProperty[] serializedProperties;

            private bool shouldHideDefaultScript;


            private void OnEnable()
            {
                  targetType = target.GetType();
                  shouldHideDefaultScript = targetType.IsDefined(typeof(HideDefaultScriptAttribute));

                  #region F E T C H   P R O P E R T I E S
                  if (serializedProperties == null)
                  {
                        var properties = new List<SerializedProperty>();
                        var iterator = serializedObject.GetIterator();
                        while (iterator.NextVisible(true))
                        {
                              var property = serializedObject.FindProperty(iterator.name);
                              if (property != null)
                              {
                                    if (property.name == nameof(m_Script))
                                    {
                                          m_Script = property;
                                          continue;
                                    }
                                    properties.Add(property);
                              }
                        }
                        serializedProperties = properties.ToArray();
                  }
                  #endregion
            }
            public override void OnInspectorGUI()
            {
                  serializedObject.Update();
                  {
                        #region D E F A U L T   F I E L D
                        if (m_Script != null && !shouldHideDefaultScript)
                        {
                              GUI.enabled = false;
                              EditorGUILayout.PropertyField(m_Script);
                        }
                        #endregion

                        #region O T H E R   P R O P E R T I E S
                        foreach (var property in serializedProperties)
                        {
                              if (!EvaluateVisibility(property)) continue;

                              GUI.enabled = EvaluateEnabled(property);
                              EditorGUILayout.PropertyField(property);
                        }
                        #endregion
                  }
                  serializedObject.ApplyModifiedProperties();
            }

            private bool EvaluateEnabled(SerializedProperty property)
            {
                  if (property.TryGetAttribute(out EnableWhenAttribute enableWhenAttribute))
                  {
                        return ReflectionUtility.GetBool(enableWhenAttribute.ConditionName, target);
                  }
                  if (property.TryGetAttribute(out DisableWhenAttribute disableWhenAttribute))
                  {
                        return !ReflectionUtility.GetBool(disableWhenAttribute.ConditionName, target);
                  }
                  return true;
            }
            private bool EvaluateVisibility(SerializedProperty property)
            {
                  if (property.TryGetAttribute(out ShowWhenAttribute showWhenAttribute))
                  {
                        return ReflectionUtility.GetBool(showWhenAttribute.ConditionName, target);
                  }
                  if (property.TryGetAttribute(out HideWhenAttribute hideWhenAttribute))
                  {
                        return !ReflectionUtility.GetBool(hideWhenAttribute.ConditionName, target);
                  }
                  return true;
            }
      }
}