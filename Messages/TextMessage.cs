
using NwtServer.Core.Attributes;

namespace NwtServer.Message
{
    [MessageType("TextMessage")]
    public class TextMessage : MessageBase
    {
        [MessageContentType("Text")]
        public string Content { set; get; }
        public TextMessage(bool isGroup) : base(isGroup)
        {

        }
    }
}