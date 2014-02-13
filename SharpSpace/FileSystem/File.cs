using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace SharpSpace.FileSystem
{
    public class File: AFileSystemObject
    {

        private long _size = 0;

        public override long Size
        {
            get
            {
                return _size;
            }
        }

        public File(FileSystem fs, FileInfo fi)
            : base(fs,fi)
        {
            this._size = fi.Length;
        }

    }
}
