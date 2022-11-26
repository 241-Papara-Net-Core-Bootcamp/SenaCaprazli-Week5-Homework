using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Papara.BackgroundQueue.Api.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Papara.BackgroundQueue.Api.Background
{
    public class BackgroundWorker : BackgroundService
    {
        private readonly IBackgroundQueue<Book> queue;
        private readonly IServiceScopeFactory scopeFactory;
        private readonly ILogger<BackgroundWorker> logger;

        public BackgroundWorker(IBackgroundQueue<Book> queue,
            IServiceScopeFactory scopeFactory,
            ILogger<BackgroundWorker> logger)
        {
            this.queue = queue;
            this.scopeFactory = scopeFactory;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("{Type} is now running in the background", nameof(BackgroundWorker));
            await BackgroundProcessing(stoppingToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogCritical("The {Type} is stopping due to host shutdown, " +
                "queued item might not processed anymore", nameof(BackgroundWorker));
            return base.StopAsync(cancellationToken);
        }

        private async Task BackgroundProcessing(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(500, cancellationToken);
                    var book = queue.Dequeue();
                    if (book == null) continue;

                    logger.LogInformation("Book found! Starting to process...");
                    using (var scope = scopeFactory.CreateScope())
                    {
                        var publisher = scope.ServiceProvider.GetRequiredService<IBookPublisher>();
                        await publisher.Publish(book, cancellationToken);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogCritical("An error occured when publishing a book exception {Exception}", ex);
                }
            }
        }
    }
}
