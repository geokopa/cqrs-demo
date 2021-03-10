using CQRS.Account.Commands.CreateAccount;
using CQRS.Account.Models;
using CQRS.Account.Queries.GetAccountById;
using CQRS.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CQRS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount(long id)
        {
            var account = await _mediator.Send(new GetAccountByIdQuery() { Id = id });

            if (account == null)
                return NotFound();

            return Ok(account);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountModel request)
        {
            var createdAccountId = await _mediator.Send(new CreateAccountCommand { Iban = request.Iban, Type = request.Type });

            return Created("/", new { createdAccountId });
        }
    }
}
