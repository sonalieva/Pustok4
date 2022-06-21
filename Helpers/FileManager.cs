using Microsoft.AspNetCore.Http;
using Pustok4.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok4.Helpers
{
    public static class FileManager
    {
        public static string Save(string root,string folder, IFormFile file)
        {
            var guid = Guid.NewGuid().ToString();
            var newfilename = guid + (file.FileName.Length>64?file.FileName.Substring(file.FileName.Length - 64, 64):file.FileName);
            string path = Path.Combine(root, folder, newfilename);
            using(FileStream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return newfilename;
        }
        public static bool Delete(string root,string folder,string filename)
        {
            string path = Path.Combine(root, folder, filename);
            if (File.Exists(path))
            {
                File.Delete(path);
                    return true;
            }
            return false;

        }

        internal static void DeleteAll(string webRootPath, string v, List<string> deletedFiles)
        {
            throw new NotImplementedException();
        }
    }
}
