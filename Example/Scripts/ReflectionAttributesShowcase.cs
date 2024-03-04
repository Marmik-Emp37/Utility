using UnityEngine;

using Emp37.Utility;

public class ReflectionAttributesShowcase : MonoBehaviour
{
      [SerializeField] private bool showWhen = true;
      [ShowWhen(nameof(showWhen))]
      public string[] A;

      [Space(5F), Separator, Space(5F)]

      [SerializeField] private bool hideWhen;
      [HideWhen(nameof(hideWhen))]
      public string B;

      [Space(5F), Separator, Space(5F)]

      [EnableWhen(nameof(whenBoth))]
      public string Message1 = "Is enabled when both boolean above are true.";
      private bool whenBoth => showWhen && hideWhen;

      [DisableWhen(nameof(whenNone))]
      public string Message2 = "Is enabled when both boolean above are false.";
      private bool whenNone => showWhen || hideWhen;
}