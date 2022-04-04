namespace AS91892.Data;

/// <summary>
/// Custom type converter to serialize a string into a timespan
/// </summary>
public class TimeSpanConverter : JsonConverter<TimeSpan>
{

    /// <inheritdoc></inheritdoc>
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return TimeSpan.Parse(reader.GetString()!);
    }

    /// <inheritdoc></inheritdoc>
    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
