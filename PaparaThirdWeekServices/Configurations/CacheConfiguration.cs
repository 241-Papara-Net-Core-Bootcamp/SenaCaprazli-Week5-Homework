namespace PaparaThirdWeek.Services.Configurations
{
    public class CacheConfiguration
    {
        public int AbsoluteExpirationInHours { get; set; } // cache ne kadar hayatta kalacak
        public int SlidingExpritionInMinutes { get; set; } //cache kullanılmadığı zaman silinsin
    }
}
