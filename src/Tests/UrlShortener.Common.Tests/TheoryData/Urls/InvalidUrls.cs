using Xunit;

namespace UrlShortener.Common.Tests.TheoryData.Urls;

public class InvalidUrls : TheoryData<string>
{
    public InvalidUrls()
    {
        Add("https://google\\com");
        Add(
            "www.google.co.uk/maps/place/Louth,+UK/@53.370272,-0.004034,14z/data=!4m5!3m4!1s0x47d62a0c21a749c5:0x720a7c7d4fa901f0!8m2!3d53.365962!4d-0.007711?hl=en");
        Add(
            "https:www.google.co.uk/maps/place/Louth,+UK/@53.370272,-0.004034,14z/data=!4m5!3m4!1s0x47d62a0c21a749c5:0x720a7c7d4fa901f0!8m2!3d53.365962!4d-0.007711?hl=en");
        Add(
            "https://www.google.co.uk\\maps/place/Louth,+UK/@53.370272,-0.004034,14z/data=!4m5!3m4!1s0x47d62a0c21a749c5:0x720a7c7d4fa901f0!8m2!3d53.365962!4d-0.007711?hl=en");
        Add(" ");
        Add(string.Empty);
        Add(null);
    }
}