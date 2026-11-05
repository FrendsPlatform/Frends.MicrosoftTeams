namespace Frends.MicrosoftTeams.SendMessage.Definitions;

/// <summary>
/// Options parameters.
/// </summary>
public class Options
{
    /// <summary>
    /// Gets or sets a value indicating whether to throw an error on failure.
    /// </summary>
    /// <example>true</example>
    public bool ThrowErrorOnFailure { get; set; } = true;

    /// <summary>
    /// Gets or sets the error message to use on failure.
    /// </summary>
    /// <example>Microsoft Teams message delivery failed</example>
    public string ErrorMessageOnFailure { get; set; } = string.Empty;
}
