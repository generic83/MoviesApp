using System.IO;

namespace MoviesApp.SystemIo
{
    public interface IFile
    {
        Stream OpenRead(string filePath);
    }
}
