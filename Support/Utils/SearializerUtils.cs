using System.IO;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Xml;

namespace Support.Utils
{

    [DataContract]
    public class SearializerUtils<T> where T : class, new()
    {

        public static T ReadXmlObject(string xmlFileName)
        {
            DataContractSerializer serializator = new DataContractSerializer(typeof(T));
            using (XmlReader xmlReader = XmlReader.Create(xmlFileName))
            {
                T entity = (T)serializator.ReadObject(xmlReader);
                return entity;
            }
        }

        public static T ReadJsonObject(string jsonFileName)
        {
            using (StreamReader streamReader = new StreamReader(jsonFileName))
            {
                T entity = JsonSerializer.Deserialize<T>(streamReader.ReadToEnd());
                return entity;
            }
        }
    }
}
