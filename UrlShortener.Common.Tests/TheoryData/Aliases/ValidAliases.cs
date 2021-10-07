using Xunit;

namespace UrlShortener.Common.Tests.TheoryData.Aliases
{
    public class ValidAliases : TheoryData<string>
    {
        public ValidAliases()
        {
            // Those aliases means that alias will be generated automatically
            // (they are formally valid)
            Add(null);
            Add(string.Empty);
            Add("            ");
            // --------------------------------------------------------------
            Add("ddd");
            Add("google");
            Add("2goo3gl4e");
            Add("123456789112345678921234567890");
            Add("relax1234560000000000000000000");
            Add("000000000000000000000000000000");
            Add("  doo                             ");
        }
    }
}