using System;

using UnityEngine;

namespace Emp37.Utility
{
      /// <summary>
      /// Attribute for drawing a button in the inspector.
      /// </summary>
      [AttributeUsage(AttributeTargets.Method | AttributeTargets.Field)]
      public class ButtonAttribute : PropertyAttribute
      {
            public readonly string Text;
            public readonly int Size = 21;

            public ButtonAttribute(string text) => Text = text;
            public ButtonAttribute(string text, int size) : this(text) => Size = Mathf.Clamp(value: size, min: 18, max: 48);
      }
}