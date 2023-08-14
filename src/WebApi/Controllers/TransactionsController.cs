using Infrastructure.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;
using WebApi.Models.Transactions;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionsController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Post(TransactionCreateRequestModel request, CancellationToken cancellationToken)
    {
        var validator = await new TransactionCreateRequestModel.Validator()
            .ValidateAsync(request, cancellationToken);

        if (validator.IsValid)
        {
            var transactionId = await _transactionService.CreateTransactionAsync(request.TransactionBy, request.Amount, cancellationToken);
            var url = Url.Action(nameof(Get), new
            {
                transactionId
            });
            return Results.Created(url!, null);
        }

        return Results.ValidationProblem(validator.GetErrors());
    }

    [HttpGet("{transactionId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get(Guid transactionId, CancellationToken cancellationToken)
    {
        var transaction = await _transactionService.GetTransactionByIdAsync(transactionId, cancellationToken);
        return transaction is null ? Results.NotFound() : Results.Ok(transaction);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Get(CancellationToken cancellationToken)
    {
        var transaction = await _transactionService.GetAllTransactionsAsync(cancellationToken);
        return Results.Ok(transaction);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Put(TransactionUpdateRequestModel request, CancellationToken cancellationToken)
    {
        await _transactionService.UpdateTransactionAsync(
            request.TransactionId,
            request.TransactionBy,
            request.Amount,
            cancellationToken);

        return Results.Ok();
    }

    [HttpDelete("{transactionId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> Delete(Guid transactionId, CancellationToken cancellationToken)
    {
        await _transactionService.DeleteTransactionAsync(transactionId, cancellationToken);
        return Results.NoContent();
    }
}
