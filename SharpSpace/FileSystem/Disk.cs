using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace SharpSpace.FileSystem
{
    public class Disk: AFileSystemContainer
    {

        private DriveInfo di;

        public long TotalSpace
        {
            get
            {
                return di.TotalFreeSpace;
            }
        }

        public long UsedSpace
        {
            get
            {
                return di.TotalSize - di.TotalFreeSpace;
            }
        }

        public Disk(FileSystem fs, DriveInfo di)
            : base(fs,di.RootDirectory)
        {
            this.fs = fs;
            this.di = di;
            this.Name = di.Name;
        }


    }
}
