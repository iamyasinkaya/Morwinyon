namespace Morwinyon.Logging;

/// <summary>
/// Dosya boyutu limitine ulaşıldığında nasıl davranılacağını temsil eder.
/// </summary>
public record FileSizeLimitBehavior(int Value, string Name)
{
    /// <summary>
    /// Log dosyasını kes.
    /// </summary>
    public static FileSizeLimitBehavior Truncate { get; } = new FileSizeLimitBehavior(1, "Truncate");

    /// <summary>
    /// Yeni bir dosyaya geçir (rotasyon yap).
    /// </summary>
    public static FileSizeLimitBehavior RollOver { get; } = new FileSizeLimitBehavior(2, "RollOver");

    public static implicit operator int(FileSizeLimitBehavior fileSizeLimitBehavior) => fileSizeLimitBehavior.Value;
    public static implicit operator FileSizeLimitBehavior(int value) => value switch
    {
        1 => Truncate,
        2 => RollOver,
        _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Invalid value")
    };

    public bool IsFileSizeLimitBehavior => this == FileSizeLimitBehavior.Truncate || this == FileSizeLimitBehavior.RollOver;

    public override string ToString() => Name;
}
