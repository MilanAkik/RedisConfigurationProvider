namespace RedisConfigurationProvider
{
    public class Class1
    {
        public static IEnumerable<int> GetSquares(int n) => Enumerable.Range(1, n).Select(x => x * x);
    }
}
