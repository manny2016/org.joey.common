using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Joey.Common
{
    public static class LocalFileExtension
    {
        public static void SaveAs(this string target)
        {
            FileInfo info = new FileInfo(target);
            if (!info.Directory.Exists)
                Directory.CreateDirectory(info.Directory.FullName);
        }
    }
}
