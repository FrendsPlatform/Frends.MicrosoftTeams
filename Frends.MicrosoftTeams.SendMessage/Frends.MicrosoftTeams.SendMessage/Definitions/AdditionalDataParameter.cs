namespace Frends.MicrosoftTeams.SendMessage.Definitions;

/// <summary>
/// Additional data parameter.
/// </summary>
public class AdditionalDataParameter
{
    /// <summary>
    /// Gets or sets the key.
    /// </summary>
    /// <example>foo</example>
    public string Key { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    /// <example>bar</example>
    public object Value { get; set; }
}
