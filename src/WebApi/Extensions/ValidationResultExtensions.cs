using FluentValidation.Results;

namespace WebApi.Extensions;

public static class ValidationResultExtensions
{
    public static Dictionary<string, string[]> GetErrors(this ValidationResult validationResult) =>
        validationResult
            .Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(g => g.Key, g => g.Select(y => y.ErrorMessage).ToArray());
}
