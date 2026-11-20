using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BlazorShop.Api.Validations;

public class ValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            context.Result = new BadRequestObjectResult(new ValidationProblemDetails(
                context.ModelState.Where(x => x.Value?.Errors.Count > 0)
                    .ToDictionary(x => x.Key, _ => new[] { "Valor inválido." })));
            return;
        }

        var errors = new Dictionary<string, string[]>();
        foreach (var parameter in context.ActionDescriptor.Parameters)
        {
            var validatorType = typeof(IValidator<>).MakeGenericType(parameter.ParameterType);
            if (context.HttpContext.RequestServices.GetService(validatorType) is not IValidator validator)
                continue;

            if (!context.ActionArguments.TryGetValue(parameter.Name, out var argument) || argument is null)
            {
                errors[parameter.Name] = ["Dados obrigatórios."];
                continue;
            }

            var result = await validator.ValidateAsync(
                new ValidationContext<object>(argument), context.HttpContext.RequestAborted);
            foreach (var group in result.Errors.GroupBy(x => x.PropertyName))
                errors[group.Key] = group.Select(x => x.ErrorMessage).Distinct().ToArray();
        }

        if (errors.Count > 0)
        {
            context.Result = new BadRequestObjectResult(new ValidationProblemDetails(errors));
            return;
        }

        await next();
    }
}

