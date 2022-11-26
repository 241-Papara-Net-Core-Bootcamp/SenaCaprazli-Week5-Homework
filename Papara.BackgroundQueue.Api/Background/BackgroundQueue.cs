using System;
using System.Collections.Concurrent;

namespace Papara.BackgroundQueue.Api.Background
{
    public interface IBackgroundQueue<T>
    {
        /// <summary>
        /// Process işlemlerini zamanı gelince çalıştıran method. Kuyruğa ekleme
        /// </summary>
        /// <param name="item"></param>
        void Enqueue(T item);
        /// <summary>
        /// Kuyruğa eklenen elemanı siler.
        /// </summary>
        /// <returns></returns>
        T Dequeue();
    }
    public class BackgroundQueue<T> : IBackgroundQueue<T> where T : class
    {
        private readonly ConcurrentQueue<T> _items = new ConcurrentQueue<T>();

        public T Dequeue()
        {
            var success = _items.TryDequeue(out var result);
            return success ? result : null; // başarılı olursa çıkarsın, başarısız olursa null dönsün
        }

        public void Enqueue(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            _items.Enqueue(item);
        }
    }
}
