using ConsoleApp11;

static void Main()
{
    FunctionCache<string, int> cache = new FunctionCache<string, int>(TimeSpan.FromMinutes(10));

    Func<string, int> expensiveFunction = (key) =>
    {
        Console.WriteLine($"Executing expensive function for key: {key}");
        return key.Length;
    };

    string key1 = "abc";
    string key2 = "def";

    int result1 = cache.GetOrAdd(key1, expensiveFunction);
    int result2 = cache.GetOrAdd(key1, expensiveFunction); 

    int result3 = cache.GetOrAdd(key2, expensiveFunction); 

    Console.WriteLine($"Result 1: {result1}");
    Console.WriteLine($"Result 2 (from cache): {result2}");
    Console.WriteLine($"Result 3: {result3}");
}