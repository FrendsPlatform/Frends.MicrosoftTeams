namespace Frends.MicrosoftTeams.SendMessage.Definitions;

/// <summary>
/// Additional data parameter.
/// </summary>
public class AdditionalDataParameter
{
    /// <summary>
    /// The key.
    /// </summary>
    /// <example>foo</example>
    public string Key { get; set; }

    /// <summary>
    /// The value.
    /// </summary>
    /// <example>bar</example>
    public object Value { get; set; }
}
