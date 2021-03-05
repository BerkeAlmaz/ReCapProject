using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Core.Utilities.FileHelper
{
    public static class ImageFileHelper
    {

        public static string UploadFile(IFormFile file)
        {
            return Upload(file);
        }

        

        public static void DeleteFile(string path)
        {
            File.Delete(path);
        }

        public static string UpdateFile(string path, IFormFile file)
        {
            File.Delete(path);

            return Upload(file);

        }

        public static string NewPath(IFormFile file)
        {
            FileInfo ff = new FileInfo(file.FileName);
            string fileExtension = ff.Extension;

            string path = Environment.CurrentDirectory + @"\wwwroot\Images";
            var newPath = Guid.NewGuid().ToString() + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Year + fileExtension;

            string result = $@"{path}\{newPath}";
            return result;
        }

        private static string Upload(IFormFile file)
        {
            var filePath = Path.GetTempFileName();
            if (file.Length > 0 || file != null)
            {

                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }

            }
            var result = NewPath(file);
            File.Move(filePath, result);
            return result;
        }
    }
}
