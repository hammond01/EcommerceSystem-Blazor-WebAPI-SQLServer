namespace Server.Repositories.Interfaces
{
    public interface IMessageProducer
    {
        void SendMessage<T>(T message, string apiType, string acction);
    }
}
