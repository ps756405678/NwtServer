using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using NwtServer.Core.Attributes;

namespace NwtServer.Message
{
    public abstract class MessageBase
    {
        public string FromUsr { set; get; }
        public List<string> ToUrs { private set; get; }
        public DateTime Time { private set; get; }
        public bool IsGroupMessage { private set; get; }

        public MessageBase(bool isGroup)
        {
            ToUrs = new List<string>();
            Time = DateTime.Now;
            IsGroupMessage = isGroup;
        }

        public byte[] Encode(Type type)
        {
            MemoryStream stream = new MemoryStream();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                List<Attribute> attributes = new List<Attribute>(property.GetCustomAttributes());
                var attributeQuery = from attribute in attributes
                            where attribute.GetType() == typeof(MessageContentTypeAttribute)
                            select attribute;
                
                if (attributeQuery.Count() > 0)
                {
                    string messageType = (attributeQuery.ToList()[0] as MessageContentTypeAttribute).Value;
                    Type[] encoderTypes = Assembly.Load("NwtServer.Message.Encoder").GetTypes();
                    foreach (Type encoderType in encoderTypes)
                    {
                        foreach (Attribute attribute in encoderType.GetCustomAttributes())
                        {
                            if (attribute.GetType() == typeof(MessageContentTypeAttribute))
                            {
                                string encoderMessageType = (attribute as MessageContentTypeAttribute).Value;
                                if (encoderMessageType == messageType)
                                {
                                    MessageContentEncoderAdapter encoder = (MessageContentEncoderAdapter)Assembly.Load("NwtServer.Message.Encoder")
                                                                            .CreateInstance(encoderType.FullName);
                                    Tuple<int, byte[]> encodeResult = encoder.Encode(property.GetValue(this));
                                    stream.Write(BitConverter.GetBytes(encodeResult.Item1));
                                    stream.Write(encodeResult.Item2);
                                }
                            }
                        }
                    }
                }
            }
            byte[] result = stream.ToArray();
            stream.Close();
            return result;
        }
    }
}