using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace VCardOnAbp.ApiServices.Vmcardio.Json;


public class NullableDecimalConverter : JsonConverter<decimal?>
{
    public override decimal? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Handle null
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }

        // Handle both number and string representations
        if (reader.TokenType == JsonTokenType.Number && reader.TryGetDecimal(out decimal number))
        {
            return number;
        }
        else if (reader.TokenType == JsonTokenType.String)
        {
            string? numberString = reader.GetString();
            if (decimal.TryParse(numberString, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result))
            {
                return result;
            }
        }

        throw new JsonException($"Unable to convert \"{reader.GetString()}\" to decimal?.");
    }

    public override void Write(Utf8JsonWriter writer, decimal? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
        {
            // Serialize decimal as a number
            writer.WriteNumberValue(value.Value);
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}
