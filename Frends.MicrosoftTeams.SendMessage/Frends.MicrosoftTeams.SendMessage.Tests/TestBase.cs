using dotenv.net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Frends.MicrosoftTeams.SendMessage.Tests;

public abstract class TestBase
{
    protected static string TeamId { get; private set; } = string.Empty;

    protected static string ChannelId { get; private set; } = string.Empty;

    protected static string AccessToken { get; private set; } = string.Empty;

    private static string ClientId { get; set; } = string.Empty;

    private static string ClientSecret { get; set; } = string.Empty;

    private static string Username { get; set; } = string.Empty;

    private static string Password { get; set; } = string.Empty;

    private static string TenantId { get; set; } = string.Empty;

    [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
    public static async Task BaseClassInitialize(TestContext context)
    {
        DotEnv.Load();
        ClientId = GetEnvVar("TestTeams_ClientId");
        ClientSecret = GetEnvVar("TestTeams_ClientSecret");
        Username = GetEnvVar("TestTeams_Username");
        Password = GetEnvVar("TestTeams_Password");
        TenantId = GetEnvVar("TestTeams_TenantId");
        TeamId = GetEnvVar("TestTeams_TeamId");
        ChannelId = GetEnvVar("TestTeams_ChannelId");

        AccessToken = await CreateAccessToken(context.CancellationTokenSource.Token);
    }

    private static async Task<string> CreateAccessToken(CancellationToken cancellationToken)
    {
        using var client = new RestClient();
        RestRequest request = new($"https://login.microsoftonline.com/{TenantId}/oauth2/v2.0/token", Method.Post);

        request.AddParameter("client_id", ClientId);
        request.AddParameter("client_secret", ClientSecret);
        request.AddParameter("scope", "https://graph.microsoft.com/.default");
        request.AddParameter("username", Username);
        request.AddParameter("password", Password);
        request.AddParameter("grant_type", "password");

        var response = await client.ExecuteAsync(request, cancellationToken);
        var json = string.IsNullOrEmpty(response.Content) ? new JObject() : JObject.Parse(response.Content);
        var accessToken = json["access_token"]?.ToString();

        return !string.IsNullOrEmpty(accessToken)
            ? accessToken
            : throw new InvalidOperationException(
                $"Failed to get access token ({(int)response.StatusCode}): {json["error_description"] ?? response.ErrorMessage}");
    }

    private static string GetEnvVar(string name)
    {
        var value = Environment.GetEnvironmentVariable(name);
        return !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new InvalidOperationException($"Missing required env var: {name}");
    }
}
