using UnityEngine;

using Emp37.Utility;

public class CommentsShowcase : MonoBehaviour
{
      [Comment("This is a basic comment.")]
      [Comment("This is a rich & <color=#FCF75E>tinted</color> comment.", Shades.Icterine)]
      [Comment("This is a rich, stylized & <color=#FFFFFF>tinted</color> comment.", Shades.White, FontStyle.BoldAndItalic)]
      [Readonly] public bool AllowMultiple = true;

      [Space(15F)]

      [Comment("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent hendrerit non ipsum id rutrum. Vivamus sit amet odio vel felis gravida ultricies id ac sapien. Quisque dictum laoreet feugiat. Sed lectus neque, condimentum non rhoncus eget, mattis ac ipsum. Sed eleifend a neque vel iaculis. Vivamus ut mollis est. Nunc vitae vulputate lacus. Morbi eu feugiat urna. Morbi et porttitor nunc, non aliquet orci. Cras vehicula, augue a vestibulum dignissim, purus velit mattis est, quis consectetur leo ipsum quis nisl. Praesent id scelerisque leo, nec commodo ex. Nullam ultrices posuere sodales. Vivamus tristique tincidunt massa, ac ultricies dolor. Quisque congue ornare justo, interdum sollicitudin libero laoreet vel. Pellentesque ac vehicula mi, id consectetur eros. Vivamus vel commodo ex, sed elementum nulla.", Shades.Violet, FontStyle.Italic)]
      public string Description = "A lorem ipsum text that tests the height calculation implemented in the attribute's drawer.";
      public int Next;
}