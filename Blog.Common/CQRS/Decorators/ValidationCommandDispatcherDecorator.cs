using Blog.Common.Domain.Errors;
using Blog.Common.Domain.Results;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Common.CQRS.Decorators
{
    internal class ValidationCommandDispatcherDecorator : ICommandDispatcher
    {
        private readonly ICommandDispatcher _commandDispatcher;

        private readonly IServiceProvider _serviceProvider;

        public ValidationCommandDispatcherDecorator(ICommandDispatcher commandDispatcher, IServiceProvider serviceProvider)
        {
            _commandDispatcher = commandDispatcher;
            _serviceProvider = serviceProvider;
        }

        public async Task<TCommandResult> Dispatch<TCommand, TCommandResult>(TCommand command, CancellationToken token)
            where TCommand : ICommand
            where TCommandResult : Result
        {
            var validators = _serviceProvider.GetService<IEnumerable<IValidator<TCommand>>>();

            if (validators is null || !validators.Any())
            {
                return await _commandDispatcher.Dispatch<TCommand, TCommandResult>(command, token);
            }

            var errors = validators
                .Select(validator => validator.Validate(command))
                .SelectMany(validationResult => validationResult.Errors)
                .Where(validationFailure => validationFailure is not null)
                .Select(failure => new Error(
                    failure.PropertyName,
                    failure.ErrorMessage))
                .Distinct();

            if (errors.Any())
            {
                return CreateValidationResult<TCommandResult>(errors);
            }

            return await _commandDispatcher.Dispatch<TCommand, TCommandResult>(command, token);
        }

        private static TResult CreateValidationResult<TResult>(IEnumerable<Error> errors)
            where TResult : Result
        {
            if (typeof(TResult) == typeof(Result))
            {
                return (ValidationResult.WithErrors(errors) as TResult)!;
            }

            if (typeof(TResult) == typeof(ValidationResult<>))
            {
                object validationResult = typeof(ValidationResult<>)
                    .GetGenericTypeDefinition()
                    .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
                    .GetMethod(nameof(ValidationResult.WithErrors))!
                    .Invoke(null, new object?[] { errors })!;

                return (TResult)validationResult;
            }

            if (typeof(TResult) == typeof(IdentityResult))
            {
                return (IdentityResult.WithErrors(errors) as TResult)!;
            }

            object identityResult = typeof(IdentityResult<>)
                    .GetGenericTypeDefinition()
                    .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
                    .GetMethod(nameof(IdentityResult.WithErrors))!
                    .Invoke(null, new object?[] { errors })!;

            return (TResult)identityResult;
        }
    }
}
