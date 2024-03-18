using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

using UnityEngine;

using UnityEditor;

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

                  #region I N I T I A L I Z E   P R O P E R T I E S
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

                  #region I N I T I A L I Z E   M E T H O D S
                  methods = targetType.GetMethods(DEFAULT_FLAGS);
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
                              var field = FetchFieldInfo(property.name, targetType);
                              if (!EvaluateVisibility(field)) continue;

                              GUI.enabled = EvaluateEnabled(field);
                              EditorGUILayout.PropertyField(property);
                        }
                        #endregion

                        #region B U T T O N S
                        foreach (var method in methods)
                        {
                              if (!EvaluateVisibility(method)) continue;

                              GUI.enabled = EvaluateEnabled(method);
                              var button = method.GetCustomAttribute<ButtonAttribute>();
                              if (button != null)
                              {
                                    DrawButton(button, method);
                              }
                        }
                        #endregion

                        GUI.enabled = true;
                  }
                  serializedObject.ApplyModifiedProperties();
            }

            private bool EvaluateVisibility(MemberInfo member)
            {
                  var showWhen = member.GetCustomAttribute<ShowWhenAttribute>();
                  if (showWhen != null) if (GetValue(showWhen.ConditionName, target) is bool value) return value;

                  var hideWhen = member.GetCustomAttribute<HideWhenAttribute>();
                  if (hideWhen != null) if (GetValue(hideWhen.ConditionName, target) is bool value) return !value;

                  return true;
            }
            private bool EvaluateEnabled(MemberInfo member)
            {
                  var enableWhen = member.GetCustomAttribute<EnableWhenAttribute>();
                  if (enableWhen != null) if (GetValue(enableWhen.ConditionName, target) is bool value) return value;

                  var disableWhen = member.GetCustomAttribute<DisableWhenAttribute>();
                  if (disableWhen != null) if (GetValue(disableWhen.ConditionName, target) is bool value) return !value;

                  return true;
            }

            private void DrawButton(ButtonAttribute button, MethodInfo method)
            {
                  GUI.backgroundColor = ColorLibrary.Pick(button.Shade);

                  if (!GUILayout.Button(method.Name, GUILayout.Height(button.Height))) return;

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