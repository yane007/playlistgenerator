using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PG.Services.Contract;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GenreUpdateService
{
    public class GenreHostedService : IHostedService
    {
        private Timer _timer;
        private readonly IServiceProvider _serviceProvider;

        public GenreHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(CallSyncGenresAsync, null, TimeSpan.Zero,
                TimeSpan.FromMinutes(10));

            return Task.CompletedTask;
        }

        private async void CallSyncGenresAsync(object state)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var genreService = scope.ServiceProvider.GetRequiredService<IGenreService>();
                Log.Logger.Information("Syncing genres");
                await genreService.SyncGenresAsync();
                Log.Logger.Information("Syncing done");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }
}
