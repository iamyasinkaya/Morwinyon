using Polly;
using System;

namespace Morwinyon.ResilienceFaultTolerance;

/// <summary>
/// Provides extension methods for executing actions with resilience policies.
/// </summary>
public static class ResilienceExtensions
{
    /// <summary>
    /// Executes an asynchronous action with resilience applied using the specified policy.
    /// </summary>
    /// <typeparam name="TResult">The return type of the asynchronous action.</typeparam>
    /// <param name="action">The asynchronous action to execute.</param>
    /// <param name="policy">The resilience policy to apply.</param>
    /// <returns>A Task containing the result of the asynchronous action.</returns>
    /// <exception cref="NotSupportedException">Thrown if the provided policy type is not supported.</exception>
    public static async Task<TResult> ExecuteWithResilienceAsync<TResult>(this Func<Task<TResult>> action, ResiliencePolicy policy)
    {
        IAsyncPolicy<TResult> asyncPolicy;

        switch (policy.PolicyType)
        {
            case ResiliencePolicyType.Retry:
                var retryConfig = (RetryPolicyConfig)policy.PolicyConfig;
                asyncPolicy = ResiliencePolicyHelper.CreateRetryPolicy(retryConfig).AsAsyncPolicy<TResult>();
                break;

            case ResiliencePolicyType.CircuitBreaker:
                var cbConfig = (CircuitBreakerPolicyConfig)policy.PolicyConfig;
                asyncPolicy = ResiliencePolicyHelper.CreateCircuitBreakerPolicy(cbConfig).AsAsyncPolicy<TResult>();
                break;

            default:
                throw new NotSupportedException($"Policy type {policy.PolicyType} is not supported.");
        }

        return await asyncPolicy.ExecuteAsync(action);
    }

    /// <summary>
    /// Executes a synchronous action with resilience applied using the specified policy.
    /// </summary>
    /// <param name="action">The synchronous action to execute.</param>
    /// <param name="policy">The resilience policy to apply.</param>
    /// <exception cref="NotSupportedException">Thrown if the provided policy type is not supported.</exception>
    public static async Task ExecuteWithResilienceAsync(this Func<Task> action, ResiliencePolicy policy)
    {
        IAsyncPolicy asyncPolicy;

        switch (policy.PolicyType)
        {
            case ResiliencePolicyType.Retry:
                var retryConfig = (RetryPolicyConfig)policy.PolicyConfig;
                asyncPolicy = ResiliencePolicyHelper.CreateRetryPolicy(retryConfig);
                break;

            case ResiliencePolicyType.CircuitBreaker:
                var cbConfig = (CircuitBreakerPolicyConfig)policy.PolicyConfig;
                asyncPolicy = ResiliencePolicyHelper.CreateCircuitBreakerPolicy(cbConfig);
                break;

            default:
                throw new NotSupportedException($"Policy type {policy.PolicyType} is not supported.");
        }

        await asyncPolicy.ExecuteAsync(action);
    }
}
