namespace DotNetApi.Model.Domain.Exceptions
{
    public sealed class ProductAlreadyExistsException : ValidationException
    {
        public ProductAlreadyExistsException(string code)
            : base($"Product with code {code} already exists!")
        {
        }
    }
}
