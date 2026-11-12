namespace Frends.MicrosoftTeams.SendMessage.Definitions;

/// <summary>
/// Task's result.
/// </summary>
public class Result
{
    /// <summary>
    /// Value indicating whether the operation completed without errors.
    /// </summary>
    /// <example>true</example>
    public bool Success { get; set; }

    /// <summary>
    /// Error details.
    /// </summary>
    /// <example>object { Message = "Microsoft Graph request failed", AdditionalInfo = HttpRequestException }</example>
    public Error Error { get; set; }
}
