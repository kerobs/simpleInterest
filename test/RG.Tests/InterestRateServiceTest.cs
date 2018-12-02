using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using RG.Accounts.Model;
using RG.Accounts.Services;
using RG.Persistency;
using Shouldly;
using Xunit;

namespace RG.Tests
{
    public class InterestRateServiceTest : IDisposable
    {
        private readonly IInterestRateService _interestRateService;
        private readonly UserSocietyAccount _userSocietyAccount;

        public InterestRateServiceTest()
        {

            var mockSavingAccountDataProvider = new Mock<SavingAccountDataProvider>();

            mockSavingAccountDataProvider.Setup(x => x.GetSavingAccountRates())
                .Returns(
                    new List<InterestRates>
                {
                    new InterestRates {Id =1, LowerLimit = 0, UpperLimit = 1000,Rate = 0.01m},
                    new InterestRates {Id =2, LowerLimit = 1000, UpperLimit = 5000,Rate = 0.015m},
                    new InterestRates {Id =3, LowerLimit = 5000, UpperLimit = 10000,Rate = 0.02m},
                    new InterestRates {Id =4, LowerLimit = 10000, UpperLimit = 50000,Rate = 0.025m},
                    new InterestRates {Id =5, LowerLimit = 50000, UpperLimit = decimal.MaxValue, Rate = 0.03m}
                });

            _interestRateService = new InterestRateService(mockSavingAccountDataProvider.Object);

            _userSocietyAccount = new UserSocietyAccount
            {
                Id = 1,
                User = new User { UserId = 1, FirstName = "Jack", LastName = "Smith" },
                AccountBalance = 1001,
                AccountDepositDate = DateTime.Now.AddYears(-1)
            };
        }


        [Fact]
        public async Task Interest_should_not_be_null()
        {
            var interestForThisPeriod = await _interestRateService.CalculateSimpleInterestAsync(
                  _userSocietyAccount.AccountBalance,
                  _userSocietyAccount.AccountDepositDate);

            interestForThisPeriod.ShouldNotBeNull();
        }

        [Fact]
        public async Task Interest_should_be_a_known_value_when_rate_of_interest_is_know_for_a_given_peroid_and_principal_amount()
        {
            var interestForThisPeriod = await _interestRateService.CalculateSimpleInterestAsync(
                _userSocietyAccount.AccountBalance,
                _userSocietyAccount.AccountDepositDate);

            interestForThisPeriod.ShouldNotBeNull();
            interestForThisPeriod.ShouldBe(15.02m);
        }

        public void Dispose()
        {
        }
    }
}
