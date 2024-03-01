using System;

namespace Emp37.Utility
{
      /// <summary>
      /// Attribute to control the visibility of a field in the inspector.
      /// </summary>
      [AttributeUsage(AttributeTargets.Field)]
      public class HideWhenAttribute : Attribute
      {
            public readonly string Name;

            /// <param name="condition">The name of the boolean (field or property) member type.</param>
            public HideWhenAttribute(string condition) => Name = condition;
      }
}