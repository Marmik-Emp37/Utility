using UnityEngine;
using UnityEngine.UI;

namespace Emp37.Utility
{
      public static class ColorExtensions
      {
            public static Color32 WithAlpha(this Color32 color, byte value) => new(color.r, color.g, color.b, value);
            public static void ApplyShade(this Image renderer, Shades shade, byte alpha = byte.MaxValue) => renderer.color = ColorLibrary.Pick(shade).WithAlpha(alpha);
            public static void ApplyShade(this SpriteRenderer renderer, Shades shade, byte alpha = byte.MaxValue) => renderer.color = ColorLibrary.Pick(shade).WithAlpha(alpha);
      }
}