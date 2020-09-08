using Kaskeset.Common.Requests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Kaskeset.Client
{
    public class TcpClientHandler
    {
        private TcpClient _client;
        public TcpClientHandler(string adress, int port)
        {
            IPAddress ipAddress = IPAddress.Parse(adress);
            var remoteEP = new IPEndPoint(ipAddress, 9000);
            _client = new TcpClient(remoteEP);
            _client.Connect(remoteEP);
        }

        public void Send(Request request)
        {
            byte[] DataBytes;
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf1 = new BinaryFormatter();
            bf1.Serialize(ms, request);
            DataBytes = ms.ToArray();
            _client.GetStream().Write(DataBytes, 0, DataBytes.Length);
        }

        public string Recive()
        {
            byte[] dataBytes = new byte[_client.ReceiveBufferSize];
            int bytesRec = _client.GetStream().Read(dataBytes, 0, _client.ReceiveBufferSize);
            return Encoding.ASCII.GetString(dataBytes, 0, bytesRec);
        }
    }
}
