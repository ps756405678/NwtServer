
using System;

namespace NwtServer.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MessageTypeAttribute : Attribute
    {
        public MessageTypeAttribute(string value)
        {
            Value = value;
        }
        public string Value { set; get; }
    }
}