using System;
using System.IO;
using System.IO.Compression;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace FileProviders.Zip
{
    public class ZipFileProvider : IFileProvider
    {
        private readonly ZipArchive _archive;

        public ZipFileProvider(string path)
        {
            _archive = ZipFile.OpenRead(path);
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            throw new NotImplementedException();
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            // internal paths do not start with a root slash.
            var entry = _archive.GetEntry(subpath.Substring(1));
            if (entry == null)
            {
                return new NotFoundFileInfo(subpath);
            }

            return new ZippedFileInfo(entry);
        }

        public IChangeToken Watch(string filter)
        {
            // Assume Zips don't change.
            return NullChangeToken.Singleton;
        }

        private class ZippedFileInfo : IFileInfo
        {
            private ZipArchiveEntry _entry;

            public ZippedFileInfo(ZipArchiveEntry entry)
            {
                _entry = entry;
            }

            public bool Exists => true;

            public long Length => _entry.Length;

            public string PhysicalPath => null;

            public string Name => _entry.Name; // TODO: Dir names

            public DateTimeOffset LastModified => _entry.LastWriteTime;

            public bool IsDirectory => false; // TODO: Based on the file name ending in slash

            public Stream CreateReadStream()
            {
                return _entry.Open();
            }
        }
    }
}
