using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.MicrosoftTeams.SendMessage.Definitions;

/// <summary>
/// Connection parameters.
/// </summary>
public class Connection
{
 /// <summary>
    /// Gets or sets the Microsoft Graph access token.
    /// </summary>
    /// <example>sample-microsoft-graph-access-token</example>
    [DisplayFormat(DataFormatString = "Text")]
    [PasswordPropertyText]
    public string Token { get; set; }

 /// <summary>
    /// Gets or sets the channel ID.
    /// </summary>
    /// <example>20%3au0yPT4vDedASF0InQKu233CiYHqdAtFnXA_45UB6Zgo1%40thread.tacv2</example>
    public string ChannelId { get; set; }

 /// <summary>
    /// Gets or sets the team ID.
    /// </summary>
    /// <example>19%3au0yPT4vDedPFASD3QKu233CiZwqdAtFnXA_25UB6Zgo1%40thread.tacv2</example>
    public string TeamId { get; set; }
}
