using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PG.Services.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GenreUpdateService
{
    public class GenreHostedService : IHostedService
    {
        private Timer timer;
        private readonly IServiceProvider serviceProvider;

        public GenreHostedService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.timer = new Timer(CallSyncGenresAsync, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(3));

            return Task.CompletedTask;
        }

        private async void CallSyncGenresAsync(object state)
        {
            using (var scope = this.serviceProvider.CreateScope())
            {
                //Tuka logikata 
                var genreService = scope.ServiceProvider.GetRequiredService<IGenreService>();
                await genreService.SyncGenresAsync();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }
}
