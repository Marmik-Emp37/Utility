using System.Reflection;

namespace Emp37.Utility
{
      public static class MemberInfoExtensions
      {
            public static object GetValue(this MemberInfo info, object target) => info switch
            {
                  FieldInfo field => field.GetValue(target),
                  MethodInfo method => method.Invoke(target, null),

                  _ => null
            };
      }
}