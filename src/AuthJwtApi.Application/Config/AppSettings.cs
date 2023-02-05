namespace AuthJwtApi.Application.Config
{
    public class AppSettings
    {
        public DefaultConnection DefaultConnection { get; set; } = null!;
        public string SecurityKey { get; set; } = null!;
    }

    public class DefaultConnection
    {
        public string CacheConnectionString { get; set; } = null!;
    }
}
