using ApplicationTZ.Core.Models;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Web;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ClientWPF
{
    /// <summary>
    /// Логика взаимодействия для EditPhone.xaml
    /// </summary>
    public partial class EditPhone : Window
    {
        Guid guidID;
        string pathFileNew = string.Empty;
        string imgBase64StringNew = string.Empty;
        public Phone phone { get; set; } = new Phone();
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="guid"></param>
        public EditPhone(Guid guid)
        {
            InitializeComponent();
            this.DataContext = this;
            guidID = guid;
            phone = GetPhoneByID();
        }

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
                pathFileNew = openFileDialog.FileName;
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(pathFileNew);
                bi.EndInit();
                imgPhonePhoto.Source = bi;
            }
        }

        /// <summary>
        /// Button event changes item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_Edit(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(tb_model.Text))
            {
                MessageBox.Show("Не заполненно название модели.");
                return;
            }
            string model = tb_model.Text;

            if (String.IsNullOrEmpty(pathFileNew))
            {
                imgBase64StringNew = phone.base64Image;
            }
            else
            {
                imgBase64StringNew = ConvertImage(pathFileNew);
            }
            phone.Model = tb_model.Text;
            phone.base64Image = imgBase64StringNew;
            WebRequest request = WebRequest.Create("https://localhost:44318/Phone/Edit");
            request.Method = "POST";
            string sName = string.Format("phone={0}", HttpUtility.UrlEncode(phone.ToJson()));
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(sName);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
            request.GetResponse();
            Notify?.Invoke(new Phone(phone.Id, phone.CreatedOn, phone.Model, phone.UpdatedOn, phone.base64Image) { });
            MessageBox.Show("Модель изменена.");
            tb_model.Text = "";
            pathFileNew = "";
            imgPhonePhoto.Source = null;
            Close();
        }
        public delegate void onAddHandler(Phone phone);
        public event onAddHandler Notify;
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

        /// <summary>
        /// Return all items
        /// </summary>
        /// <returns></returns>
        private Phone GetPhoneByID()
        {
            try
            {
                WebRequest request = WebRequest.Create("https://localhost:44318/Phone/GetByID");
                request.Method = "POST";
                string sName = string.Format("guid={0}", HttpUtility.UrlEncode(guidID.ToString()));
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(sName);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;

                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }
                var response = request.GetResponse();
                string strReader = "";
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    strReader = stream.ReadToEnd();
                }
                JObject joResponse = JObject.Parse(strReader);
                var objId = joResponse["Id"];
                var objCreatedOn = joResponse["CreatedOn"];
                var objModel = joResponse["Model"];
                var objUpdatedOn = joResponse["UpdatedOn"];
                var objbase64Image = joResponse["base64Image"];
                Guid id = new Guid(objId.ToString());
                DateTime createdOn = DateTime.Parse(objCreatedOn.ToString());
                string model = objModel.ToString();
                DateTime updatedOn = DateTime.Parse(objUpdatedOn.ToString());
                string baseImage = objbase64Image.ToString();
                return new Phone(id, createdOn, model, updatedOn, baseImage);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Server error: {ex.Message}");
            }
            return default;

        }
    }
}
