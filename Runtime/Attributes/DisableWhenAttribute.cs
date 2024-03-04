using System;

using UnityEngine;

namespace Emp37.Utility
{
      /// <summary>
      /// Attribute to conditionally disable the associated field in the Inspector.
      /// </summary>
      [AttributeUsage(AttributeTargets.Field)]
      public class DisableWhenAttribute : PropertyAttribute
      {
            public readonly string Name;

            /// <param name="condition">The name of the boolean member type as (field, property or method) on this target.</param>
            public DisableWhenAttribute(string condition) => Name = condition;
      }
}