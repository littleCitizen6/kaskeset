namespace Kaskeset.Server.RequestHandeling.RequestHandlingManegment
{
    public interface IRequestHandlingManeger
    {
        bool Continue { get; }
        void Handle(byte[] msg, int bytesRec);
    }
}
