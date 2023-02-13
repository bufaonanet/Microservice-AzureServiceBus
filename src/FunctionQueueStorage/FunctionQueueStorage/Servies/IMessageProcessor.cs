namespace FunctionQueueStorage.Servies
{
    public interface IMessageProcessor
    {
        void Process(string message);
    }
}