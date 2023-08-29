namespace Frends.MicrosoftTeams.SendMessage.Definitions;

/// <summary>
/// AttachmentParameters
/// </summary>
public class AttachmentParameters
{
    /// <summary>
    /// Attachment ID.
    /// </summary>
    /// <example>74d20c7f34aa4a7fb74e2b30004247c5</example>
    public string Id { get; set; }

    /// <summary>
    /// The media type of the content attachment. 
    /// It can have the following values: reference: Attachment is a link to another file. Populate the contentURL with the link to the object.Any contentTypes supported by the Bot Framework&apos;s Attachment objectapplication/vnd.microsoft.card.codesnippet: A code snippet. application/vnd.microsoft.card.announcement: An announcement header.
    /// </summary>
    /// <example>reference, application/vnd.microsoft.card.thumbnail</example>
    public string ContentType { get; set; }

    /// <summary>
    /// URL for the content of the attachment. 
    /// Supported protocols: http, https, file and data.
    /// </summary>
    /// <example>https://unthink.sharepoint.com/r/sites/TeamsChannel/Shared%20Documents/General/Test.txt</example>
    public string ContentUrl { get; set; }

    /// <summary>
    /// Name of the attachment.
    /// </summary>
    /// <example>Test.txt</example>
    public string Name { get; set; }
}