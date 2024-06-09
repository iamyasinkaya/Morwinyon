namespace Morwinyon.Logging;

/// <summary>
/// Dosya rotasyon stratejilerini temsil eder.
/// </summary>
public record FileRotationStrategy(int Value, string Name)
{
    /// <summary>
    /// Rotasyon yapılmaz, dosya sürekli genişler.
    /// </summary>
    public static FileRotationStrategy None { get; } = new FileRotationStrategy(1, "None");

    /// <summary>
    /// Boyuta göre rotasyon yapılır.
    /// </summary>
    public static FileRotationStrategy SizeBased { get; } = new FileRotationStrategy(1, "SizeBased");

    /// <summary>
    /// Tarih bazlı rotasyon yapılır.
    /// </summary>
    public static FileRotationStrategy DateBased { get; } = new FileRotationStrategy(1, "DateBased");

    public static implicit operator int(FileRotationStrategy fileRotationStrategy) => fileRotationStrategy.Value;

    public static implicit operator FileRotationStrategy(int value) => value switch
    {
        0 => FileRotationStrategy.None,
        1 => FileRotationStrategy.SizeBased,
        2 => FileRotationStrategy.DateBased,
        _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Invalid value")
    };

    public bool IsFileRotationStrategy => this == FileRotationStrategy.None || this == FileRotationStrategy.SizeBased || this == FileRotationStrategy.DateBased;

    public override string ToString() => Name;
}
