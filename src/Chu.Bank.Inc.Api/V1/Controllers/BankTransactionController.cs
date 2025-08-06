using Asp.Versioning;
using Chu.Bank.Inc.Api.Controllers;
using Chu.Bank.Inc.Application.UseCases.Transactions.Make;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chu.Bank.Inc.Api.V1.Controllers;

[ApiVersion(1.0)]
[Route("v{version:apiVersion}/bank-transactions")]
public class BankTransactionController : ApiController
{
    private readonly IMediator _mediator;

    public BankTransactionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("make")]
    public async Task<IActionResult> MakeTransaction([FromBody] MakeTransactionInput input, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(input, cancellationToken);
        
        return Ok(result);
    }
}