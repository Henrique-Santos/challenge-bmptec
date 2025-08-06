using Asp.Versioning;
using Chu.Bank.Inc.Api.Controllers;
using Chu.Bank.Inc.Application.UseCases.Accounts.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chu.Bank.Inc.Api.V1.Controllers;

[ApiVersion(1.0)]
[Route("v{version:apiVersion}/bank-accounts")]
public class BankAccountController : ApiController
{
    private readonly IMediator _mediator;

    public BankAccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateBankAccount([FromBody] CreateAccountInput input, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(input, cancellationToken);

        return Ok(result);
    }
}