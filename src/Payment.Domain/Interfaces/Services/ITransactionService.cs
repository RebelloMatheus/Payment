using Payment.Domain.Contracts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Payment.Domain.Interfaces.Services
{
    public interface ITransactionService
    {
        Task<TransactionContract> ProcessAsync(CardPaymentContract contract, CancellationToken cancellationToken = default);

        Task<IEnumerable<TransactionContract>> GetIdAssync(int? id);

        Task<IEnumerable<TransactionContract>> GetIdsAssync(IEnumerable<int> ids);

        Task<IEnumerable<TransactionContract>> ListAvailableAssync();

        Task<IEnumerable<TransactionContract>> GetAllAntecipationIdAsync(int id);
    }
}