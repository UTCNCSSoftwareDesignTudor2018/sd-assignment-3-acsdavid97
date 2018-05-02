using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ArticleClientServerCommons;
using ArticleClientServerCommons.Dto;

namespace ArticleClient
{
    public class StateObject
    {
        // Client stream.  
        public NetworkStream NetworkStream = null;
        // Size of receive buffer.  
        public const int BufferSize = 256;
        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];
        // Received data string.  
        public StringBuilder sb = new StringBuilder();

        public string ReadData = String.Empty;
    }

    public class Client
    {
        public static ManualResetEvent ReadDone = new ManualResetEvent(false);
        public static IList<ArticleDto> ArticleDtos;

        public static int Main(String[] args)
        {
            Console.WriteLine("connecting");
            var client = new TcpClient("127.0.0.1", 11000);
            var stream = client.GetStream();
            Console.WriteLine("connected");

            GetPrintArticles(stream);

            while (true)
            {
            }

            return 0;
        }

        private static void GetPrintArticles(NetworkStream stream)
        {
            ReadDone.Reset();

            Utils.SendObject("GetArticles", stream);

            var state = new StateObject {NetworkStream = stream};

            stream.BeginRead(state.buffer, 0, StateObject.BufferSize, ReadCallback, state);

            ReadDone.WaitOne();

            var dtos = Utils.FromString<ArticleDto[]>(state.ReadData);
            ArticleDtos = dtos;
            foreach (var articleDto in dtos)
            {
                Console.WriteLine(articleDto);
            }
        }

        private static void ReadCallback(IAsyncResult ar)
        {
            var state = (StateObject) ar.AsyncState;
            var stream = state.NetworkStream;
            int bytesRead = stream.EndRead(ar);
            if (bytesRead > 0)
            {
                state.sb.Append(Encoding.UTF8.GetString(state.buffer));
                stream.BeginRead(state.buffer, 0, StateObject.BufferSize, ReadCallback, state);
            }
            else
            {
                state.ReadData = state.sb.ToString();
                ReadDone.Set();
                if (state.ReadData == "Update" + Constants.EndOfMessageMarker)
                {
                    Console.WriteLine("update arrived:");
                    GetPrintArticles(stream);
                }
            }
        }
    }
}