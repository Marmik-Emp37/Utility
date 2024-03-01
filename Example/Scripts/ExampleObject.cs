using UnityEngine;

[Emp37.Utility.HideDefaultScript]
[CreateAssetMenu]
public class ExampleObject : ScriptableObject
{
      public bool IsActive;
      [Range(1F, 10F)] public float Amount = 1F;
      [TextArea(5, 5)] public string Story;
}