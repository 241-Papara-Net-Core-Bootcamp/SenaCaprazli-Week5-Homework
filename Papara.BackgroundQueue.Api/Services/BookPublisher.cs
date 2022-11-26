using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Papara.BackgroundQueue.Api.Services
{
    public interface IBookPublisher
    {
        Task Publish(Book book, CancellationToken cancellationToken);
    }
    public class BookPublisher : IBookPublisher
    {
        private readonly ILogger<BookPublisher> logger;

        public BookPublisher(ILogger<BookPublisher> logger)
        {
            this.logger = logger;
        }

        public async Task Publish(Book book, CancellationToken cancellationToken)
        {
            logger.LogInformation("Veritabanı ve logic işlemler yapılıyor...");
            await Task.Delay(2500, cancellationToken);
            logger.LogInformation($"Yazar {book.Author} {book.Name} kitabını yayınladı!");
        }
    }
}
