using Kaskeset.Common.Requests;

namespace Kaskeset.Server.RequestHandeling.RequestHandlers
{
    public interface IRequestHandler
    {
        public void Handle(Request request);
    }
}
