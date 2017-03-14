using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;

namespace LAB2
{
    public partial class CreateWindow : Window
    {
        private MainWindow mainWindow;

        public CreateWindow(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            InitializeComponent();
        }

        private void Cencel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            File file = this.mainWindow.treeView.SelectedItem as File;
            string path = file.Fsi.FullName + @"\" + this.textBox.Text;

            if (this.File.IsChecked == true)
            {
                if (Regex.IsMatch(this.textBox.Text, @"[\w~\-]{1,8}.(txt|php|htm)"))
                {
                    if (!System.IO.File.Exists(path))
                    {
                        System.IO.File.Create(path).Dispose();

                        FileInfo child = new FileInfo(path);
                        this.setAttr(child);
                        file.Items.Add(new LAB2.File(child, file));

                        this.Close();
                    }
                }
            }
            else
            {
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);

                    DirectoryInfo child = new DirectoryInfo(path);
                    this.setAttr(child);
                    file.Items.Add(new LAB2.File(child, file));

                    this.Close();
                }
            }            
        }

        private void setAttr(FileSystemInfo child)
        {
            if (this.ReadOnly.IsChecked == true)
            {
                child.Attributes |= FileAttributes.ReadOnly;
            }
            if (this.Archive.IsChecked == true)
            {
                child.Attributes |= FileAttributes.Archive;
            }
            if (this.Hidden.IsChecked == true)
            {
                child.Attributes |= FileAttributes.Hidden;
            }
            if (this.SystemAtr.IsChecked == true)
            {
                child.Attributes |= FileAttributes.System;
            }
        }
    }
}
