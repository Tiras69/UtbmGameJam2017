using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public class XmlSerializerHelper<T> where T : class
{
    
    public static void SerializeXmlFile(string _fileName, T _law)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using(StreamWriter writer = new StreamWriter(_fileName))
        {
            serializer.Serialize(writer, _law);
        }
    }

    public static T DeserializeXmlFile(string _fileName)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using (StreamReader reader = new StreamReader(_fileName))
        {
            return serializer.Deserialize(reader) as T;
        }

        return default(T);
    }

}
