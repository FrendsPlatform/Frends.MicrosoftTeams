using dotenv.net;

namespace Frends.MicrosoftTeams.SendMessage.Tests;

public abstract class TestBase
{
    protected TestBase()
    {
        DotEnv.Load();
        ClientId = GetEnvVar("TestTeams_ClientId");
        ClientSecret = GetEnvVar("TestTeams_ClientSecret");
        Username = GetEnvVar("TestTeams_Username");
        Password = GetEnvVar("TestTeams_Password");
        TenantId = GetEnvVar("TestTeams_TenantId");
        TeamId = GetEnvVar("TestTeams_TeamId");
        ChannelId = GetEnvVar("TestTeams_ChannelId");
    }

    protected string ClientId { get; }

    protected string ClientSecret { get; }

    protected string Username { get; }

    protected string Password { get; }

    protected string TenantId { get; }

    protected string TeamId { get; }

    protected string ChannelId { get; }

    private static string GetEnvVar(string name)
    {
        var value = Environment.GetEnvironmentVariable(name);
        return !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new InvalidOperationException($"Missing required env var: {name}");
    }
}
