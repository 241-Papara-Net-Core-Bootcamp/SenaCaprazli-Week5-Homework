using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Papara.BackgroundQueue.Api.Background;
using Papara.BackgroundQueue.Api.Services;

namespace Papara.BackgroundQueue.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBackgroundQueue<Book> queue;

        public BookController(IBackgroundQueue<Book> queue)
        {
            this.queue = queue;
        }

        [HttpPost]
        public IActionResult Publish(Book model)
        {
            queue.Enqueue(model);
            return Accepted("İşleminiz sıraya alındı");
        }
    }
}
