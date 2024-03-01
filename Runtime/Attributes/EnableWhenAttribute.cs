using System;

using UnityEngine;

namespace Emp37.Utility
{
      /// <summary>
      /// Attribute to control the behaviour of a field in the inspector.
      /// </summary>
      [AttributeUsage(AttributeTargets.Field)]
      public class EnableWhenAttribute : PropertyAttribute
      {
            public readonly string Name;

            /// <param name="condition">The name of the boolean (field or property) member type.</param>
            public EnableWhenAttribute(string condition) => Name = condition;
      }
}