using System;

using UnityEngine;

namespace Emp37.Utility
{
      [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
      public class HelpBoxAttribute : PropertyAttribute
      {
            public readonly string Message;
            public readonly MessageType MessageType;

            public HelpBoxAttribute(string message) => Message = message;
            public HelpBoxAttribute(string message, MessageType type) : this(message) => MessageType = type;
      }
}