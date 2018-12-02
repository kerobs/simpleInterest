using System;
using System.Threading.Tasks;

namespace RG.Accounts.Services
{
    public interface IInterestRateService
    {
        Task<decimal> CalculateSimpleInterestAsync(decimal principalAmount, DateTime depositDate);
    }
}
