using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace MoviesApp.Converters
{
    public interface IJsonConvert
    {
        Task<T> DeserializeAsync<T>(Stream stream, JsonSerializerOptions options);
    }
}