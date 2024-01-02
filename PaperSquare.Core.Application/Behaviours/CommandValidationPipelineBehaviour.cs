using FluentValidation;
using MediatR;
using PaperSquare.Core.Application.Exceptions;

namespace PaperSquare.API.Middlewares.CommandValidation;

public sealed class CommandValidationPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public CommandValidationPipelineBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if(!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var errorsDictionary = _validators.Select(v => v.Validate(context))
                                          .SelectMany(v => v.Errors)
                                          .Where(v => v != null)
                                          .GroupBy(v => v.PropertyName, v => v.ErrorMessage, (propertyName, errorMessages) => new KeyValuePair<string, IEnumerable<string>>(propertyName, errorMessages.Distinct().ToList()))
                                          .ToDictionary(v => v.Key, v => v.Value);

        if(errorsDictionary.Any())
        {
            throw new CommandValidationException("Validation exception", errorsDictionary.AsReadOnly());
        }

        return await next();
    }
}
