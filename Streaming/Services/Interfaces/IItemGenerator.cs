namespace Streaming.Services
{
    public interface IItemGenerator
    {
        public Task<object> GenerateOneAsync();
    }
}