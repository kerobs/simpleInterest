using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RG.Accounts.Model;
using RG.Accounts.Services;

namespace RG.Persistency
{
    public class InterestRateService : IInterestRateService
    {
        private readonly SavingAccountDataProvider _savingAccountDataProvider;
        public InterestRateService(SavingAccountDataProvider savingAccountDataProvider)
        {
            this._savingAccountDataProvider = savingAccountDataProvider;
        }

        public Task<decimal> CalculateSimpleInterestAsync(decimal principalAmount, DateTime depositDate)
        {
            var interestRates = _savingAccountDataProvider.GetSavingAccountRates();

            var rateOfInterst = interestRates
                .FirstOrDefault(
                    x => principalAmount >= x.LowerLimit
                         && principalAmount < x.UpperLimit)?.Rate ?? 1;
            var numberOfYeas = (decimal)DateTime.Now.Subtract(depositDate).TotalDays / 365;

            var interest = Math.Round(principalAmount * rateOfInterst * numberOfYeas, 2);
            return Task.FromResult(interest);

        }

    }
}
