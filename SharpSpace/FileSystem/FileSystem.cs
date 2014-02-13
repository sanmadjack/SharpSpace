using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
namespace SharpSpace.FileSystem
{
    public class FileSystem: BackgroundWorker
    {
        public bool Paused = false;

        private Dictionary<string, BackgroundWorker> drive_threads;
        private MTObservableCollection<Disk> _disks = new MTObservableCollection<Disk>();
        public MTObservableCollection<Disk> Disks
        {
            get
            {
                return _disks;
            }
        }
        

        public FileSystem()
        {
            this.DoWork += new DoWorkEventHandler(bw_DoWork);
            this.WorkerReportsProgress = true;
        }


        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadFiles();
        }



        private void LoadFiles()
        {
            this._disks.Clear();
            foreach (DriveInfo drive in System.IO.DriveInfo.GetDrives())
            {
                if (drive.IsReady&&drive.DriveType==DriveType.Fixed && drive.Name.ToUpper().Contains("C"))
                {
                    this._disks.Add(new Disk(this,drive));
                }
            }

            foreach (Disk disk in this._disks)
            {
                disk.LoadChildren();
            }

        }
        public long TotalUsedSpace
        {
            get
            {
                long space = 0;
                foreach (Disk disk in this._disks)
                {
                    space += disk.UsedSpace;
                }
                return space;
            }
        }
        public long TotalScannedSpace
        {
            get
            {
                long size = 0;
                foreach (Disk disk in this._disks)
                {
                    size += disk.Size;
                }
                return size;
            }
        }

        private int CurrentProgress
        {
            get
            {
                Double percent = (Convert.ToDouble(TotalScannedSpace) / Convert.ToDouble(TotalUsedSpace)) * 100;
                Int32 output = Convert.ToInt32(Math.Floor(percent));
                return output;
            }
        }

        public void TriggerUpdate()
        {
            this.ReportProgress(CurrentProgress);
        }
        public void TriggerUpdate(string message)
        {
            this.ReportProgress(CurrentProgress,message);
        }


    }
}
