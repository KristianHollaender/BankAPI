using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class PutAccountValidation : AbstractValidator<PutAccountDTO>
{
    public PutAccountValidation()
    {
        RuleFor(a => a.id).NotEmpty();
        RuleFor(a => a.AccountName).NotEmpty();
        RuleFor(a => a.CustomerId).NotEmpty();
    }
}