﻿using Frends.MicrosoftTeams.SendMessage.Definitions;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Graph.Models.ODataErrors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Frends.MicrosoftTeams.SendMessage;

/// <summary>
/// MicrosoftTeams Task.
/// </summary>
public class MicrosoftTeams
{
    /// <summary>
    /// Send message to Microsoft Teams.
    /// [Documentation](https://tasks.frends.com/tasks/frends-tasks/Frends.MicrosoftTeams.SendMessage)
    /// </summary>
    /// <param name="connection">Connection parameters.</param>
    /// <param name="input">Input parameters</param>
    /// <param name="options">Optional parameters.</param>
    /// <param name="cancellationToken">Token generated by Frends to stop this Task.</param>
    /// <returns>Object { bool Success, string ErrorMessage }</returns>
    public static async Task<Result> SendMessage([PropertyTab] Connection connection, [PropertyTab] Input input, [PropertyTab] Options options, CancellationToken cancellationToken)
    {
        try
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", connection.Token);
            var graphClient = new GraphServiceClient(httpClient, baseUrl: $"https://graph.microsoft.com/v1.0");
            var requestBody = GetMessageBody(input);
            var result = await graphClient.Teams[connection.TeamId].Channels[connection.ChannelId].Messages.PostAsync(requestBody, cancellationToken: cancellationToken);

            return new Result(true, null);
        }
        catch (HttpRequestException httpException)
        {
            if (options.ThrowOnError)
                throw new HttpRequestException($"HTTP Request Exception: {httpException.Message}");

            return new Result(false, $"HTTP Request Exception: {httpException.Message}");
        }
        catch (ServiceException serviceException)
        {
            if (options.ThrowOnError)
                throw new HttpRequestException($"Microsoft Graph Service Exception: {serviceException.Message}");

            return new Result(false, $"Microsoft Graph Service Exception: {serviceException.Message}");
        }
        catch (ODataError oDataError)
        {
            if (options.ThrowOnError)
                throw new HttpRequestException($"ODataError Exception: {oDataError.Message}, {oDataError.Error.Code}, {oDataError.Error.Message}");

            return new Result(false, $"ODataError Exception: {oDataError.Message}, {oDataError.Error.Code}, {oDataError.Error.Message}");
        }
        catch (Exception ex)
        {
            if (options.ThrowOnError)
                throw new HttpRequestException($"Exception: {ex.Message}");

            return new Result(false, $"Exception: {ex.Message}");
        }
    }

    private static ChatMessage GetMessageBody(Input input)
    {
        var mentionList = input.SetMentions && input.MentionParameters != null ? GetMentionBody(input) : null;
        var attachmentList = input.SetAttachments && input.AttachmentParameters != null ? GetAttachmentBody(input) : null;

        var chatMessage = new ChatMessage
        {
            Body = new ItemBody
            {
                ContentType = input.BodyType == BodyTypes.text ? BodyType.Text : BodyType.Html,
                Content = input.MessageContent,
            },
            Subject = input.Subject,
        };

        if (mentionList != null && mentionList.Any())
            chatMessage.Mentions = mentionList;

        if (attachmentList != null && attachmentList.Any())
            chatMessage.Attachments = attachmentList;

        return chatMessage;
    }


    private static List<ChatMessageMention> GetMentionBody(Input input)
    {
        var mentionList = new List<ChatMessageMention>();

        foreach (var mentionparam in input.MentionParameters)
        {
            var additionalData = new Dictionary<string, object>();
            if (mentionparam.AdditionalDataParameters != null)
                foreach (var param in mentionparam.AdditionalDataParameters)
                    additionalData.Add(param.Key, param.Value);

            var mention = new ChatMessageMention
            {
                Id = mentionparam.MentionId,
                MentionText = mentionparam.MentionText,
                Mentioned = new ChatMessageMentionedIdentitySet
                {
                    User = new Identity
                    {
                        Id = mentionparam.UserId,
                        DisplayName = mentionparam.UserDisplayName,
                        AdditionalData = additionalData,
                    },
                },
            };

            mentionList.Add(mention);
        }

        return mentionList;
    }

    private static List<ChatMessageAttachment> GetAttachmentBody(Input input)
    {
        var attachmentList = new List<ChatMessageAttachment>();

        foreach (var attachmentparam in input.AttachmentParameters)
        {
            var attachment = new ChatMessageAttachment
            {
                Id = string.IsNullOrWhiteSpace(attachmentparam.Id) ? null : attachmentparam.Id,
                Name = attachmentparam.Name,
                ContentType = attachmentparam.ContentType,
                ContentUrl = attachmentparam.ContentUrl,
            };

            attachmentList.Add(attachment);
        }

        return attachmentList;
    }
}