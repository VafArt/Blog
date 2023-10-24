using Blog.Common.Domain.Errors;
using Blog.Common.Domain.Results;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Common.CQRS.Decorators
{
    internal class ValidationQueryDispatcherDecorator : IQueryDispatcher
    {
        private readonly IQueryDispatcher _queryDispatcher;

        private readonly IServiceProvider _serviceProvider;

        public ValidationQueryDispatcherDecorator(IQueryDispatcher queryDispatcher, IServiceProvider serviceProvider)
        {
            _queryDispatcher = queryDispatcher;
            _serviceProvider = serviceProvider;
        }

        public async Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery query, CancellationToken token)
            where TQuery : IQuery
            where TQueryResult : Result
        {
            var validators = _serviceProvider.GetRequiredService<IEnumerable<IValidator<TQuery>>>();

            if (!validators.Any())
            {
                return await _queryDispatcher.Dispatch<TQuery, TQueryResult>(query, token);
            }

            IEnumerable<Error> errors = validators
                .Select(validator => validator.Validate(query))
                .SelectMany(validationResult => validationResult.Errors)
                .Where(validationFailure => validationFailure is not null)
                .Select(failure => new Error(
                    failure.PropertyName,
                    failure.ErrorMessage))
                .Distinct();

            if (errors.Any())
            {
                return CreateValidationResult<TQueryResult>(errors);
            }

            return await _queryDispatcher.Dispatch<TQuery, TQueryResult>(query, token);
        }

        private static TResult CreateValidationResult<TResult>(IEnumerable<Error> errors)
            where TResult : Result
        {
            if(typeof(TResult) == typeof(Result))
            {
                return (ValidationResult.WithErrors(errors) as TResult)!;
            }

            object validationResult = typeof(ValidationResult<>)
                .GetGenericTypeDefinition()
                .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
                .GetMethod(nameof(ValidationResult.WithErrors))!
                .Invoke(null, new object?[] { errors })!;

            return (TResult)validationResult;
        }
    }
}
