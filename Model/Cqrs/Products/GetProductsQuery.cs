using DotNetApi.Model.DataAccess;
using DotNetApi.Model.Domain;
using MediatR;

namespace DotNetApi.Model.Cqrs.Products
{
    public sealed record GetProductsQuery(string? Code, string? Name, int PageNumber, int PageSize)
        : IRequest<IEnumerable<Product>>;

    public sealed class GetProductsQueryHandler
        : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<IEnumerable<Product>> Handle(
            GetProductsQuery request,
            CancellationToken cancellationToken)
        {
            var products = _productRepository.GetByFilter(
                request.PageNumber,
                request.PageSize,
                request.Code,
                request.Name);

            return Task.FromResult(products);
        }
    }
}
