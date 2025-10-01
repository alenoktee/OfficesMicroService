using FluentValidation;

using System.ComponentModel.DataAnnotations;

public class ValidationFilter<T> : IEndpointFilter where T : class
{
    private readonly string[] _ruleSets;

    public ValidationFilter(params string[] ruleSets)
    {
        _ruleSets = ruleSets;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();

        if (validator is null)
        {
            return await next(context);
        }

        var validatable = context.Arguments.OfType<T>().FirstOrDefault();
        if (validatable is null)
        {
            return await next(context);
        }

        var validationResult = await validator.ValidateAsync(validatable, options =>
        {
            if (_ruleSets is not null && _ruleSets.Length > 0)
            {
                options.IncludeRuleSets(_ruleSets);
            }
        });

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        return await next(context);
    }
}
