using NwtServer.Message;

namespace NwtServer.Session
{
    public interface ISession
    {
        void SendMessage(MessageBase message);
    }
}