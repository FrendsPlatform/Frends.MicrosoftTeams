using System.ComponentModel;

namespace Frends.MicrosoftTeams.SendMessage.Definitions;

/// <summary>
/// MentionParameters
/// </summary>
public class MentionParameters
{
    /// <summary>
    /// Index of an entity being mentioned in the specified chatMessage. Matches the {index} value in the corresponding &lt;at id=&apos;{index}&apos;&gt; tag in the message body.
    /// </summary>
    /// <example>0</example>
    [DefaultValue(0)]
    public int MentionId { get; set; }

    /// <summary>
    /// String used to represent the mention. 
    /// For example, a user&apos;s display name, a team name.
    /// </summary>
    /// <example>Text</example>
    public string MentionText { get; set; }

    /// <summary>
    /// The display name of the identity. 
    /// Note that this might not always be available or up to date. 
    /// For example, if a user changes their display name, the API might show the new value in a future response, but the items associated with the user won&apos;t show up as having changed when using delta.
    /// </summary>
    /// <example>User</example>
    public string UserDisplayName { get; set; }

    /// <summary>
    /// Unique identifier for the identity.
    /// </summary>
    /// <example>ef1c916a-3135-4417-ba27-8eb7bd084193</example>
    public string UserId { get; set; }

    /// <summary>
    /// Stores additional data not described in the OpenAPI description found when deserializing. 
    /// Can be used for serialization as well.
    /// </summary>
    /// <example>[ userIdentityType, "aadUser" ]</example>
    public AdditionalDataParameter[] AdditionalDataParameters { get; set; }
}