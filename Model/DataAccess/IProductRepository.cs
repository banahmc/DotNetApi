using DotNetApi.Model.Domain;

namespace DotNetApi.Model.DataAccess
{
    public interface IProductRepository
    {
        /// <summary>
        /// Retrieves products matching provided filter. Method supports pagination.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        IEnumerable<Product> GetByFilter(int pageNumber = 1, int pageSize = 1, string? code = null, string? name = null);

        /// <summary>
        /// Retrieves product by code.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Product? GetByCode(string code);

        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Product Add(string code, string name);
    }
}
