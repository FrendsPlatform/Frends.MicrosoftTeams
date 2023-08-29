using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.MicrosoftTeams.SendMessage.Definitions;

/// <summary>
/// Input parameters.
/// </summary>
public class Input
{
    /// <summary>
    /// The subject of the chat message, in plaintext.
    /// Can be empty.
    /// </summary>
    /// <example>This is subject</example>
    public string Subject { get; set; }

    /// <summary>
    /// The type of the content.
    /// </summary>
    /// <example>BodyTypes.html</example>
    [DefaultValue(BodyTypes.html)]
    public BodyTypes BodyType { get; set; }

    /// <summary>
    /// Message content.
    /// </summary>
    /// <example>This is a plain message, This is html message to &lt;at id=\"0\"&gt;Jane Smith&gt;/at&lt;</example>
    public string MessageContent { get; set; }

    /// <summary>
    /// Set entities mentioned in the chat message.
    /// </summary>
    /// <example>true</example>
    [DefaultValue(false)]
    public bool SetMentions { get; set; }

    /// <summary>
    /// List of entities mentioned in the chat message. 
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
    /// Set references to attached objects like files, tabs, meetings.
    /// Note: The file must already be in SharePoint. See https://learn.microsoft.com/en-us/graph/api/chatmessage-post?view=graph-rest-1.0&amp;tabs=csharp
    /// </summary>
    /// <example>true</example>
    [DefaultValue(false)]
    public bool SetAttachments { get; set; }

    /// <summary>
    /// References to attached objects like files, tabs, meetings.
    /// </summary>
    /// <example>
    /// {
    ///     [ 1, reference, https://m365x987948.sharepoint.com/sites/test/Shared%20Documents/General/foo.docx, foo ],
    ///     [ reference, https://m365x987948.sharepoint.com/sites/test/Shared%20Documents/General/bar.docx, bar ],
    /// }
    /// </example>
    public AttachmentParameters[] AttachmentParameters { get; set; }
}