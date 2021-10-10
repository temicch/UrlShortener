using Xunit;

namespace UrlShortener.Common.Tests.TheoryData.Aliases
{
    public class InvalidAliases : TheoryData<string>
    {
        public InvalidAliases()
        {
            Add("d");
            Add("dd");
            Add("00");
            Add("-_-");
            Add("google-");
            Add("@google@");
            Add("@#$$@$!@#");
            Add("00000000000)00000000000000");
            Add("dasddddddddddddddd-dsadsad");
            Add("000000000000000000000000000000000000000000000000");
        }
    }
}