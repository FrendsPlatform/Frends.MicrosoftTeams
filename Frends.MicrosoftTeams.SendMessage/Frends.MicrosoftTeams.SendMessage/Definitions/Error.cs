using System;

namespace Frends.MicrosoftTeams.SendMessage.Definitions;

/// <summary>
/// Error that occurred during the task.
/// </summary>
public class Error
{
    /// <summary>
    /// The summary of the error.
    /// </summary>
    /// <example>Microsoft Graph request failed</example>
    public string Message { get; set; }

    /// <summary>
    /// Additional information about the error.
    /// </summary>
    /// <example>HttpRequestException: Request failed</example>
    public Exception AdditionalInfo { get; set; }
}
