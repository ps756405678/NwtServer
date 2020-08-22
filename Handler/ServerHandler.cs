
using System;
using System.Text;
using DotNetty.Buffers;
using DotNetty.Transport.Channels;

namespace NwtServer
{
    public class ServerHandler : ChannelHandlerAdapter
    {
        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var buffer = message as IByteBuffer;
            if (buffer != null)
            {
                string msg = buffer.ToString(Encoding.UTF8);
                Console.WriteLine(msg);
                if (msg == "end")
                {
                    context.CloseAsync();
                    Console.WriteLine("Channel close...");
                }
            }
        }

        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            Console.WriteLine("Exception:" + exception);
            context.CloseAsync();
        }
    }
}