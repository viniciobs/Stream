namespace Streaming.Services
{
    public interface IStreamService
    {
        public Task Stream(Action<object> sendAction);
    }
}