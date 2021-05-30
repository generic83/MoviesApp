using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace MoviesApp.Converters
{
    public class JsonConvert : IJsonConvert
    {
        public async Task<T> DeserializeAsync<T>(Stream stream, JsonSerializerOptions options)
        {
            return await JsonSerializer.DeserializeAsync<T>(stream, options);
        }
    }
}
