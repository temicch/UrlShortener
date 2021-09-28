namespace UrlShortener.Application.Interfaces.Services
{
    public interface IUrlShortenerService
    {
        public bool TryShortUrl(string urlString, out string alias, string salt = "");
        public string NormalizeUrl(string urlString);
        public bool IsValidUrl(string urlString);
    }
}