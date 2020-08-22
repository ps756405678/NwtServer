using System.Collections;
using System.Collections.Generic;
using NwtServer.Session;

namespace NwtServer.Core
{
    public class SessionStorage
    {
        private static SessionStorage _Instance;

        private Dictionary<string, ISession> _Container;

        private SessionStorage()
        {
            _Container = new Dictionary<string, ISession>();
        }

        public static SessionStorage GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new SessionStorage();
            }
            return _Instance;
        }

        public void AddSession(string usrId, ISession session)
        {
            lock (_Container)
            {
                _Container.Add(usrId, session);
            }
        }

        public void RemoveSession(string usrId)
        {
            lock (_Container)
            {
                if (_Container.ContainsKey(usrId))
                {
                    _Container.Remove(usrId);
                }
            }
        }

        public bool IsOnline(string usrId)
        {
            lock (_Container)
            {
                return _Container.ContainsKey(usrId);
            }
        }

        public List<string> GetAllOnline()
        {
            lock (_Container)
            {
                return new List<string>(_Container.Keys);
            }
        }
    }
}