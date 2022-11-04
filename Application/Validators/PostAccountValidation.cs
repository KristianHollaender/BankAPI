using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class PostAccountValidation : AbstractValidator<PostAccountDTO>
{
    public PostAccountValidation()
    {
        RuleFor(a => a.AccountName).NotEmpty();
        RuleFor(a => a.CustomerId).NotEmpty();
    }
}