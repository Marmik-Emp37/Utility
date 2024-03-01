using UnityEngine;

using UnityEditor;
using System;

namespace Emp37.Utility.Editor
{
      public abstract class PropertyDrawer : UnityEditor.PropertyDrawer
      {
            private bool init = false;

            public virtual void Initialize(SerializedProperty property) { }
            public abstract void OnPropertyDraw(Rect position, SerializedProperty property, GUIContent label);

            public sealed override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                  if (!init)
                  {
                        Initialize(property);
                        init = true;
                        return;
                  }
                  using (new EditorGUI.PropertyScope(position, label, property))
                  {
                        OnPropertyDraw(position, property, label);
                  }
            }
            public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => EditorGUI.GetPropertyHeight(property, label);
      }
}