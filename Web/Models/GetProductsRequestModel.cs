namespace DotNetApi.Web.Models
{
    public sealed class GetProductsRequestModel : RequestWithPagination
    {
        public GetProductsRequestModel()
            : base(minPageNumber: 1, minPageSize: 10)
        {
        }

        // Filter
        public string? Code { get; set; }
        public string? Name { get; set; }
    }
}
