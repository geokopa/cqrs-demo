using CQRS.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Account.Commands.CreateAccount
{
    public class CreateAccountCommand : IRequest<long>
    {
        public string Type { get; set; }
        public string Iban { get; set; }
    }

    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, long>
    {
        private readonly AppDbContext _context;

        public CreateAccountCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<long> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var createdAccount = _context.Accounts.Add(new CQRS.Models.Account { Iban = request.Iban, Type = request.Type });
            await _context.SaveChangesAsync();

            return createdAccount.Entity.Id;
        }
    }
}
