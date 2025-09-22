using System.ComponentModel.DataAnnotations;

public class ValidationFilter<T> : IEndpointFilter where T : class
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var argument = context.Arguments.FirstOrDefault(a => a?.GetType() == typeof(T));
        if (argument is null)
        {
            return await next(context);
        }

        var validationContext = new ValidationContext(argument);
        var validationResults = new List<ValidationResult>();

        if (!Validator.TryValidateObject(argument, validationContext, validationResults, true))
        {
            var errors = validationResults
                .GroupBy(r => r.MemberNames.FirstOrDefault() ?? string.Empty)
                .ToDictionary(
                group => group.Key,
                group => group.Select(r => r.ErrorMessage)
                              .Where(msg => msg is not null)
                              .ToArray() as string[]
                );

            return Results.ValidationProblem(errors);
        }

        return await next(context);
    }
}
