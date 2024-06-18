using Morwinyon.ResilienceFaultTolerance;
/// <summary>
/// This program demonstrates resilience patterns using a MyService class.
/// </summary>
class Program
{
    static async Task Main(string[] args)
    {
        /// <summary>
        /// Creates an instance of the MyService class.
        /// </summary>
        var myService = new MyService();

        try
        {
            /// <summary>
            /// Calls the MyMethodAsync method of the MyService class asynchronously.
            /// This method implements a retry policy in case of failures.
            /// </summary>
            await myService.MyMethodAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"MyMethodAsync hata verdi: {ex.Message}");
        }

        try
        {
            /// <summary>
            /// Calls the MyMethodWithoutResultAsync method of the MyService class asynchronously.
            /// This method implements a circuit breaker policy in case of failures.
            /// </summary>
            await myService.MyMethodWithoutResultAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"MyMethodWithoutResultAsync hata verdi: {ex.Message}");
        }
    }
}

/// <summary>
/// This class provides methods with resilience patterns.
/// </summary>
public class MyService
{
    /// <summary>
    /// This method calls an unreliable method (MyUnreliableMethodAsync) asynchronously 
    /// with retry logic in case of failures.
    /// </summary>
    public async Task MyMethodAsync()
    {
        var retryPolicy = new ResiliencePolicy
        {
            PolicyType = ResiliencePolicyType.Retry,
            PolicyConfig = new RetryPolicyConfig
            {
                RetryCount = 3,
                RetryDelayInSeconds = 2
            }
        };

        var result = await new Func<Task<string>>(async () => await MyUnreliableMethodAsync()).ExecuteWithResilienceAsync(retryPolicy);
        Console.WriteLine(result);
    }

    /// <summary>
    /// This method calls an unreliable method (MyUnreliableVoidMethodAsync) asynchronously 
    /// with circuit breaker logic in case of failures.
    /// </summary>
    public async Task MyMethodWithoutResultAsync()
    {
        var circuitBreakerPolicy = new ResiliencePolicy
        {
            PolicyType = ResiliencePolicyType.CircuitBreaker,
            PolicyConfig = new CircuitBreakerPolicyConfig
            {
                ExceptionsAllowedBeforeBreaking = 2,
                DurationOfBreakInSeconds = 30
            }
        };

        await new Func<Task>(async () => await MyUnreliableVoidMethodAsync()).ExecuteWithResilienceAsync(circuitBreakerPolicy);
    }

    /// <summary>
    /// This method simulates unreliable behavior by throwing an exception.
    /// Replace this with your actual application logic.
    /// </summary>
    private async Task<string> MyUnreliableMethodAsync()
    {
        // Application logic here
        await Task.Delay(500); // Sample delay
        throw new Exception("An error has occurred!"); // Error simulation
        return "Successful outcome";
    }

    /// <summary>
    /// This method simulates unreliable behavior by throwing an exception.
    /// Replace this with your actual application logic.
    /// </summary>
    private async Task MyUnreliableVoidMethodAsync()
    {
        // Application logic here
        await Task.Delay(500); // Sample delay
        throw new Exception("An error has occurred!"); // Error simulation
    }
}
