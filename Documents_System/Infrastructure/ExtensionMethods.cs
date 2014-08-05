using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Documents_System.Infrastructure
{
    public static class ExtensionMethods
    {
        public static byte[] GetFileData(this string fileName, string filePath)
        {
            var fullFilePath = string.Format("{0}/{1}", filePath, fileName);
            if (!System.IO.File.Exists(fullFilePath))
            {
                throw new FileNotFoundException("The file does not exist.",
                fullFilePath);
            }
            return System.IO.File.ReadAllBytes(fullFilePath);

        }
    }
}