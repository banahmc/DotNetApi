using DotNetApi.Model.DataAccess;
using DotNetApi.Model.Domain;
using MediatR;

namespace DotNetApi.Model.Cqrs.Products
{
    public sealed record GetProductByCodeQuery(string Code)
        : IRequest<Product?>;

    public sealed class GetProductByCodeQueryHandler
        : IRequestHandler<GetProductByCodeQuery, Product?>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByCodeQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<Product?> Handle(
            GetProductByCodeQuery request,
            CancellationToken cancellationToken)
        {
            var product = _productRepository.GetByCode(request.Code);

            return Task.FromResult(product);
        }
    }
}
