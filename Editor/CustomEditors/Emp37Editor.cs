using System.Collections.Generic;
using System.Reflection;

using UnityEditor;

namespace Emp37.Utility.Editor
{
      internal class Emp37Editor : UnityEditor.Editor
      {
            private SerializedProperty defaultProperty;
            private bool shouldHideDefaultScript;

            private SerializedProperty[] properties;

            private void OnEnable()
            {
                  var type = target.GetType();
                  shouldHideDefaultScript = type.IsDefined(typeof(HideDefaultScriptAttribute));

                  #region F E T C H I N G   P R O P E R T I E S
                  var properties = new List<SerializedProperty>();
                  var iterator = serializedObject.GetIterator();
                  while (iterator.NextVisible(true))
                  {
                        var property = serializedObject.FindProperty(iterator.name);

                        if (property == null) continue;
                        if (property.name == "m_Script")
                        {
                              defaultProperty = property;
                              continue;
                        }
                        properties.Add(property);
                  }
                  this.properties = properties.ToArray();
                  #endregion
            }

            public override void OnInspectorGUI()
            {
                  serializedObject.Update();
                  {
                        #region D E F A U L T   S C R I P T   F I E L D
                        if (!shouldHideDefaultScript)
                        {
                              using (new EditorGUI.DisabledScope(true))
                              {
                                    EditorGUILayout.PropertyField(defaultProperty, false);
                              }
                        }
                        #endregion

                        foreach (var property in properties)
                        {
                              EditorGUILayout.PropertyField(property);
                        }
                  }
                  serializedObject.ApplyModifiedProperties();
            }
      }
}