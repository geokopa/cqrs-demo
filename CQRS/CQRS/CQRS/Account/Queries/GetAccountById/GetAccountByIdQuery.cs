using CQRS.Account.Models;
using CQRS.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Account.Queries.GetAccountById
{
    public class GetAccountByIdQuery : IRequest<AccountItemModel>
    {
        public long Id { get; set; }
    }

    public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, AccountItemModel>
    {
        private readonly AppDbContext _context;

        public GetAccountByIdQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AccountItemModel> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var account = await _context.Accounts.FindAsync(request.Id);

            return new AccountItemModel
            {
                Iban = account.Iban,
                Description = $"Requested account is type of {account.Type} and IBAN is {account.Iban}"
            };
        }
    }
}
