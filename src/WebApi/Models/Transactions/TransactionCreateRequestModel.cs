using FluentValidation;

namespace WebApi.Models.Transactions;

public sealed record TransactionCreateRequestModel(string TransactionBy, decimal Amount)
{
    public class Validator : AbstractValidator<TransactionCreateRequestModel>
    {
        public Validator()
        {
            RuleFor(x => x.TransactionBy)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .MaximumLength(256);

            RuleFor(x => x.Amount)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .GreaterThanOrEqualTo(0);
        }
    }
}
