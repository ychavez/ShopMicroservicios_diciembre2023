using FluentValidation;
using MediatR;

namespace Ordering.Application.Behaviors
{
    public class ValidationBehavior<TRquest, TResponse>(IEnumerable<IValidator<TRquest>> validators)
        : IPipelineBehavior<TRquest, TResponse> where TRquest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRquest request, RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (validators.Any())
            {
                //nos traemos el contexto de validacion
                var context = new ValidationContext<TRquest>(request);

                // ejecutamos las validaciones
                var validationResults = await
                    Task.WhenAll(validators.Select(v => v.ValidateAsync(context)));

                // buscamos validaciones fallidas
                var failures = validationResults.SelectMany(r => r.Errors)
                    .Where(f => f is not null).ToList();

                // regresamos el error
                if (failures.Any())
                    throw new ValidationException(failures);
            }

            return await next();
        }
    }
}
