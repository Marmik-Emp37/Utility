using UnityEngine;

using UnityEditor;

namespace Emp37.Utility.Editor
{
      [CustomPropertyDrawer(typeof(MaxAttribute))]
      internal class MaxAttributeDrawer : PropertyDrawer
      {
            public override void OnPropertyDraw(Rect position, SerializedProperty property, GUIContent label)
            {
                  if (property.propertyType is not (SerializedPropertyType.Float or SerializedPropertyType.Integer or SerializedPropertyType.Vector2 or SerializedPropertyType.Vector3 or SerializedPropertyType.Vector2Int or SerializedPropertyType.Vector3Int))
                  {
                        EditorGUI.HelpBox(position, "Use MaxAttribute on 'Floating' or 'Integer' field types.", MessageType.Error);
                        return;
                  }
                  EditorGUI.BeginChangeCheck();
                  EditorGUI.PropertyField(position, property, label);
                  if (EditorGUI.EndChangeCheck())
                  {
                        var attribute = base.attribute as MaxAttribute;
                        switch (property.propertyType)
                        {
                              #region F L O A T
                              case SerializedPropertyType.Float:
                                    {
                                          property.floatValue = limitSingle(property.floatValue);
                                          break;
                                    }
                              case SerializedPropertyType.Vector2:
                                    {
                                          var value = property.vector2Value;
                                          property.vector2Value = new(x: limitSingle(value.x), y: limitSingle(value.y));
                                          break;
                                    }
                              case SerializedPropertyType.Vector3:
                                    {
                                          var value = property.vector3Value;
                                          property.vector3Value = new(x: limitSingle(value.x), y: limitSingle(value.y), z: limitSingle(value.z));
                                          break;
                                    }
                              #endregion

                              #region I N T E G E R
                              case SerializedPropertyType.Integer:
                                    {
                                          property.intValue = limitInt(property.intValue);
                                          break;
                                    }
                              case SerializedPropertyType.Vector2Int:
                                    {
                                          var value = property.vector2IntValue;
                                          property.vector2IntValue = new(x: limitInt(value.x), y: limitInt(value.y));
                                          break;
                                    }
                              case SerializedPropertyType.Vector3Int:
                                    {
                                          var value = property.vector3IntValue;
                                          property.vector3IntValue = new(x: limitInt(value.x), y: limitInt(value.y), z: limitInt(value.z));
                                          break;
                                    }
                                    #endregion
                        }
                        int limitInt(int value) => Mathf.Clamp(value, int.MinValue, (int) attribute.Value); float limitSingle(float value) => Mathf.Clamp(value, float.MinValue, attribute.Value);
                  }
            }
      }
}