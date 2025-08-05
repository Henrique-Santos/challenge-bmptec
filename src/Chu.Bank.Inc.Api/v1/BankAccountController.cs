using Asp.Versioning;
using Chu.Bank.Inc.Api.Controllers;
using Microsoft.AspNetCore.Components;

namespace Chu.Bank.Inc.Api.v1;

[ApiVersion(1.0)]
[Route("v{version:apiVersion}/bank-accounts")]
public class BankAccountController : ApiController
{
    
}