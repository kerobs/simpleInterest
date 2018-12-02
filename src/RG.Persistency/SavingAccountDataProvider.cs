using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RG.Accounts.Model;
using RG.Accounts.Services;

namespace RG.Persistency
{
    public class SavingAccountDataProvider
    {

        public virtual IEnumerable<InterestRates> GetSavingAccountRates()
        {
            var savingInterestRates = new List<InterestRates>
            {
                new InterestRates {Id =1, LowerLimit = 0, UpperLimit = 1000,Rate = 0.01m},
                new InterestRates {Id =2, LowerLimit = 1000, UpperLimit = 5000,Rate = 0.015m},
                new InterestRates {Id =3, LowerLimit = 5000, UpperLimit = 10000,Rate = 0.02m},
                new InterestRates {Id =4, LowerLimit = 10000, UpperLimit = 50000,Rate = 0.025m},
                new InterestRates {Id =5, LowerLimit = 50000, UpperLimit = decimal.MaxValue, Rate = 0.03m}
            };

            return savingInterestRates;
        }
    }
}
