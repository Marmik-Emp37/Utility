using System;
using System.Collections.Generic;
using System.Reflection;

using UnityEngine;

using UnityEditor;

namespace Emp37.Utility.Editor
{
      #region B A S E   E D I T O R S
      [CanEditMultipleObjects, CustomEditor(typeof(MonoBehaviour), true, isFallback = true)]
      internal class MonoBehaviourEditor : Emp37Editor
      {
      }

      [CanEditMultipleObjects, CustomEditor(typeof(ScriptableObject), true, isFallback = true)]
      internal class ScriptableObjectEditor : Emp37Editor
      {
      }
      #endregion

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
                        if (ReflectionUtility.GetValue(enableWhenAttribute.ConditionName, target) is bool value)
                        {
                              return value;
                        }
                  }
                  if (property.TryGetAttribute(out DisableWhenAttribute disableWhenAttribute))
                  {
                        if (ReflectionUtility.GetValue(disableWhenAttribute.ConditionName, target) is bool value)
                        {
                              return !value;
                        }
                  }
                  return true;
            }
            private bool EvaluateVisibility(SerializedProperty property)
            {
                  if (property.TryGetAttribute(out ShowWhenAttribute showWhenAttribute))
                  {
                        if (ReflectionUtility.GetValue(showWhenAttribute.ConditionName, target) is bool value)
                        {
                              return value;
                        }
                  }
                  if (property.TryGetAttribute(out HideWhenAttribute hideWhenAttribute))
                  {
                        if (ReflectionUtility.GetValue(hideWhenAttribute.ConditionName, target) is bool value)
                        {
                              return !value;
                        }
                  }
                  return true;
            }
      }
}