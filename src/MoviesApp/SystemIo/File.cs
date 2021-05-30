using System.IO;

namespace MoviesApp.SystemIo
{
    public class File : IFile
    {
        public Stream OpenRead(string filePath)
        {
            return System.IO.File.OpenRead(filePath);
        }
    }
}
