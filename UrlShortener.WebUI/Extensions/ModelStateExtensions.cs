using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace UrlShortener.WebUI.Extensions
{
    public static class ModelStateExtensions
    {
        public static void AddModelErrors(this ModelStateDictionary dictionary,
            ValidationException exception)
        {
            foreach (var error in exception.Errors) dictionary.AddModelError(error.PropertyName, error.ErrorMessage);
        }
    }
}