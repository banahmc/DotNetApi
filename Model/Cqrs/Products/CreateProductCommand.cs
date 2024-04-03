using DotNetApi.Model.DataAccess;
using DotNetApi.Model.Domain.Exceptions;
using MediatR;

namespace DotNetApi.Model.Cqrs.Products
{
    public sealed record CreateProductCommand(string Code, string Name)
        : IRequest;

    public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
    {
        private readonly IProductRepository _repository;

        public CreateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(
            CreateProductCommand request,
            CancellationToken cancellationToken)
        {
            var existingProduct = _repository.GetByCode(request.Code);
            if (existingProduct is not null)
            {
                throw new ProductAlreadyExistsException(request.Code);
            }

            _repository.Add(request.Code, request.Name);

            return Task.CompletedTask;
        }
    }
}
