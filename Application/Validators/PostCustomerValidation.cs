using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class PostCustomerValidation : AbstractValidator<PostCustomerDTO>
{
    public PostCustomerValidation()
    {
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.LastName).NotEmpty();
    }
}