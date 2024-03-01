using UnityEngine;

using Emp37.Utility;

public class OtherExamples : MonoBehaviour
{
      [Tooltip("Attribute defined as: [Max(25F)]")]
      [Max(25F)] public int X;

      [ExpandableObject]
      public ExampleObject ExpandableObject;

      [Separator(Shades.Silver)]

      [Header("Pythagorean Theorem")]
      public int Hypotenuse;
      [SerializeField, Indent(1), Label("Side")] private int side_1 = 4;
      [SerializeField, Indent(1), Label("Side")] private int side_2 = 1;


      private void OnValidate()
      {
            Hypotenuse = side_1 * side_1 + side_2 * side_2;
      }
}