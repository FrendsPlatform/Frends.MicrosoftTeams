namespace Frends.MicrosoftTeams.SendMessage.Definitions;

/// <summary>
/// Options parameters.
/// </summary>
public class Options
{
    /// <summary>
    /// Indicates whether to throw an error on failure.
    /// </summary>
    /// <example>true</example>
    public bool ThrowErrorOnFailure { get; set; } = true;

    /// <summary>
    /// The error message to use on failure.
    /// </summary>
    /// <example>Microsoft Teams message delivery failed</example>
    public string ErrorMessageOnFailure { get; set; } = string.Empty;
}
