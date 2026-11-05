namespace Frends.MicrosoftTeams.SendMessage.Definitions;

/// <summary>
/// Task's result.
/// </summary>
public class Result
{
    /// <summary>
    /// Gets or sets a value indicating whether the operation completed without errors.
    /// </summary>
    /// <example>true</example>
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets the error message.
    /// </summary>
    /// <example>Microsoft Graph request failed</example>
    public string ErrorMessage { get; set; }

    /// <summary>
    /// Gets or sets the error details.
    /// </summary>
    /// <example>object { Message = "Microsoft Graph request failed", AdditionalInfo = HttpRequestException }</example>
    public Error Error { get; set; }
}
