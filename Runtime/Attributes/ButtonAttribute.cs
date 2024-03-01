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

            // size
            // show only at runtime functionality

            public ButtonAttribute(string text) => Text = text;
      }
}