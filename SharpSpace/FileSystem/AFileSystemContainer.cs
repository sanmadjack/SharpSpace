using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
namespace SharpSpace.FileSystem
{
    public abstract class AFileSystemContainer : AFileSystemObject
    {


        private long _size;
        public override long Size
        {
            get
            {
                return _size;
            }
        }

        public AFileSystemContainer(FileSystem fs, FileSystemInfo fsi)
            : base(fs, fsi)
        {
        }

        public void LoadChildren()
        {
            while (fs.Paused)
            {
                System.Threading.Thread.Sleep(1000);
            }


            DirectoryInfo info = (DirectoryInfo)this.info;

            fs.TriggerUpdate(info.FullName);

            try
            {

                foreach (FileInfo file in info.GetFiles())
                {
                    File new_file = new File(this.fs, file);
                    this._size += new_file.Size;
                    this.Children.Add(new_file);
                }
            }
            catch (Exception e)
            {
                fs.TriggerUpdate(e.Message);
            }

            OnPropertyChanged("Display");


            try
            {
                foreach (DirectoryInfo dir in info.GetDirectories())
                {
                    try
                    {
                        if (Monitor.Core.Utilities.JunctionPoint.Exists(dir.FullName))
                        {
                            continue;
                        }
                        Folder fold = new Folder(this.fs, dir);
                        this._size += fold.Size;
                        this.Children.Add(fold);
                        fold.LoadChildren();

                    }
                    catch (Exception e)
                    {
                    }
                }
            }
            catch (Exception e)
            {
                fs.TriggerUpdate(e.Message);
            }


        }


    }
}
