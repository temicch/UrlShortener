using System;
using System.Collections.Generic;
using System.Linq;
using UrlShortener.Common.Tests.TheoryData.Aliases;
using UrlShortener.Common.Tests.TheoryData.Urls;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Common.Tests.Factories;

public static class EntitiesFactory
{
    public static IEnumerable<ShortLink> GetValidShortLinks()
    {
        var aliases = new ValidAliases().Select(x => x[0] as string);
        var urls = new ValidUrls().Select(x => x[0] as string);

        var count = aliases.Count() > urls.Count() ? urls.Count() : aliases.Count();

        if (count < 1)
            throw new Exception("Collection empty");

        for (var i = 0; i < count; i++) yield return new ShortLink { Link = urls.ElementAt(i), Alias = aliases.ElementAt(i) };
    }
}
