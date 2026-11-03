using Frends.MicrosoftTeams.SendMessage.Definitions;
using Frends.MicrosoftTeams.SendMessage.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Frends.MicrosoftTeams.SendMessage.Tests;

[TestClass]
public class HarmonizationTests
{
    [TestMethod]
    public void LegacyThrowOnErrorUpdatesStandardOption()
    {
        var options = new Options
        {
            ThrowOnError = false,
        };

        Assert.IsFalse(options.ThrowErrorOnFailure);
    }

    [TestMethod]
    public void HandleReturnsFailureDetailsWhenThrowingIsDisabled()
    {
        var exception = new InvalidOperationException("Request failed");
        var options = new Options
        {
            ThrowErrorOnFailure = false,
            ErrorMessageOnFailure = "Microsoft Teams message delivery failed",
        };

        var result = exception.Handle(options);

        Assert.IsFalse(result.Success);
        Assert.AreEqual("Microsoft Teams message delivery failed: Request failed", result.ErrorMessage);
        Assert.AreEqual(result.ErrorMessage, result.Error.Message);
        Assert.AreSame(exception, result.Error.AdditionalInfo);
    }

    [TestMethod]
    public void HandleThrowsOriginalExceptionWhenThrowingIsEnabled()
    {
        var exception = new InvalidOperationException("Request failed");
        var options = new Options
        {
            ThrowErrorOnFailure = true,
        };

        var thrownException = Assert.ThrowsException<InvalidOperationException>(() => exception.Handle(options));

        Assert.AreSame(exception, thrownException);
    }
}
