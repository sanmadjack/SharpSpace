using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SharpSpace
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FileSystem.FileSystem file_system = new FileSystem.FileSystem();

        public MainWindow()
        {
            InitializeComponent();
            LoadFilesystemData();
            this.treeView1.DataContext = this.file_system;
            this.file_system.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(file_system_ProgressChanged);
        }

        void file_system_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
            //this.treeView1.Items.Refresh();
            //this.treeView1.UpdateLayout();
            if(e.UserState!=null) {
                this.lblProgress.Content = e.UserState;
            }
        }


        private void LoadFilesystemData()
        {
            this.file_system.RunWorkerAsync();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.file_system.Paused = !this.file_system.Paused;
        }

    }
}
