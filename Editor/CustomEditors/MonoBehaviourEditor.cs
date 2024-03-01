using UnityEngine;

using UnityEditor;

namespace Emp37.Utility.Editor
{
      [CanEditMultipleObjects, CustomEditor(typeof(MonoBehaviour), true, isFallback = true)]
      internal class MonoBehaviourEditor : Emp37Editor
      {
      }
}