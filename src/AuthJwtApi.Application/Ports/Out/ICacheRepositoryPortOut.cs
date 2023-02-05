namespace AuthJwtApi.Application.Ports.Out
{
    public interface ICacheRepositoryPortOut
    {
        public Task<string> GetCacheValueAsync(string key);
        public Task SetCacheValueAsync(string key, object value);
    }
}
