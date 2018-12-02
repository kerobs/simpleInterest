using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RG.Accounts.Model;
using RG.Accounts.Services;
using RG.Persistency;

namespace RG.ConsoleApp
{
    public class Program
    {
        static ILogger<Program> _logger;

        public static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false);
        }

        static async Task MainAsync(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<SavingAccountDataProvider>()
                .AddSingleton<IInterestRateService, InterestRateService>()
                .BuildServiceProvider();

            serviceProvider
                   .GetService<ILoggerFactory>()
                   .AddConsole(LogLevel.Information);

            _logger = serviceProvider.GetService<ILoggerFactory>()
               .CreateLogger<Program>();

            _logger.LogInformation("Starting Application");


            var interestRateService
                = serviceProvider.GetService<IInterestRateService>();

            var userAccount = new UserSocietyAccount
            {
                Id = 1,
                User = new User { UserId = 1, FirstName = "Jack", LastName = "Smith" },
                AccountBalance = 1001,
                AccountDepositDate = DateTime.Now.AddYears(-1)
            };

            userAccount.InterestForThisPeriod = await interestRateService
                .CalculateSimpleInterestAsync(userAccount.AccountBalance, userAccount.AccountDepositDate);

            WriteOutput(userAccount);

            Console.ReadLine();

        }

        private static void WriteOutput(
            UserSocietyAccount userSocietyAccount)
        {
            _logger.LogInformation($"Account name : {userSocietyAccount.User.FirstName} {userSocietyAccount.User.LastName}");
            _logger.LogInformation($"Account balance : {userSocietyAccount.AccountBalance}");
            _logger.LogInformation($"Account interest : {userSocietyAccount.InterestForThisPeriod}");
            _logger.LogInformation($"Account new balance : { userSocietyAccount.AccountBalance + userSocietyAccount.InterestForThisPeriod}");
        }


    }
}
