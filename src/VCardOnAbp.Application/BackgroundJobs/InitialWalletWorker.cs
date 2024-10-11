using Hangfire;
using System;
using System.Threading;
using System.Threading.Tasks;
using VCardOnAbp.Currencies;
using VCardOnAbp.Users;
using Volo.Abp.BackgroundWorkers.Hangfire;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Uow;
using Volo.Abp.Users;

namespace VCardOnAbp.BackgroundJobs
{
    public class InitialWalletWorker : HangfireBackgroundWorkerBase
    {
        private readonly IRepository<UserCurrency, Guid> _userCurrencyRepo;
        private readonly IRepository<Currency, Guid> _currencyRepo;
        private readonly IUserRepository<IdentityUser> _userRepository;

        public InitialWalletWorker(
            IRepository<UserCurrency, Guid> userCurrencyRepo,
            IUserRepository<IdentityUser> userRepository,
            IRepository<Currency, Guid> currencyRepo
        )
        {
            RecurringJobId = nameof(SyncVmcardioTransactionWorker);
            CronExpression = Cron.Hourly(3);

            _userCurrencyRepo = userCurrencyRepo;
            _currencyRepo = currencyRepo;
            _userRepository = userRepository;
        }

 
        public override Task DoWorkAsync(CancellationToken cancellationToken = default)
        {
            using (var uow = LazyServiceProvider.LazyGetRequiredService<IUnitOfWorkManager>().Begin())
            {
                // TODO: Add create wallet logic here
                return Task.CompletedTask;
            }
        }
    }
}
