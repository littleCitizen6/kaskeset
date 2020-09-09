using Kaskeset.Common.Requests;
using Kaskeset.Server.CommonInfo;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Kaskeset.Server.RequestHandeling
{
    public class BasicRequestHandlingManeger : IRequestHandlingManeger
    {
        private ILogger _logger;
        private bool _continue;
        private RequestHandlerFactory _factory;
        private IStateInfo _stateInfo;
        public bool Continue => _continue;
        public BasicRequestHandlingManeger(IStateInfo stateInfo, ILogger logger)
        {
            _continue = true;
            _stateInfo = stateInfo;
            _factory = new RequestHandlerFactory(_stateInfo);
            _logger = logger;
        }
        public void Handle(byte[] msg, int bytesRec)
        {
            Request request;
            using (var memStream = new MemoryStream(msg))
            {
                memStream.Position = 0;
                var binForm = new BinaryFormatter();
                request = (Request) binForm.Deserialize(memStream);
                _logger.LogInformation($"an request has accepted: {request.Type}");
            }
            Task handle = new Task(() => _factory.Generate(request).Handle(request));
            handle.Start();
        }
    }
}
