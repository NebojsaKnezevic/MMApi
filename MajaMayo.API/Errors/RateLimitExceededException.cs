namespace MajaMayo.API.Errors
{
    public class RateLimitExceededException : Exception
    {
        public RateLimitExceededException() : base("Rate limit exceeded!")
        {
            
        }
    }
}
