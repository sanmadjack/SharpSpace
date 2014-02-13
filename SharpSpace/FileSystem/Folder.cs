using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace SharpSpace.FileSystem
{
    public class Folder: AFileSystemContainer
    {
        public Folder(FileSystem fs, DirectoryInfo di)
            : base(fs,di)
        {
        }
    }
}
