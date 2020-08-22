
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using DotNetty.Transport.Channels;
using NwtServer.Core.Attributes;
using NwtServer.Message;

namespace NwtServer.Session
{
    public class Session : ISession
    {

        private IChannelHandlerContext _Cctx;

        public Session(IChannelHandlerContext cctx)
        {
            _Cctx = cctx;
        }

        public async void SendMessage(MessageBase message)
        {
            Type type = message.GetType();
            var typeQuery = from attribute in type.GetCustomAttributes()
                            where attribute.GetType() == typeof(MessageTypeAttribute)
                            select attribute;
            
            if (typeQuery.Count() > 0)
            {
                string messageType = (typeQuery as MessageTypeAttribute).Value;
                byte[] messageTypeData = Encoding.UTF8.GetBytes(messageType);
                await _Cctx.WriteAsync(BitConverter.GetBytes(messageTypeData.Length));
                await _Cctx.WriteAsync(messageTypeData);
                _Cctx.Flush();
            }
        }
    }
}