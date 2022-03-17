using StackExchange.Redis.Extensions.Core.Configuration;

namespace RedPhase.SharedDependencies;

public class ConfigurationBase
{
    public string ApiName { get; set; }

    public string AuthorityUrl { get; set; }

    public ConnectionStrings ConnectionStrings { get; set; }

    public CapConfig CapConfig { get; set; }

    public RedisConfiguration Redis { get; set; }
}

public class CapConfig
{
    public string SchemaName { get; set; }

    public int SucceedMessageExpiredAfter { get; set; } = 60 * 6000 * 1;

    public int ConsumerThreadCount { get; set; } = 5;

    public int ProducerThreadCount { get; set; } = 5;
}