namespace Frends.MicrosoftTeams.SendMessage.Definitions;

/// <summary>
/// AdditionalDataParameter.
/// </summary>
public class AdditionalDataParameter
{
    /// <summary>
    /// Key.
    /// </summary>
    /// <example>foo</example>
    public string Key { get; set; }

    /// <summary>
    /// Value.
    /// </summary>
    /// <example>bar</example>
    public object Value { get; set; }
}