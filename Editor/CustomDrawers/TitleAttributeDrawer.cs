using UnityEngine;

using UnityEditor;

namespace Emp37.Utility.Editor
{
      using static ColorLibrary;


      [CustomPropertyDrawer(typeof(TitleAttribute))]
      internal class TitleAttributeDrawer : DecoratorDrawer
      {
            private const float UnderlineHeight = 3F;

            private readonly GUIStyle style = new(EditorStyles.boldLabel)
            {
                  fontSize = 14,
                  stretchHeight = false,
            };

            private Vector2 size;

            private TitleAttribute Attribute => attribute as TitleAttribute;


            public override void Initialize()
            {
                  if (!Attribute.Stretch)
                  {
                        style.CalcMinMaxWidth(Attribute.Content, out _, out float max);
                        size.x = max;
                  }
                  size.y = style.CalcHeight(Attribute.Content, EditorGUIHelper.ReleventWidth);
            }
            public override void OnGUI(Rect position)
            {
                  base.OnGUI(position);
                  position.height = size.y;

                  style.normal.textColor = Pick(Attribute.Text);
                  EditorGUI.LabelField(position, Attribute.Content, style);

                  position.y += position.height + EditorGUIUtility.standardVerticalSpacing; // - [ 1 ]
                  if (!Attribute.Stretch)
                  {
                        position.width = size.x;
                  }
                  position.height = UnderlineHeight; // - [ 2 ]
                  EditorGUI.DrawRect(position, Pick(Attribute.Underline));
            }
            public override float GetHeight() => size.y + 2F * EditorGUIUtility.standardVerticalSpacing /* - [ 1 ] + extra spacing*/ + UnderlineHeight /* - [ 2 ]*/;
      }
}