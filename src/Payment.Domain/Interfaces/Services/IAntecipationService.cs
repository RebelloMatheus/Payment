using Payment.Domain.Contracts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Payment.Domain.Interfaces.Services
{
    public interface IAntecipationService
    {
        Task<IEnumerable<AntecipationsContract>> RequestAntecipationAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default);

        Task<IEnumerable<AntecipationsContract>> GetListHistoryAsync(string status, CancellationToken cancellationToken = default);

        Task<IEnumerable<AntecipationsContract>> ModifyStatusAsync(ModifyStatusAntecipationContract contract, CancellationToken cancellationToken = default);

        Task<AntecipationsContract> StartAnalyzisAsync(int antecipationId, CancellationToken cancellationToken = default);
    }
}