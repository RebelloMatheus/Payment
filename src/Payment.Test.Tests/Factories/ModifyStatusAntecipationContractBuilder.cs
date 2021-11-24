using Payment.Domain.Contracts;
using Payment.Domain.Enumerators;
using System.Collections.Generic;

namespace Payment.Test.Tests.Factories
{
    internal class ModifyStatusAntecipationContractBuilder
    {
        private AntecipationStatus antecipationStatus;
        private IEnumerable<int> transactionsId;

        public ModifyStatusAntecipationContract Build()
        {
            return new ModifyStatusAntecipationContract(
                antecipationStatus: antecipationStatus,
                transactionsId: transactionsId);
        }

        public ModifyStatusAntecipationContractBuilder()
        {
            antecipationStatus = AntecipationStatus.Approved;
        }

        public ModifyStatusAntecipationContractBuilder SetAntecipationStatus(AntecipationStatus antecipationStatus)
        {
            this.antecipationStatus = antecipationStatus;
            return this;
        }

        public ModifyStatusAntecipationContractBuilder SettransactionsId(IEnumerable<int> transactionsId)
        {
            if (transactionsId == null)
                transactionsId = new List<int>();

            this.transactionsId = transactionsId;

            return this;
        }
    }
}