using System;
using static System.Byte;

using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Emp37.Utility
{
      using static Shades;

      public static class ColorLibrary
      {
            #region L I B R A R Y
            private static readonly Color32 amaranth = new(229, 43, 80, MaxValue);
            private static readonly Color32 amethyst = new(153, 102, 204, MaxValue);
            private static readonly Color32 apricot = new(251, 206, 177, MaxValue);
            private static readonly Color32 aquamarine = new(127, 255, 212, MaxValue);
            private static readonly Color32 azure = new(0, 127, 255, MaxValue);
            private static readonly Color32 beige = new(245, 245, 220, MaxValue);
            private static readonly Color32 black = new(0, 0, 0, MaxValue);
            private static readonly Color32 blond = new(250, 240, 190, MaxValue);
            private static readonly Color32 blue = new(0, 0, 255, MaxValue);
            private static readonly Color32 brown = new(150, 75, 0, MaxValue);
            private static readonly Color32 cinnamon = new(210, 105, 30, MaxValue);
            private static readonly Color32 cherry = new(222, 49, 99, MaxValue);
            private static readonly Color32 chocolate = new(123, 63, 0, MaxValue);
            private static readonly Color32 cobalt = new(0, 71, 171, MaxValue);
            private static readonly Color32 coffee = new(111, 78, 55, MaxValue);
            private static readonly Color32 coral = new(255, 127, 80, MaxValue);
            private static readonly Color32 cottonCandy = new(255, 188, 217, MaxValue);
            private static readonly Color32 crimson = new(220, 20, 60, MaxValue);
            private static readonly Color32 cyan = new(0, 255, 255, MaxValue);
            private static readonly Color32 dandelion = new(240, 225, 48, MaxValue);
            private static readonly Color32 darkGrey = new(90, 90, 90, MaxValue);
            private static readonly Color32 editorText = new(185, 185, 185, MaxValue);
            private static readonly Color32 eggplant = new(97, 64, 81, MaxValue);
            private static readonly Color32 emerald = new(80, 200, 120, MaxValue);
            private static readonly Color32 forest = new(34, 139, 34, MaxValue);
            private static readonly Color32 gold = new(255, 215, 0, MaxValue);
            private static readonly Color32 green = new(0, 255, 0, MaxValue);
            private static readonly Color32 grey = new(127, 127, 127, MaxValue);
            private static readonly Color32 heliotrope = new(223, 115, 255, MaxValue);
            private static readonly Color32 honeydew = new(240, 255, 240, MaxValue);
            private static readonly Color32 icterine = new(252, 247, 94, MaxValue);
            private static readonly Color32 khaki = new(195, 176, 145, MaxValue);
            private static readonly Color32 lavender = new(230, 230, 250, MaxValue);
            private static readonly Color32 lemon = new(255, 247, 0, MaxValue);
            private static readonly Color32 lime = new(191, 255, 0, MaxValue);
            private static readonly Color32 linen = new(250, 240, 230, MaxValue);
            private static readonly Color32 magenta = new(255, 0, 255, MaxValue);
            private static readonly Color32 maroon = new(127, 0, 0, MaxValue);
            private static readonly Color32 mint = new(62, 180, 137, MaxValue);
            private static readonly Color32 mistyRose = new(255, 228, 225, MaxValue);
            private static readonly Color32 mustard = new(255, 219, 88, MaxValue);
            private static readonly Color32 olive = new(128, 128, 0, MaxValue);
            private static readonly Color32 onyx = new(15, 15, 15, MaxValue);
            private static readonly Color32 orange = new(255, 165, 0, MaxValue);
            private static readonly Color32 pear = new(209, 226, 49, MaxValue);
            private static readonly Color32 pink = new(255, 192, 203, MaxValue);
            private static readonly Color32 pistachio = new(147, 197, 114, MaxValue);
            private static readonly Color32 plum = new(221, 160, 221, MaxValue);
            private static readonly Color32 raspberry = new(227, 11, 93, MaxValue);
            private static readonly Color32 red = new(255, 0, 0, MaxValue);
            private static readonly Color32 richBlack = new(0, 64, 64, MaxValue);
            private static readonly Color32 rose = new(255, 0, 127, MaxValue);
            private static readonly Color32 ruby = new(224, 17, 95, MaxValue);
            private static readonly Color32 salmon = new(250, 128, 114, MaxValue);
            private static readonly Color32 seaGreen = new(0, 87, 51, MaxValue);
            private static readonly Color32 sienna = new(136, 45, 23, MaxValue);
            private static readonly Color32 silver = new(192, 192, 192, MaxValue);
            private static readonly Color32 skyblue = new(135, 206, 235, MaxValue);
            private static readonly Color32 tangerine = new(242, 133, 0, MaxValue);
            private static readonly Color32 teal = new(0, 127, 127, MaxValue);
            private static readonly Color32 tomato = new(255, 99, 71, MaxValue);
            private static readonly Color32 turquoise = new(48, 213, 200, MaxValue);
            private static readonly Color32 vanilla = new(243, 229, 171, MaxValue);
            private static readonly Color32 violet = new(238, 130, 238, MaxValue);
            private static readonly Color32 white = new(255, 255, 255, MaxValue);
            private static readonly Color32 whiteSmoke = new(245, 245, 238, MaxValue);
            private static readonly Color32 wisteria = new(201, 160, 220, MaxValue);
            private static readonly Color32 yellow = new(255, 255, 0, MaxValue);
            #endregion

            private static readonly int length = Enum.GetNames(typeof(Shades)).Length;

            public static Color32 Pick(Shades shade) => shade switch
            {
                  Amaranth => amaranth,
                  Amethyst => amethyst,
                  Apricot => apricot,
                  Aquamarine => aquamarine,
                  Azure => azure,
                  Beige => beige,
                  Black => black,
                  Blond => blond,
                  Blue => blue,
                  Brown => brown,
                  Cinnamon => cinnamon,
                  Cherry => cherry,
                  Chocolate => chocolate,
                  Cobalt => cobalt,
                  Coffee => coffee,
                  Coral => coral,
                  CottonCandy => cottonCandy,
                  Crimson => crimson,
                  Cyan => cyan,
                  Dandelion => dandelion,
                  DarkGrey => darkGrey,
                  EditorText => editorText,
                  Eggplant => eggplant,
                  Emerald => emerald,
                  Forest => forest,
                  Gold => gold,
                  Green => green,
                  Grey => grey,
                  Heliotrope => heliotrope,
                  Honeydew => honeydew,
                  Icterine => icterine,
                  Khaki => khaki,
                  Lavender => lavender,
                  Lemon => lemon,
                  Lime => lime,
                  Linen => linen,
                  Magenta => magenta,
                  Maroon => maroon,
                  Mint => mint,
                  MistyRose => mistyRose,
                  Mustard => mustard,
                  Olive => olive,
                  Onyx => onyx,
                  Orange => orange,
                  Pear => pear,
                  Pink => pink,
                  Pistachio => pistachio,
                  Plum => plum,
                  Raspberry => raspberry,
                  Red => red,
                  RichBlack => richBlack,
                  Rose => rose,
                  Ruby => ruby,
                  Salmon => salmon,
                  SeaGreen => seaGreen,
                  Sienna => sienna,
                  Silver => silver,
                  Skyblue => skyblue,
                  Tangerine => tangerine,
                  Teal => teal,
                  Tomato => tomato,
                  Turquoise => turquoise,
                  Vanilla => vanilla,
                  Violet => violet,
                  White => white,
                  WhiteSmoke => whiteSmoke,
                  Wisteria => wisteria,
                  Yellow => yellow,
                  _ => throw new NotImplementedException()
            };
            public static Color32 WithAlpha(this Color32 color, byte value) => new(color.r, color.g, color.b, value);
            public static Color32 PickRandom() => Pick((Shades) Random.Range(0, length));

            public static void ApplyShade(this Image renderer, Shades shade, byte alpha = MaxValue) => renderer.color = Pick(shade).WithAlpha(alpha);
            public static void ApplyShade(this SpriteRenderer renderer, Shades shade, byte alpha = MaxValue) => renderer.color = Pick(shade).WithAlpha(alpha);
      }
}