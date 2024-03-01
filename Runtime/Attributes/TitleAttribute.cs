using System;

using UnityEngine;

namespace Emp37.Utility
{
      /// <summary>
      /// Attribute used for adding titles above any field in the inspector.
      /// </summary>
      [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
      public class TitleAttribute : PropertyAttribute
      {
            public readonly string Title;
            public readonly Shades Text = Shades.Silver, Underline = Shades.DarkGrey;
            /// <summary>
            /// Specifies weather the underline should stretch to default width or adjust to the title width.
            /// Default is true, indicating that the underline stretches.
            /// </summary>
            public bool Stretch = true;

            public TitleAttribute(string title) => Title = title;
            public TitleAttribute(string title, Shades shade) : this(title) => Text = Underline = shade;
            public TitleAttribute(string title, Shades text, Shades underline) : this(title)
            {
                  Text = text;
                  Underline = underline;
            }
      }
}