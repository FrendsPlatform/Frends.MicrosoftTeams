using Frends.MicrosoftTeams.SendMessage.Definitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Frends.MicrosoftTeams.SendMessage.Tests;

[TestClass]
public class UnitTests
{
    private static readonly string? ClientId = Environment.GetEnvironmentVariable("TestTeams_ClientId");
    private static readonly string? ClientSecret = Environment.GetEnvironmentVariable("TestTeams_ClientSecret");
    private static readonly string? TestUserId = Environment.GetEnvironmentVariable("TestTeams_TestUserId");
    private static readonly string? Username = Environment.GetEnvironmentVariable("TestTeams_Username");
    private static readonly string? Password = Environment.GetEnvironmentVariable("TestTeams_Password");
    private static readonly string? TenantId = Environment.GetEnvironmentVariable("TestTeams_TenantId");
    private static string accessToken = string.Empty;
    private static bool initialized;

    private Input input = new();
    private Connection connection = new();
    private Options options = new();

    [TestInitialize]
    public async Task Init()
    {
        // Workaround for MS tools "OneTimeSetup"
        if (!initialized)
        {
            accessToken = await CreateAccessToken();
            initialized = true;
        }

        connection = new()
        {
            ChannelId = "19%3aWjtkDM-KdSDaF8iWG_0Wcc5TRkAp5VkPugilCPwY3EM1%40thread.tacv2",
            TeamId = "4bfb0e07-d3e7-4924-a761-c46a5ef8f574",
            Token = accessToken,
        };

        input = new()
        {
            Subject = "Test subject",
            MessageContent = "Test content.",
            SetAttachments = false,
            AttachmentParameters = null,
            SetMentions = false,
            MentionParameters = null,
            BodyType = BodyTypes.Html,
        };

        options = new()
        {
            ThrowOnError = true,
        };
    }

    [TestMethod]
    public async Task Test_Send_Success()
    {
        var result = await MicrosoftTeams.SendMessage(input, connection, options, default);
        Assert.IsTrue(result.Success);
        Assert.IsNull(result.ErrorMessage);
    }

    [TestMethod]
    public async Task Test_Send_Mentions_Success()
    {
        var mention = new[]
        {
            new MentionParameters()
            {
                MentionId = 0,
                AdditionalDataParameters = null,
                MentionText = "Teemu Tossavainen",
                UserDisplayName = "UserDisplayName",
                UserId = TestUserId,
            },
            new MentionParameters()
            {
                MentionId = 1,
                AdditionalDataParameters = null,
                MentionText = "Borissov Jefim",
                UserDisplayName = "UserDisplayName",
                UserId = "78252de7-fc63-46a5-a903-3c43760f97dd",
            },
        };

        input.MessageContent = "Hello <at id=\"0\">Teemu Tossavainen</at> <at id=\"1\">Borissov Jefim</at>";
        input.SetMentions = true;
        input.MentionParameters = mention;

        var result = await MicrosoftTeams.SendMessage(input, connection, options, default);
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
                Name = "TeamsTaskTest.txt",
            },
            new AttachmentParameters()
            {
                Id = "6740d7bf-2d0c-498b-a5f5-40b309b101cd",
                ContentType = "reference",
                ContentUrl = "https://unthink.sharepoint.com/:t:/r/sites/FrendsTeamsTaskDevelopment/Shared%20Documents/General/TeamsTaskTest_Second.txt",
                Name = "TeamsTaskTest_Second.txt",
            },
        };

        input.MessageContent = "Attachment 1: <attachment id=\"128b6438-5936-4093-97c9-44e0365222ab\"></attachment> -  Attachment 2: <attachment id=\"6740d7bf-2d0c-498b-a5f5-40b309b101cd\"></attachment>";

        input.SetAttachments = true;
        input.AttachmentParameters = attachment;

        var result = await MicrosoftTeams.SendMessage(input, connection, options, default);
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
                Name = "TeamsTaskTest.txt",
            },
            new AttachmentParameters()
            {
                Id = "6740d7bf-2d0c-498b-a5f5-40b309b101cd",
                ContentType = "reference",
                ContentUrl = "https://unthink.sharepoint.com/:t:/r/sites/FrendsTeamsTaskDevelopment/Shared%20Documents/General/TeamsTaskTest_Second.txt",
                Name = "TeamsTaskTest_Second.txt",
            },
        };

        var mentions = new[]
        {
            new MentionParameters()
            {
                MentionId = 0,
                AdditionalDataParameters = null,
                MentionText = "Teemu Tossavainen",
                UserDisplayName = "UserDisplayName",
                UserId = TestUserId,
            },
            new MentionParameters()
            {
                MentionId = 1,
                AdditionalDataParameters = null,
                MentionText = "Borissov Jefim",
                UserDisplayName = "UserDisplayName",
                UserId = "78252de7-fc63-46a5-a903-3c43760f97dd",
            },
        };

        input.MessageContent = "Hello <at id=\"0\">Teemu Tossavainen</at> and <at id=\"1\">Borissov Jefim</at>! Here's some test attachments: Attachment 1: <attachment id=\"128b6438-5936-4093-97c9-44e0365222ab\"></attachment> -  Attachment 2: <attachment id=\"6740d7bf-2d0c-498b-a5f5-40b309b101cd\"></attachment>";
        input.SetAttachments = true;
        input.AttachmentParameters = attachment;
        input.SetMentions = true;
        input.MentionParameters = mentions;

        var result = await MicrosoftTeams.SendMessage(input, connection, options, default);
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
                Name = "TeamsTaskTest.txt",
            },
        };

        input.MessageContent = "Attachment 1: <attachment id=\"128b6438-5936-4093-97c9-44e0365222ab\"></attachment>";

        input.SetAttachments = true;
        input.AttachmentParameters = attachment;
        options.ThrowOnError = false;

        var result = await MicrosoftTeams.SendMessage(input, connection, options, default);
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
                Name = "TeamsTaskTest.txt",
            },
        };

        input.MessageContent = "Attachment 1: <attachment id=\"128b6438-5936-4093-97c9-44e0365222ab\"></attachment>";

        input.SetAttachments = true;
        input.AttachmentParameters = attachment;
        options.ThrowOnError = true;

        var ex = await Assert.ThrowsExceptionAsync<HttpRequestException>(() => MicrosoftTeams.SendMessage(input, connection, options, default));
        Assert.IsTrue(ex.Message.Contains("Body does not contain marker for attachment with Id 'Missing ID'"));
    }

    private static async Task<string> CreateAccessToken()
    {
        using var client = new RestClient();
        RestRequest request = new($"https://login.microsoftonline.com/{TenantId}/oauth2/v2.0/token", Method.Post);

        request.AddParameter("client_id", ClientId);
        request.AddParameter("client_secret", ClientSecret);
        request.AddParameter("scope", "https://graph.microsoft.com/.default");
        request.AddParameter("username", Username);
        request.AddParameter("password", Password);
        request.AddParameter("grant_type", "password");

        var response = await client.ExecuteAsync(request);
        if (response.Content is not null)
        {
            var accessToken = JObject.Parse(response.Content)["access_token"]?.ToString() ?? string.Empty;
            return accessToken;
        }

        return string.Empty;
    }
}
