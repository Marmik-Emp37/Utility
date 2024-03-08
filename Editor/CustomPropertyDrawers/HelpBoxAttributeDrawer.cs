using UnityEngine;

using UnityEditor;

namespace Emp37.Utility.Editor
{
      [CustomPropertyDrawer(typeof(HelpBoxAttribute))]
      internal class HelpBoxAttributeDrawer : DecoratorDrawer
      {
            private const float boxHeight = 40F;

            public override void OnGUI(Rect position)
            {
                  base.OnGUI(position);
                  var attribute = base.attribute as HelpBoxAttribute;

                  position.height = boxHeight;
                  EditorGUI.HelpBox(position, attribute.Message, (UnityEditor.MessageType) attribute.MessageType);
            }
            public override float GetHeight() => boxHeight + EditorGUIUtility.standardVerticalSpacing;
      }
}