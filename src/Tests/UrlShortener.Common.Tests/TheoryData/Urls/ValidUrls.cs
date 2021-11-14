using Xunit;

namespace UrlShortener.Common.Tests.TheoryData.Urls;

public class ValidUrls : TheoryData<string>
{
    public ValidUrls()
    {
        Add(
            "https://www.google.co.uk/maps/place/Louth,+UK/@53.370272,-0.004034,14z/data=!4m5!3m4!1s0x47d62a0c21a749c5:0x720a7c7d4fa901f0!8m2!3d53.365962!4d-0.007711?hl=en");
        Add(
            "https://google.co.uk/maps/place/Louth,+UK/@53.370272,-0.004034,14z/data=!4m5!3m4!1s0x47d62a0c21a749c5:0x720a7c7d4fa901f0!8m2!3d53.365962!4d-0.007711?hl=en");
        Add(
            "https://www.google.com/search?q=c%23+normalize+url+%252F&newwindow=1&ei=9J9MYY2tJo6prgStwK6IDQ&oq=c%23+normalize+url+%252F&gs_lcp=Cgdnd3Mtd2l6EAM6CAgAEAoQARAqOgYIABAHEB46BQgAEIAESgQIQRgAUMWNBVjapwVgiKkFaAFwAngAgAHxAYgBhxCSAQYwLjE2LjGYAQCgAQHAAQE&sclient=gws-wiz&ved=0ahUKEwiN9-mdt5XzAhWOlIsKHS2gC9EQ4dUDCA4&uact=5");
        Add(
            "https://google.com/search?q=c%23+normalize+url+%252F&newwindow=1&ei=9J9MYY2tJo6prgStwK6IDQ&oq=c%23+normalize+url+%252F&gs_lcp=Cgdnd3Mtd2l6EAM6CAgAEAoQARAqOgYIABAHEB46BQgAEIAESgQIQRgAUMWNBVjapwVgiKkFaAFwAngAgAHxAYgBhxCSAQYwLjE2LjGYAQCgAQHAAQE&sclient=gws-wiz&ved=0ahUKEwiN9-mdt5XzAhWOlIsKHS2gC9EQ4dUDCA4&uact=5");
        Add("https://google/com");
        Add("https://www.google/com");
        Add("ftp://123.54.66.77/important_file");
    }
}
