using UnityEngine;

using UnityEditor;

namespace Emp37.Utility.Editor
{
      [CanEditMultipleObjects, CustomEditor(typeof(ScriptableObject), true, isFallback = true)]
      internal class ScriptableObjectEditor : Emp37Editor
      {
      }
}