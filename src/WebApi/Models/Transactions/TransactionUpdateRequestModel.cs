using FluentValidation;

namespace WebApi.Models.Transactions;

public sealed record TransactionUpdateRequestModel(Guid TransactionId, string TransactionBy, decimal Amount)
{
    public sealed class Validator : AbstractValidator<TransactionUpdateRequestModel>
    {
        public Validator()
        {
            RuleFor(x => x.TransactionId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEqual(Guid.Empty)
                .WithMessage("Transaction id must not be empty");
            
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
