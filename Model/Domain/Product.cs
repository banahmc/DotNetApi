namespace DotNetApi.Model.Domain
{
    public class Product
    {
        public required int Id { get; init; }
        public required string Code { get; init; }
        public required string Name { get; init; }
    }
}
