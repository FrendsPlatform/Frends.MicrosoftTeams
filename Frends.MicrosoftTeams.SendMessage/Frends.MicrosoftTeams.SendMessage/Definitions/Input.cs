using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.MicrosoftTeams.SendMessage.Definitions;

/// <summary>
/// Input parameters.
/// </summary>
public class Input
{
    /// <summary>
    /// Gets or sets the subject of the chat message in plain text.
    /// It can be empty.
    /// </summary>
    /// <example>This is subject</example>
    public string Subject { get; set; }

    /// <summary>
    /// Gets or sets the type of the message content.
    /// </summary>
    /// <example>BodyTypes.Html</example>
    [DefaultValue(BodyTypes.Html)]
    public BodyTypes BodyType { get; set; }

    /// <summary>
    /// Gets or sets the message content.
    /// </summary>
    /// <example>This is a plain message, This is html message to &lt;at id=\"0\"&gt;Jane Smith&gt;/at&lt;</example>
    public string MessageContent { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to include entities mentioned in the chat message.
    /// </summary>
    /// <example>true</example>
    [DefaultValue(false)]
    public bool SetMentions { get; set; }

    /// <summary>
    /// Gets or sets the entities mentioned in the chat message.
    /// Supported entities are: user, bot, team, and channel.
    /// </summary>
    /// <example>
    /// {
    ///     [ 0, MentionText, UserDisplayName, ef1c916a-3135-4417-ba27-8eb7bd084193, [ foo, bar ],
    ///     [ 1, MentionText2, UserDisplayName2, ef1c916a-3135-4417-ba27-8eb7bd084194, [ bar, foo ]
    /// }
    /// </example>
    [UIHint(nameof(SetMentions), "", true)]
    public MentionParameters[] MentionParameters { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to add references to attached files, tabs, or meetings.
    /// The file must already be in SharePoint. See https://learn.microsoft.com/en-us/graph/api/chatmessage-post?view=graph-rest-1.0&amp;tabs=csharp.
    /// </summary>
    /// <example>true</example>
    [DefaultValue(false)]
    public bool SetAttachments { get; set; }

    /// <summary>
    /// Gets or sets references to attached files, tabs, or meetings.
    /// </summary>
    /// <example>
    /// {
    ///     [ 1, reference, https://m365x987948.sharepoint.com/sites/test/Shared%20Documents/General/foo.docx, foo ],
    ///     [ reference, https://m365x987948.sharepoint.com/sites/test/Shared%20Documents/General/bar.docx, bar ],
    /// }
    /// </example>
    public AttachmentParameters[] AttachmentParameters { get; set; }
}
