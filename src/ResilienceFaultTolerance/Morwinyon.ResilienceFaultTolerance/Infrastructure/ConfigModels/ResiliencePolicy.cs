using System;

namespace Morwinyon.ResilienceFaultTolerance;

/// <summary>
/// Represents a resilience policy configuration.
/// </summary>
public class ResiliencePolicy
{
    /// <summary>
    /// The type of resilience policy to apply.
    /// </summary>
    public ResiliencePolicyType PolicyType { get; set; }

    /// <summary>
    /// The configuration object specific to the chosen resilience policy type.
    /// </summary>
    public object PolicyConfig { get; set; }
}