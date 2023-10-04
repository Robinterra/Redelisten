using System.Text.Json;
using System.Text.Json.Serialization;
using Redelisten.Data.Extensions;

namespace Redelisten.Data.Helper
{

    public class JsonGuidConverter : JsonConverter<Guid>
    {

        public JsonGuidConverter()
        {

        }

        public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? value = reader.GetString();
            if (string.IsNullOrEmpty(value)) return Guid.Empty;

            return GetGuid(value);
        }

        public static Guid GetGuid(string value)
        {
            ReadOnlySpan<char> spanResult = value;
            int length = spanResult.Length;
            Span<char> mutableSpan = stackalloc char[length + 2];
            spanResult.CopyTo(mutableSpan);

            mutableSpan.Replace('_', '/');
            mutableSpan.Replace('-', '+');
            mutableSpan[length] = '=';
            mutableSpan[length + 1] = '=';

            Span<byte> resultBinaryGuid = stackalloc byte[16];

            if (!Convert.TryFromBase64Chars(mutableSpan, resultBinaryGuid, out _)) return Guid.Empty;

            Guid id = new Guid(resultBinaryGuid);
            return id;
        }

        public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options)
        {
            if (value == Guid.Empty)
            {
                writer.WriteNullValue();
                return;
            }

            Span<byte> binaryGuid = stackalloc byte[16];
            if (!value.TryWriteBytes(binaryGuid)) return;

            Span<char> base64text = stackalloc char[24];
            if (!Convert.TryToBase64Chars(binaryGuid, base64text, out int numbers)) return;

            base64text = base64text.Slice(0, 22);

            base64text.Replace('/', '_');
            base64text.Replace('+', '-');

            writer.WriteStringValue(base64text);
        }

    }

}