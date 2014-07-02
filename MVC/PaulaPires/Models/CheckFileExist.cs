using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PaulaPires.Models
{
    public class CheckFileExist
    {
        public enum MyPaths
        {
            tecnicas,
            paginasHTML
        }

        private static bool isFileExist(MyPaths myPaths, string fileName)
        {
            string fileFolder;

            switch (myPaths)
            {
                case MyPaths.paginasHTML:
                    fileFolder = HttpContext.Current.Server.MapPath("~/img/paginasHTML/");
                    break;
                default:
                    fileFolder = HttpContext.Current.Server.MapPath("~/img/tecnicas/");
                    break;
            }

            return File.Exists(Path.Combine(fileFolder, fileName));
        }

        public static string ReturnFileUrl(MyPaths myPaths, string fileName)
        {
            if (isFileExist(myPaths, fileName))
            {
                return fileName;
            }
            return "default-img.jpg";
        }
    }
}