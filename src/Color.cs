namespace Starlight.NullLink;

[GenerateSerializer]
[Alias("Color")]
public sealed record Color
{
    public static Color White = new() { R = 255, G = 255, B = 255 };

    [Id(0)]
    public byte R { get; set; }
    [Id(1)]
    public byte G { get; set; }
    [Id(2)]
    public byte B { get; set; }
}