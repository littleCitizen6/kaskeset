using Kaskeset.Common.Requests;
using Kaskeset.Server.CommonInfo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kaskeset.Server.RequestHandeling.RequestHandlers
{
    public interface IRequestHandler
    {
        public void Handle(Request request);
    }
}
