using DotNetCore.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Application
{
    public sealed class FileService : IFileService
    {
        public Task<IEnumerable<FileBinary>> AddAsync(string directory, IEnumerable<FileBinary> files)
        {
            Directory.CreateDirectory(directory);

            foreach (var file in files)
            {
                var name = string.Concat(file.Id, Path.GetExtension(file.Name));
                var path = Path.Combine(directory, name);
                File.WriteAllBytes(path, file.Bytes);
                file.Bytes = null;
            }

            return Task.FromResult(files);
        }

        public Task<FileBinary> SelectAsync(string directory, Guid id)
        {
            var fileInfo = new DirectoryInfo(directory).GetFiles("*" + id + "*.*").First();
            var bytes = File.ReadAllBytes(fileInfo.FullName);
            var fileBinary = new FileBinary(id, fileInfo.Name, bytes, fileInfo.Length);

            return Task.FromResult(fileBinary);
        }
    }
}
