using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using DalToWeb.Repositories;

namespace CustomAuth.Utils
{
    public static class FileHelper
    {
        public static string SaveFileToDisk(HttpPostedFileBase img, string mapPath)
        {
            var r = new Random();
            Directory.CreateDirectory(mapPath + "/UserContent/");
            var ext = "." + img.FileName.Split('.').Last();
            var name = r.Next(1000000, Int32.MaxValue).ToString() +
                       r.Next(1000000, Int32.MaxValue).ToString() + ext;
            var fName = mapPath + "UserContent\\" + name;
            img.SaveAs(fName);
            return name;
        }

        public static void RemoveFileFromDisk(string mapPath, string path)
        {
            if(string.IsNullOrEmpty(path))
                return;
            var fullPath = mapPath + "/UserContent/" + path;
            File.Delete(fullPath);
        }
    }
}