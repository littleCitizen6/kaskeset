﻿using Kaskeset.Common.Requests;
using Kaskeset.Server.ClientInfo;
using Kaskeset.Server.RequestHandeling.RequestHandlingManegment;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Kaskeset.Server.ClientsConnection
{
    public class TcpClientConnection : IClientConnection
    {
        private ILogger _logger;
        private TcpClient _client;
        private IRequestHandlingManeger _requestHandler;
        public bool IsConnected => _client.Connected;

        public IClientInfo Info { get; set; }

        public TcpClientConnection(TcpClient client, IRequestHandlingManeger requestHandler, ILogger logger)
        {
            Info = new BasicClientInfo(); 
            _client = client;
            _requestHandler = requestHandler;
            _logger = logger;
            _logger.LogInformation($"Sending Id {Info.Id}");
            Send(Info.Id.ToString());
        }
        public void Dispose()
        {
            _client.Dispose();
        }

        public void Run()
        {
            Byte[] bytes = new Byte[_client.ReceiveBufferSize];
            int bytesRec;
            while (_requestHandler.Continue && IsConnected)
            {
                try
                {
                    bytesRec = _client.GetStream().Read(bytes, 0, bytes.Length);
                    _requestHandler.Handle(bytes, bytesRec);
                }
                catch (IOException)
                {
                    _logger.LogInformation($"client left : {Info.Name}::{Info.Id}");
                }
                catch (Exception e)
                {
                    _logger.LogError($"exception :{e}");
                }
            }
        }

        public void Send(string response)
        {
            _logger.LogInformation($"sending value : {response} for {Info}");
            _client.GetStream().Write(Encoding.ASCII.GetBytes(response));
        }

        public void Send(Request response)
        {
            _logger.LogInformation($"sending response : {response.Type} for {Info}");
            byte[] DataBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf1 = new BinaryFormatter();
                bf1.Serialize(ms, response);
                DataBytes = ms.ToArray();
            }
            _client.GetStream().Write(DataBytes, 0, DataBytes.Length);
        }
    }
}
