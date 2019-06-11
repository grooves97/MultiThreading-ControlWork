using System;
using System.Collections.Generic;
using System.IO;
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
using Dropbox.Api;
using Dropbox.Api.Files;
using Microsoft.Win32;

namespace Dropbox.Ui
{
    /// <summary>
    /// Логика взаимодействия для AppWindow.xaml
    /// </summary>
    public partial class AppWindow : Window
    {
        private static string token = "EV-YEhTT8EAAAAAAAAAADT-uVKZMewntnUm4889BEoaKADdtKZgMo4T6zqbkF14r";
        public AppWindow()
        {
            InitializeComponent();
            GetFiles();
        }
        private async void GetFiles()
        {
            using (var dropbox = new DropboxClient(token))
            {
                var list = await dropbox.Files.ListFolderAsync(string.Empty);
                filesListBox.Items.Clear();
                foreach (var item in list.Entries.Where(i => i.IsFile))
                {
                    filesListBox.Items.Add(item.Name);
                }
            }
        }

        private async void UploadButtonClick(object sender, RoutedEventArgs e)
        {
            using (var dropbox = new DropboxClient(token))
            {
                OpenFileDialog file = new OpenFileDialog();
                try
                {
                    file.ShowDialog();
                    await dropbox.Files.UploadAsync($"/{file.SafeFileName}", null, false, null, false, null, false, file.OpenFile());
                }
                catch (Exception)
                {
                    MessageBox.Show("Error. Please try again");
                    return;
                }
                GetFiles();
            }
        }

        private void CloseButonClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        //private async Task Upload(DropboxClient dbx)
        //{
        //    var task = Task.Run((Func<Task>)Run);
        //    task.Wait();

        //    using (var client = new DropboxClient(token))
        //    {
        //        OpenFileDialog openFileDialog = new OpenFileDialog();
        //        if (openFileDialog.ShowDialog() == true)
        //        {
        //            using (var mem = new MemoryStream(File.ReadAllBytes(openFileDialog.FileName)))
        //            {
        //                var updated = await dbx.Files.UploadAsync(openFileDialog.SafeFileName, WriteMode.Overwrite.Instance, body: mem);
        //                MessageBox.Show("Saved {0}/{1} rev {2}", updated.Rev);
        //            }
        //        }
        //    }
        //}
    }
}
