using DotNetApi.Model.Cqrs.Products;
using FluentValidation;

namespace DotNetApi.Model.Domain.Validation
{
    public sealed class CreateProductCommandValidator
        : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(command => command.Code)
                .NotNull()
                .WithMessage("The product code is required.")
                .NotEmpty()
                .WithMessage("The product code cannot be empty.")
                .MaximumLength(13)
                .WithMessage("The product code needs to be max 13 characters long.");

            RuleFor(command => command.Name)
                .NotNull()
                .WithMessage("The product name is required.")
                .NotEmpty()
                .WithMessage("The product name cannot be empty.")
                .MaximumLength(255)
                .WithMessage("The product name needs to be max 255 characters long.");
        }
    }
}
