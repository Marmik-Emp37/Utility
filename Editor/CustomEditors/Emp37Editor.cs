using System;
using System.Collections.Generic;
using System.Reflection;

using UnityEngine;

using UnityEditor;
using System.Linq;
using UnityEditor.PackageManager;
using System.Security.Cryptography;
using Unity.Plastic.Newtonsoft.Json.Linq;

namespace Emp37.Utility.Editor
{
      using static ReflectionUtility;


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

            private MethodInfo[] methods;

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

                  methods = targetType.GetMethods(DEFAULT_FLAGS);
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

                        GUI.enabled = true;
                  }
                  serializedObject.ApplyModifiedProperties();

                  foreach (var method in methods)
                  {
                        var button = method.GetCustomAttribute<ButtonAttribute>();
                        if (button != null) DrawButton(button, method);
                  }
            }

            private bool EvaluateEnabled(SerializedProperty property)
            {
                  if (property.TryGetAttribute(out EnableWhenAttribute enableWhenAttribute))
                  {
                        if (GetValue(enableWhenAttribute.ConditionName, target) is bool value)
                        {
                              return value;
                        }
                  }
                  if (property.TryGetAttribute(out DisableWhenAttribute disableWhenAttribute))
                  {
                        if (GetValue(disableWhenAttribute.ConditionName, target) is bool value)
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
                        if (GetValue(showWhenAttribute.ConditionName, target) is bool value)
                        {
                              return value;
                        }
                  }
                  if (property.TryGetAttribute(out HideWhenAttribute hideWhenAttribute))
                  {
                        if (GetValue(hideWhenAttribute.ConditionName, target) is bool value)
                        {
                              return !value;
                        }
                  }
                  return true;
            }

            private void DrawButton(ButtonAttribute button, MethodInfo method)
            {
                  if (!GUILayout.Button(method.Name, GUILayout.Height(button.Height)))
                  {
                        return;
                  }

                  List<object> values = new();
                  ParameterInfo[] parameters = method.GetParameters();

                  if (parameters.Length > 0)
                  {
                        string[] paramNames = button.Parameters;

                        Assert(paramNames != null && paramNames.Length == parameters.Length, "Number of parameters specified does not match the expected number.");

                        for (byte i = 0; i < parameters.Length; i++)
                        {
                              object value = GetValue(paramNames[i], target, allowedTypes: MemberType.Field | MemberType.Property);

                              Assert(value != null, $"Unable to fetch value for '{paramNames[i]}' in type '{targetType.FullName}'. The member may not exist or may not be accessible.");

                              Type expectedType = parameters[i].ParameterType, parameterType = value.GetType();

                              Assert(expectedType == parameterType, $"Parameter type mismatch at index {i}. Expected type '{expectedType}' but recieved '{parameterType}'.");

                              values.Add(value);
                        }

                        void Assert(bool condition, string message)
                        {
                              if (condition) return;

                              string attribute = nameof(ButtonAttribute);
                              string signature = $"{targetType}.{method.Name}({string.Join(", ", parameters.Select(param => param.ParameterType.Name))})";

                              throw new ArgumentException($"Couldn't invoke method with [{attribute}] in '{signature}'.\n{message}");
                        }
                  }
                  method.Invoke(target, values.ToArray());
            }
      }
}