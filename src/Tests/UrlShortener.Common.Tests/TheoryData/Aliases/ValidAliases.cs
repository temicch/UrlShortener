using Xunit;

namespace UrlShortener.Common.Tests.TheoryData.Aliases;

public class ValidAliases : TheoryData<string>
{
    public ValidAliases()
    {
        Add("ddd");
        Add("google");
        Add("goo_gle");
        Add("____");
        Add("2goo3gl4e");
        Add("dasddddddddddddddd_dsadsad");
        Add("123456789112345678921234567890");
        Add("relax1234560000000000000000000");
        Add("000000000000000000000000000000");
        Add("00000000000_000000000000000000");
        Add("  doo                             ");
    }
}