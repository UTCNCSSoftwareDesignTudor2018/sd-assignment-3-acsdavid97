using System.IO;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

namespace ArticleClientServerCommons
{
    public class Utils
    {
        public static byte[] ToBytes(object obj)
        {
            var serializer = new JsonSerializer();
            var writer = new StringWriter();

            serializer.Serialize(writer, obj);

            var data = Encoding.UTF8.GetBytes(writer.ToString() + "<EOM>");
            return data;
        }

        public static T FromString<T>(string data)
        {
            var serializer = new JsonSerializer();
            var reader = new StringReader(data);
            var jsonReader = new JsonTextReader(reader);
            T ojb = serializer.Deserialize<T>(jsonReader);
            return ojb;
        }

        public static string ReadMessage(NetworkStream stream)
        {
            var data = "";
            while (true)
            {
                var bytes = new byte[1024];
                var bytesRec = stream.Read(bytes, 0, 1024);
                data += Encoding.UTF8.GetString(bytes, 0, bytesRec);
                if (data.IndexOf(Constants.EndOfMessageMarker) > -1)
                {
                    break;
                }
            }
            var trimmedData = data.Substring(0, data.Length - Constants.EndOfMessageMarker.Length);
            return trimmedData;
        }

        public static T ReadObject<T>(NetworkStream stream)
        {
            var message = ReadMessage(stream);
            return FromString<T>(message);
        }

        public static void SendObject(object obj, NetworkStream stream)
        {
            var data = Utils.ToBytes(obj);
            stream.Write(data, 0, data.Length);
        }
    }
}
