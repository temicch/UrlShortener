namespace UrlShortener.Application.Interfaces.Services
{
    public interface IUrlShortenerService
    {
        public bool TryShortUrl(string encodedUrl, out string alias, string salt = "");
        public string NormalizeUrl(string encodedUrl);
        public bool IsValidUrl(string encodedUrl);
    }
}