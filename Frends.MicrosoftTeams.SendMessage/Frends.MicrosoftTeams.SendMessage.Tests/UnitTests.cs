using Frends.MicrosoftTeams.SendMessage.Definitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Frends.MicrosoftTeams.SendMessage.Tests;

[TestClass]
public class UnitTests
{
    private static readonly string? _clientId = Environment.GetEnvironmentVariable("TestTeams_ClientId");
    private static readonly string? _clientSecret = Environment.GetEnvironmentVariable("TestTeams_ClientSecret");
    private static readonly string? _testUserId = Environment.GetEnvironmentVariable("TestTeams_TestUserId");
    private static readonly string? _username = Environment.GetEnvironmentVariable("TestTeams_Username");
    private static readonly string? _password = Environment.GetEnvironmentVariable("TestTeams_Password");
    private static readonly string? _tenantId = Environment.GetEnvironmentVariable("TestTeams_TenantId");
    private static string _accessToken = "";
    private static bool _initialized = false;


    Input _input = new();
    Connection _connection = new();
    Options _options = new();

    [TestInitialize]
    public async Task Init()
    {
        // Workaround for MS tools "OneTimeSetup"
        if (!_initialized)
        {
            _accessToken = await CreateAccessToken();
            _initialized = true;
        }

        _connection = new()
        {
            ChannelId = "19%3aWjtkDM-KdSDaF8iWG_0Wcc5TRkAp5VkPugilCPwY3EM1%40thread.tacv2",
            TeamId = "4bfb0e07-d3e7-4924-a761-c46a5ef8f574",
            Token = _accessToken
        };

        _input = new()
        {
            Subject = "Test subject",
            MessageContent = "Test content.",
            SetAttachments = false,
            AttachmentParameters = null,
            SetMentions = false,
            MentionParameters = null,
            BodyType = BodyTypes.html
        };

        _options = new()
        {
            ThrowOnError = true,
        };
    }

    [TestMethod]
    public async Task Test_Send_Success()
    {
        var result = await MicrosoftTeams.SendMessage(_connection, _input, _options, default);
        Assert.IsTrue(result.Success);
        Assert.IsNull(result.ErrorMessage);
    }

    [TestMethod]
    public async Task Test_Send_Mentions_Success()
    {

        var mention = new[] {
            new MentionParameters()
            {
                MentionId = 0,
                AdditionalDataParameters = null,
                MentionText = "Teemu Tossavainen",
                UserDisplayName = "UserDisplayName",
                UserId = _testUserId
            },
            new MentionParameters()
            {
                MentionId = 1,
                AdditionalDataParameters = null,
                MentionText = "Borissov Jefim",
                UserDisplayName = "UserDisplayName",
                UserId = "78252de7-fc63-46a5-a903-3c43760f97dd"
            }
        };

        _input.MessageContent = "Hello <at id=\"0\">Teemu Tossavainen</at> <at id=\"1\">Borissov Jefim</at>";
        _input.SetMentions = true;
        _input.MentionParameters = mention;

        var result = await MicrosoftTeams.SendMessage(_connection, _input, _options, default);
        Assert.IsTrue(result.Success);
        Assert.IsNull(result.ErrorMessage);
    }

    [TestMethod]
    public async Task Test_Attachment_Success()
    {
        var attachment = new[]
        {
            new AttachmentParameters()
            {
                Id = "128b6438-5936-4093-97c9-44e0365222ab",
                ContentType = "reference",
                ContentUrl = "https://unthink.sharepoint.com/:t:/r/sites/FrendsTeamsTaskDevelopment/Shared%20Documents/General/TeamsTaskTest.txt",
                Name = "TeamsTaskTest.txt"
            },
            new AttachmentParameters()
            {
                Id = "6740d7bf-2d0c-498b-a5f5-40b309b101cd",
                ContentType = "reference",
                ContentUrl = "https://unthink.sharepoint.com/:t:/r/sites/FrendsTeamsTaskDevelopment/Shared%20Documents/General/TeamsTaskTest_Second.txt",
                Name = "TeamsTaskTest_Second.txt"
            },
        };

        _input.MessageContent = "Attachment 1: <attachment id=\"128b6438-5936-4093-97c9-44e0365222ab\"></attachment> -  Attachment 2: <attachment id=\"6740d7bf-2d0c-498b-a5f5-40b309b101cd\"></attachment>";

        _input.SetAttachments = true;
        _input.AttachmentParameters = attachment;

        var result = await MicrosoftTeams.SendMessage(_connection, _input, _options, default);
        Assert.IsTrue(result.Success);
        Assert.IsNull(result.ErrorMessage);
    }

    [TestMethod]
    public async Task Test_Mentions_Attachments_Success()
    {
        var attachment = new[]
        {
            new AttachmentParameters()
            {
                Id = "128b6438-5936-4093-97c9-44e0365222ab",
                ContentType = "reference",
                ContentUrl = "https://unthink.sharepoint.com/:t:/r/sites/FrendsTeamsTaskDevelopment/Shared%20Documents/General/TeamsTaskTest.txt",
                Name = "TeamsTaskTest.txt"
            },
            new AttachmentParameters()
            {
                Id = "6740d7bf-2d0c-498b-a5f5-40b309b101cd",
                ContentType = "reference",
                ContentUrl = "https://unthink.sharepoint.com/:t:/r/sites/FrendsTeamsTaskDevelopment/Shared%20Documents/General/TeamsTaskTest_Second.txt",
                Name = "TeamsTaskTest_Second.txt"
            },
        };

        var mentions = new[] {
            new MentionParameters()
            {
                MentionId = 0,
                AdditionalDataParameters = null,
                MentionText = "Teemu Tossavainen",
                UserDisplayName = "UserDisplayName",
                UserId = _testUserId
            },
            new MentionParameters()
            {
                MentionId = 1,
                AdditionalDataParameters = null,
                MentionText = "Borissov Jefim",
                UserDisplayName = "UserDisplayName",
                UserId = "78252de7-fc63-46a5-a903-3c43760f97dd"
            }
        };

        _input.MessageContent = "Hello <at id=\"0\">Teemu Tossavainen</at> and <at id=\"1\">Borissov Jefim</at>! Here's some test attachments: Attachment 1: <attachment id=\"128b6438-5936-4093-97c9-44e0365222ab\"></attachment> -  Attachment 2: <attachment id=\"6740d7bf-2d0c-498b-a5f5-40b309b101cd\"></attachment>";
        _input.SetAttachments = true;
        _input.AttachmentParameters = attachment;
        _input.SetMentions = true;
        _input.MentionParameters = mentions;

        var result = await MicrosoftTeams.SendMessage(_connection, _input, _options, default);
        Assert.IsTrue(result.Success);
        Assert.IsNull(result.ErrorMessage);
    }

    [TestMethod]
    public async Task Test_Exception_ThrowOnError_False()
    {
        var attachment = new[]
        {
            new AttachmentParameters()
            {
                Id = "Missing ID",
                ContentType = "reference",
                ContentUrl = "https://unthink.sharepoint.com/:t:/r/sites/FrendsTeamsTaskDevelopment/Shared%20Documents/General/TeamsTaskTest.txt",
                Name = "TeamsTaskTest.txt"
            },
        };

        _input.MessageContent = "Attachment 1: <attachment id=\"128b6438-5936-4093-97c9-44e0365222ab\"></attachment>";

        _input.SetAttachments = true;
        _input.AttachmentParameters = attachment;
        _options.ThrowOnError = false;

        var result = await MicrosoftTeams.SendMessage(_connection, _input, _options, default);
        Assert.IsFalse(result.Success);
        Assert.IsNotNull(result.ErrorMessage);
        Assert.IsTrue(result.ErrorMessage.Contains("Body does not contain marker for attachment with Id 'Missing ID'"));
    }

    [TestMethod]
    public async Task Test_Exception_ThrowOnError_True()
    {
        var attachment = new[]
        {
            new AttachmentParameters()
            {
                Id = "Missing ID",
                ContentType = "reference",
                ContentUrl = "https://unthink.sharepoint.com/:t:/r/sites/FrendsTeamsTaskDevelopment/Shared%20Documents/General/TeamsTaskTest.txt",
                Name = "TeamsTaskTest.txt"
            },
        };

        _input.MessageContent = "Attachment 1: <attachment id=\"128b6438-5936-4093-97c9-44e0365222ab\"></attachment>";

        _input.SetAttachments = true;
        _input.AttachmentParameters = attachment;
        _options.ThrowOnError = true;

        var ex = await Assert.ThrowsExceptionAsync<HttpRequestException>(() => MicrosoftTeams.SendMessage(_connection, _input, _options, default));
        Assert.IsTrue(ex.Message.Contains("Body does not contain marker for attachment with Id 'Missing ID'"));
    }

    private static async Task<string> CreateAccessToken()
    {
        Console.WriteLine("Creating AccessToken!");
        // Workflow debugging
        if (_tenantId != null)
            Console.WriteLine("_tenantId found, length: " + _tenantId.Length + ", first 3 chars: " + _tenantId.Substring(0, 3));
        if (_clientId != null)
            Console.WriteLine("_clientId found, length: " + _clientId.Length + ", first 3 chars: " + _clientId.Substring(0, 3));
        if (_clientSecret != null)
            Console.WriteLine("_clientSecret found, length: " + _clientSecret.Length + ", first 3 chars: " + _clientSecret.Substring(0, 3));
        if (_username != null)
            Console.WriteLine("_username found, length: " + _username.Length + ", first 3 chars: " + _username.Substring(0, 3));
        if (_password != null)
            Console.WriteLine("_password found, length: " + _password.Length + ", first 3 chars: " + _password.Substring(0, Math.Min(3, _password.Length)));

        using var client = new RestClient();
        RestRequest request = new($"https://login.microsoftonline.com/{_tenantId}/oauth2/v2.0/token", Method.Post);


        request.AddParameter("client_id", _clientId);
        request.AddParameter("client_secret", _clientSecret);
        request.AddParameter("scope", "https://graph.microsoft.com/.default");
        request.AddParameter("username", _username);
        request.AddParameter("password", _password);
        request.AddParameter("grant_type", "password");

        var response = await client.ExecuteAsync(request);
        var accessToken = JObject.Parse(response.Content)["access_token"]?.ToString() ?? "";
        Console.WriteLine("Token:" + accessToken);
        Console.WriteLine("Full response:" + (response.Content ?? "(empty)"));

        return accessToken;
    }

}