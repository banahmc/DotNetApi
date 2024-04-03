using Bogus;
using DotNetApi.Model.Domain;
using System.Collections.Concurrent;

namespace DotNetApi.Model.DataAccess
{
    internal sealed class ProductRepository : IProductRepository
    {
        private static readonly ConcurrentBag<Product> _products = new(GenerateRandomProducts());
        private static readonly object lockObject = new();

        public IEnumerable<Product> GetByFilter(int pageNumber = 1, int pageSize = 1, string? code = null, string? name = null)
        {
            // todo: thread safety for filter & pagination?

            // Filtering
            var filteredProducts = _products;
            if (!string.IsNullOrEmpty(code))
            {
                filteredProducts = FilterBy(filteredProducts, p => p.Code.Contains(code.Trim(), StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrEmpty(name))
            {
                filteredProducts = FilterBy(filteredProducts, p => p.Name.Contains(name.Trim(), StringComparison.OrdinalIgnoreCase));
            }

            // Pagination
            var paginatedProducts = filteredProducts
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return paginatedProducts;
        }

        public Product? GetByCode(string code) =>
            GetByFilter(code: code)
            .FirstOrDefault();

        public Product Add(string code, string name)
        {
            var product = new Product
            {
                Id = GetNextId(),
                Code = code.Trim(),
                Name = name.Trim()
            };

            _products.Add(product);

            return product;
        }

        private static int GetNextId()
        {
            Monitor.Enter(lockObject);

            try
            {
                return _products.Count + 1;
            }
            finally
            {
                Monitor.Exit(lockObject);
            }
        }

        private static ConcurrentBag<Product> FilterBy(
            ConcurrentBag<Product> source,
            Func<Product, bool> predicate) =>
                new(source.Where(predicate));

        private static List<Product> GenerateRandomProducts() =>
            new Faker<Product>()
                .RuleFor(p => p.Id, f => ++f.IndexVariable)
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Code, f => f.Commerce.Ean13())
                .Generate(100);
    }
}
