namespace DotNetApi.Model.Domain.Validation
{
    public record ValidationError(string PropertyName, string ErrorMessage);
}
