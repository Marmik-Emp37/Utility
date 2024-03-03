using UnityEngine;

using Emp37.Utility;

public class OtherShowcases : MonoBehaviour
{
      [Tooltip("Attribute defined as: [Max(25F)]")]
      [Max(25F)] public int X;

      [ExpandableObject]
      public ExampleObject ExpandableObject;

      [Title("Pythagorean Theorem")]
      [Readonly] public float Hypotenuse;
      [SerializeField, Indent(1), Label("Side")] private float side_1 = 4;
      [SerializeField, Indent(1), Label("Side")] private float side_2 = 1;

      [Separator(Shades.White, 1)]

      [Title("Example Title", Shades.Azure, Stretch = false)]
      public Color32 Picker;


      private void OnValidate()
      {
            Hypotenuse = side_1 * side_1 + side_2 * side_2;
      }
}