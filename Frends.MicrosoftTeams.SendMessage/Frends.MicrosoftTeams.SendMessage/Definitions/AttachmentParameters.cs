namespace Frends.MicrosoftTeams.SendMessage.Definitions;

/// <summary>
/// Attachment parameters.
/// </summary>
public class AttachmentParameters
{
    /// <summary>
    /// The attachment ID.
    /// </summary>
    /// <example>74d20c7f34aa4a7fb74e2b30004247c5</example>
    public string Id { get; set; }

    /// <summary>
    /// The media type of the content attachment.
    /// It can have the following values: reference, which is a link to another file; content types supported by the Bot Framework Attachment object; application/vnd.microsoft.card.codesnippet, which is a code snippet; and application/vnd.microsoft.card.announcement, which is an announcement header.
    /// </summary>
    /// <example>reference, application/vnd.microsoft.card.thumbnail</example>
    public string ContentType { get; set; }

    /// <summary>
    /// The URL for the content of the attachment.
    /// Supported protocols: http, https, file, and data.
    /// </summary>
    /// <example>https://unthink.sharepoint.com/r/sites/TeamsChannel/Shared%20Documents/General/Test.txt</example>
    public string ContentUrl { get; set; }

    /// <summary>
    /// The name of the attachment.
    /// </summary>
    /// <example>Test.txt</example>
    public string Name { get; set; }
}
