using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace SharpSpace.FileSystem
{
    public abstract class AFileSystemObject: INotifyPropertyChanged
    {
        public FileSystem fs;
        public AFileSystemObject Parent;
        public String Name { get; protected set; }

        protected FileSystemInfo info;

        private MTObservableCollection<AFileSystemObject> _children = new MTObservableCollection<AFileSystemObject>();
        public MTObservableCollection<AFileSystemObject> Children
        {
            get
            {
                return _children;
            }
        }
              
        public virtual long Size
        {
            get
            {
                return 0;
            }
        }

        public long ChildCount
        {
            get
            {
                long ChildCount = Children.Count;
                foreach (AFileSystemObject obj in Children)
                {
                    ChildCount += obj.ChildCount;
                }
                return ChildCount;
            }
        }


        public AFileSystemObject(FileSystem fs, FileSystemInfo fsi)
        {
            this.fs =fs;
            this.Name = fsi.Name;

            this.info = fsi;
        }

        public String Display
        {
            get
            {
                return String.Concat(this.Name, " (", this.FormatSize(), ")");
            }
        }

        public override string ToString()
        {
            return Display;
        }

        protected string FormatSize()
        {
            if (Size < 1000)
            {
                return string.Concat(this.Size, "B");
            }
            if (Size < 1000000)
            {
                return string.Concat(this.Size/1000, "KB");
            }
            if (Size < 1000000000)
            {
                return string.Concat(this.Size/1000000, "MB");
            }
            if (Size < 1000000000000)
            {
                return string.Concat(this.Size / 1000000000, "GB");
            }
            if (Size < 1000000000000000)
            {
                return string.Concat(this.Size / 1000000000000, "TB");
            }
            return string.Concat(this.Size / 1000000000000, "TB");
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

    }


}
