using System;
using System.Text;
using NwtServer.Core.Attributes;

namespace NwtServer.Message.Encoder
{
    [MessageContentType("Text")]
    public class TextMessageContentEncoder : MessageContentEncoderAdapter
    {
        public Tuple<int, byte[]> Encode(object messageContent)
        {
            string msgContent = messageContent as string;
            if (string.IsNullOrEmpty(msgContent))
            {
                return new Tuple<int, byte[]>(0, null);
            }
            else
            {
                byte[] data = Encoding.UTF8.GetBytes(msgContent);

                return new Tuple<int, byte[]>(data.Length, data);
            }
        }
    }
}