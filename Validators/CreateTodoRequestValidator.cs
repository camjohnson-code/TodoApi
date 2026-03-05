using FluentValidation;
using TodoApi.DTOs;

namespace TodoApi.Validators;

public class CreateTodoRequestValidator : AbstractValidator<CreateTodoRequest>
{
    public CreateTodoRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .MaximumLength(100)
            .WithMessage("Title cannot be more than 100 characters.");
        RuleFor(x => x.Description)
            .MaximumLength(500)
            .WithMessage("Description cannot be more than 500 characters.");
    }
}
