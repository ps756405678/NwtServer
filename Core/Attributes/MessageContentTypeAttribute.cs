
using System;

namespace NwtServer.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class)]
    public class MessageContentTypeAttribute : Attribute
    {
        public string Value;
        public MessageContentTypeAttribute(string value)
        {
            Value = value;
        }
    }
}