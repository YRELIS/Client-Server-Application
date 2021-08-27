using ApplicationTZ.Core.Models;
using Microsoft.Win32;
using System;
using System.IO;
using System.Net;
using System.Web;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ClientWPF
{
    /// <summary>
    /// Логика взаимодействия для InsertPhone.xaml
    /// </summary>
    public partial class InsertPhone : Window
    {
        string pathFile = string.Empty;
        string imgBase64String = string.Empty;
        public InsertPhone()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Button method adding new item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_Insert(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(tb_model.Text))
            {
                MessageBox.Show("Не заполненно название модели.");
                return;
            }
            string model = tb_model.Text;

            if (String.IsNullOrEmpty(pathFile))
            {
                MessageBox.Show("Не выбрана картинка.");
                return;
            }
            imgBase64String = ConvertImage(pathFile);
            WebRequest request = WebRequest.Create("https://localhost:44318/Phone/Add?");
            request.Method = "POST";
            string sName = string.Format("ModelName={0}&base64Image={1}", HttpUtility.UrlEncode(model), HttpUtility.UrlEncode(imgBase64String));
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(sName);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
            Notify?.Invoke(new Phone(Guid.NewGuid(), DateTime.Now, model, DateTime.Now, imgBase64String) { });
            MessageBox.Show("Модель добавлена.");
            tb_model.Text = "";
            pathFile = "";
            imgPhonePhoto.Source = null;
        }
        public delegate void onAddHandler(Phone phone);
        public event onAddHandler Notify;
        /// <summary>
        /// Select Image by PC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileSelect(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                pathFile = openFileDialog.FileName;
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(pathFile);
                bi.EndInit();
                imgPhonePhoto.Source = bi;
            }
        }
        /// <summary>
        /// Convertation image in base 64
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string ConvertImage(string path)
        {
            byte[] imageBytes = System.IO.File.ReadAllBytes(path);
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;
        }
    }
}
