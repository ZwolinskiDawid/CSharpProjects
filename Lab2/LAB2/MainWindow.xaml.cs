using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace LAB2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuOpened(object sender, RoutedEventArgs e)
        {
            File file = this.treeView.SelectedItem as File;
            ContextMenu menu = sender as ContextMenu;
            MenuItem item = menu.Items.GetItemAt(0) as MenuItem;

            if(!(file.Fsi is DirectoryInfo))
            {
                item.Visibility = Visibility.Collapsed;
            }
        }

        private void SelectItem(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem treeViewItem = VisualUpwardSearch(e.OriginalSource as DependencyObject);

            if (treeViewItem != null)
            {
                treeViewItem.Focus();
                e.Handled = true;
            }
        }

        private TreeViewItem VisualUpwardSearch(DependencyObject source)
        {
            while (source != null && !(source is TreeViewItem))
                source = VisualTreeHelper.GetParent(source);

            return source as TreeViewItem;
        }

        private void MenuItem_Create(object sender, RoutedEventArgs e)
        {
            CreateWindow Window2 = new CreateWindow(this);
            Window2.Show();
        }

        private void MenuItem_Delete(object sender, RoutedEventArgs e)
        {
            File file = this.treeView.SelectedItem as File;

            file.delete();

            if(file.Parent is TreeView)
            {
                this.treeView.Items.Clear();
            }
            else if(file.Parent is File)
            {
                (file.Parent as File).Items.Remove(file);
            }
        }

        private void MenuItem_Open(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.ShowDialog();
            string path = dialog.SelectedPath;
            if (!string.IsNullOrWhiteSpace(path))
            {
                DirectoryInfo di = new DirectoryInfo(path);
                File root = new File(di, this.treeView);
                this.treeView.Items.Clear();
                this.treeView.Items.Add(root);
            }
        }

        private void MenuItem_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            File file = this.treeView.SelectedItem as File;
            if (file != null)
            {
                this.label.Content = file.Fsi.getAttributes();
            }

            if(file.Fsi != null && file.Fsi.Extension == ".txt")
            {
                var streamReader = new StreamReader(file.Fsi.FullName, Encoding.UTF8);
                this.textBox.Text = streamReader.ReadToEnd();
                streamReader.Close();
            }
            else
            {
                this.textBox.Text = "";
            }
        }
    }

    class File
    {
        public object Parent { get; }
        public FileSystemInfo Fsi { get; }
        public string Name { get; }
        public ObservableCollection<File> Items { get; set; }

        public File(FileSystemInfo Fsi, object Parent)
        {
            this.Parent = Parent;
            this.Fsi = Fsi;
            this.Name = Fsi.Name;
            this.Items = new ObservableCollection<File>();

            if(Fsi is DirectoryInfo)
            {
                foreach (FileSystemInfo Child in (Fsi as DirectoryInfo).GetFileSystemInfos())
                {
                    this.Items.Add(new File(Child, this));
                }
            }
        }

        public void delete()
        {
            if(this.Items.Count == 0)
            {
                this.Fsi.Attributes &= ~FileAttributes.ReadOnly;
                this.Fsi.Delete();
            }
            else
            {
                foreach (File file in this.Items)
                {
                    file.delete();
                }

                this.Fsi.Attributes &= ~FileAttributes.ReadOnly;
                this.Fsi.Delete();
            }
        }
    }
}
