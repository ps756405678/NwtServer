
using System;

namespace NwtServer.Message
{
    public interface MessageContentEncoderAdapter
    {
        Tuple<int, byte[]> Encode(object messageContent);
    }
}